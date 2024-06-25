using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputController controller;
    public Rigidbody2D movementRigidbody;
    public GroundCheck groundCheck;
    public GameObject playerAttackBoxPrefab;
    private CharacterStatHandler characterStatHandler;

    public Vector2 movementDirection = Vector2.zero;
    private bool canDoubleJump = false;
    public float jumpForce = 7f;
    private SpriteRenderer sprite;

    public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;

    private AudioSource moveAudioSource;
    private AudioSource jumpAudioSource;
    private AudioSource attackAudioSource;

    private void Awake()
    {
        controller = GetComponent<InputController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        characterStatHandler = GetComponent<CharacterStatHandler>();

        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
        controller.OnAttackEvent += Attack;
        groundCheck.OnGroundedEvent += OnLand;

        moveAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource.clip = moveSound;
        moveAudioSource.loop = true;
        moveAudioSource.playOnAwake = false;

        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        jumpAudioSource.clip = jumpSound;
        jumpAudioSource.playOnAwake = false;

        attackAudioSource = gameObject.AddComponent<AudioSource>();
        attackAudioSource.clip = attackSound;
        attackAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!groundCheck.GetGroundedState() && !groundCheck.GetHilledState() && moveAudioSource.isPlaying)
        {
            moveAudioSource.Stop();
        }
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;

        if (direction.x > 0)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
        }

        if (groundCheck.GetGroundedState() || groundCheck.GetHilledState())
        {
            if (direction.magnitude > 0 && !moveAudioSource.isPlaying)
            {
                moveAudioSource.Play();
            }
            else if (direction.magnitude == 0 && moveAudioSource.isPlaying)
            {
                moveAudioSource.Stop();
            }
        }
    }

    private void Jump()
    {
        if (groundCheck.GetGroundedState() || groundCheck.GetHilledState())
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            canDoubleJump = true;
            moveAudioSource.Stop();
            jumpAudioSource.Play();
        }
        else if (canDoubleJump)
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            canDoubleJump = false;
            jumpAudioSource.Play();
        }
    }

    private void Attack()
    {
        PlayerDefaultAttackSO rangedAttackSo = characterStatHandler.CurrentStat.attackSO as PlayerDefaultAttackSO;

        attackAudioSource.Play();

        GameObject obj = Instantiate(playerAttackBoxPrefab);
        playerAttackBoxPrefab.SetActive(true);
        PlayerAttackBox abox = obj.GetComponent<PlayerAttackBox>();
        abox.Initialize(rangedAttackSo);
        controller.isAttacking = false;
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction *= characterStatHandler.CurrentStat.speed;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }

    private void OnLand()
    {
        if (movementDirection.magnitude > 0)
        {
            moveAudioSource.Play();
        }
    }
}
