using UnityEditorInternal;
using UnityEngine;

public class DeplacementJoueur : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    
    private bool isJumping;
    private bool isGrounded;
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static DeplacementJoueur instance;

    // La fonction Awake est lue même avant la fonction start
    private void Awake()
    {

        // Permet de s'assurer qu'il n'y a qu'un seul Inventory
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DeplacementJoueur dans la scène");
            return;
        }

        instance = this;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
        }
        
        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    // Dans FixedUpdate, on met que les opérations qui concernent la physique
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing) 
        {
            // Déplacement horizontal
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y); // On fait rien à la verticale, on fait quelque chose à l'horizontale
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

                if (isJumping)
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                    isJumping = false;
                }
        }
        else
        {
            //Déplacement vertical
            Vector3 targetVelocity = new Vector2(0,_verticalMovement); // On fait rien à l'horizontale, on fait quelque chose à la verticale
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity<-0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
