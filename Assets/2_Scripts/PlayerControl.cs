using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction MoveInput;
    public SkinnedMeshRenderer meshRenderer;
    public float runSpeed = 1.0f;
    public float turnSpeed = 5000.0f;

    bool IsStarted = false;
    Vector3 StartPosition;
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
        StartPosition = transform.position;
        Invoke("StartRunning", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStarted)
        {
            rigid.velocity = new Vector3(0.0f, 0.0f, runSpeed);
            rigid.AddForce(new Vector3(MoveInput.ReadValue<float>() * turnSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
    }

    public void ResetPlayer()
    {
        transform.position = StartPosition;
    }

    void StartRunning()
    {
        IsStarted = true;
    }
}
