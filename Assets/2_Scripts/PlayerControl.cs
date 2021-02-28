using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EPlayerAnimState
{
    IDLE,
    RUN,
    STUN
}

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction MoveInput;
    [SerializeField] Transform loopTarget;
    [SerializeField] Transform StartTarget;
    Animation Anim;
    public SkinnedMeshRenderer meshRenderer;
    public Color OriColor;
    public float runSpeed = 1.0f;
    public float turnSpeed = 5000.0f;
    public bool hasColor = true;

    bool IsStarted = false;
    bool IsStuned = false;
    Rigidbody rigid;
    // Start is called before the first frame update

    private void OnEnable()
    {
        MoveInput.Enable();
    }

    private void OnDisable()
    {
        MoveInput.Disable();
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        Anim = GetComponent<Animation>();
    }

    void Start()
    {
        OriColor = meshRenderer.material.color;
        Invoke("StartRunning", FindObjectOfType<GameMana>().StartCD + 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStarted)
        {
            if (!IsStuned)
            {
                LoopPlayer();
                rigid.velocity = new Vector3(0.0f, 0.0f, runSpeed);
                rigid.AddForce(new Vector3(MoveInput.ReadValue<float>() * turnSpeed * Time.deltaTime, 0.0f, 0.0f));
            }
            else
            {
                rigid.velocity = Vector3.zero;
            }
        }
    }

    void LoopPlayer()
    {
        if (transform.position.z >= loopTarget.position.z)
        {
            transform.position = new Vector3(transform.position.x, StartTarget.position.y, StartTarget.position.z);
            FindObjectOfType<GameMana>().ResetMap();
            runSpeed++;
        }
    }

    public void ReColorPlayer()
    {
        meshRenderer.material.color = OriColor;
        hasColor = false;
    }

    void StartRunning()
    {
        IsStarted = true;
        SetAnimation(EPlayerAnimState.RUN);
    }

    public void SetAnimation(EPlayerAnimState state)
    {
        switch (state)
        {
            case EPlayerAnimState.IDLE:
                Anim.clip = Anim.GetClip("Idle");
                break;
            case EPlayerAnimState.RUN:
                Anim.clip = Anim.GetClip("Run");
                break;
            case EPlayerAnimState.STUN:
                Anim.clip = Anim.GetClip("Dizzy");
                break;
            default:
                Anim.clip = Anim.GetClip("Idle");
                break;
        }
        Anim.Play();
    }

    public void Stuned()
    {
        IsStuned = true;
        SetAnimation(EPlayerAnimState.STUN);
        Invoke("Recover", 3.0f);
    }

    void Recover()
    {
        IsStuned = false;
        SetAnimation(EPlayerAnimState.RUN);
    }

    public void Score(int score)
    {
        FindObjectOfType<GameMana>().AddScore(score);
    }

    public void StopPlaying()
    {
        rigid.velocity = Vector3.zero;
        enabled = false;
        CancelInvoke("Recover");
    }
}
