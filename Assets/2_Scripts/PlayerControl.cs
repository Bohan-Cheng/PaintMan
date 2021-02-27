using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction MoveInput;
    [SerializeField] Transform loopTarget;
    [SerializeField] Transform StartTarget;
    public SkinnedMeshRenderer meshRenderer;
    Color OriColor;
    public float runSpeed = 1.0f;
    public float turnSpeed = 5000.0f;

    bool IsStarted = false;
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
    }

    void Start()
    {
        OriColor = meshRenderer.material.color;
        Invoke("StartRunning", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStarted)
        {
            LoopPlayer();
            rigid.velocity = new Vector3(0.0f, 0.0f, runSpeed);
            rigid.AddForce(new Vector3(MoveInput.ReadValue<float>() * turnSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
    }

    void LoopPlayer()
    {
        if (transform.position.z >= loopTarget.position.z)
        {
            transform.position = new Vector3(transform.position.x, StartTarget.position.y, StartTarget.position.z);
        }
    }

    public void ReColorPlayer()
    {
        meshRenderer.material.color = OriColor;
    }

    void StartRunning()
    {
        IsStarted = true;
    }
}
