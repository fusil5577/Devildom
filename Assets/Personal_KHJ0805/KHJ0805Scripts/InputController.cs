using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnAttackEvent;

    public bool isAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;

    protected CharacterStatHandler stats;
    protected GroundCheck groundCheck;

    public AudioClip onMoveSound;
    public AudioClip onJumpSound;
    public AudioClip onAttackSound;

    private AudioSource moveAudioSource;
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
        groundCheck = GetComponent<GroundCheck>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        moveAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource.clip = onMoveSound;
        moveAudioSource.loop = true;
        moveAudioSource.playOnAwake = false;
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (stats == null || stats.CurrentStat == null || stats.CurrentStat.attackSO == null)
        {
            return;
        }

        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (isAttacking && timeSinceLastAttack >= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }

    public void CallAttackEvent()
    {
        audioSource.PlayOneShot(onAttackSound);

        OnAttackEvent?.Invoke();
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);

        if (groundCheck.GetGroundedState() || groundCheck.GetHilledState() && direction != Vector2.zero && !moveAudioSource.isPlaying)
        {
            moveAudioSource.Play();
        }
        else if (direction == Vector2.zero && moveAudioSource.isPlaying)
        {
            moveAudioSource.Stop();
        }
    }

    public void CallJumpEvent()
    {
        if(!groundCheck.GetGroundedState())
        {
            audioSource.PlayOneShot(onJumpSound);
        }
        
        OnJumpEvent?.Invoke();
    }
}
