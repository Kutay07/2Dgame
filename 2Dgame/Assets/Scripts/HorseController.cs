using UnityEngine;
using UnityEngine.InputSystem;

public class HorseController : MonoBehaviour
{
  [SerializeField] private float movementSpeed = 7;
  [SerializeField] private float jumpForce = 12;

  private Rigidbody2D rb;
  private BoxCollider2D coll;
  [SerializeField] private LayerMask jumpableLayer;
  private Animator animator;
  private SpriteRenderer sprite;
  [SerializeField] private AudioSource jumpSoundEffect;

  private enum MovementState { idle, running, jumping, falling }
  private Vector2 inputVector;
  private MovementState state = MovementState.idle;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
    animator = GetComponent<Animator>();
    sprite = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    Move();
    UpdateAnimationState();
  }

  private void Move()
  {
    Vector2 delta = new Vector2(inputVector.x * movementSpeed * Time.deltaTime, 0);
    transform.Translate(delta);
  }

  private void Jump()
  {
    if (IsGrounded())
    {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
  }

  private bool IsGrounded()
  {
    return Physics2D.BoxCast(
        coll.bounds.center,
        coll.bounds.size,
        0f,
        Vector2.down,
        .1f,
        jumpableLayer
    );
  }

  private void UpdateAnimationState()
  {
    if (inputVector.x > 0f)
    {
      state = MovementState.running;
      sprite.flipX = false;
    }
    else if (inputVector.x < 0f)
    {
      state = MovementState.running;
      sprite.flipX = true;
    }
    else
    {
      state = MovementState.idle;
    }

    if (rb.velocity.y > .1f)
    {
      state = MovementState.jumping;
    }
    else if (rb.velocity.y < -.1f)
    {
      state = MovementState.falling;
    }

    animator.SetInteger("state", (int)state);
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    inputVector = context.ReadValue<Vector2>();
  }
  public void OnJump(InputAction.CallbackContext context)
  {
    if (context.started)
    {
      Jump();
    }
  }

}