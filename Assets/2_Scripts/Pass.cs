using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    [SerializeField] Pass EndPass;
    [SerializeField] MeshRenderer Door1MR;
    [SerializeField] MeshRenderer Door2MR;
    [SerializeField] bool RandomColor = true;
    public EPaintColor paintColor = EPaintColor.DEFUALT;
    [SerializeField] ColorMap[] colorMap;
    public bool shouldOpen = false;
    Color OriColor;
    Animator anim;
    bool shouldReset = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (shouldOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false;
        shouldOpen = true;
        anim.SetBool("DoorOpened", shouldOpen);
    }

    void CloseDoor()
    {
        GetComponent<BoxCollider>().enabled = true;
        shouldOpen = false;
        anim.SetBool("DoorOpened", shouldOpen);
    }

    // Start is called before the first frame update
    void Start()
    {
        OriColor = Door1MR.material.color;
        if (RandomColor)
        {
            paintColor = (EPaintColor)Random.Range(0, (int)EPaintColor.DEFUALT);
        }
        Door1MR.material = Door2MR.material = FindMat(paintColor);
    }

    Material FindMat(EPaintColor color)
    {
        foreach (ColorMap c in colorMap)
        {
            if (c.colorType == color)
            {
                return c.colorMat;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldReset)
        {
            Door1MR.material.color = Color.Lerp(Door1MR.material.color, OriColor, 5.0f * Time.deltaTime);
            Door2MR.material.color = Color.Lerp(Door2MR.material.color, OriColor, 5.0f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldReset = true;
            OpenDoor();
            Invoke("DoneReset", 2.0f);
            other.gameObject.GetComponent<PlayerControl>().ReColorPlayer();
        }
    }

    void DoneReset()
    {
        shouldReset = false;
        CloseDoor();
        if (RandomColor)
        {
            paintColor = (EPaintColor)Random.Range(0, (int)EPaintColor.DEFUALT);
            OriColor = FindMat(paintColor).color;
        }
        ResetStartPass();
        Door1MR.material.color = OriColor;
        Door2MR.material.color = OriColor;
    }

    void ResetStartPass()
    {
        if(EndPass)
        {
            paintColor = EndPass.paintColor;
            OriColor = FindMat(paintColor).color;
        }
    }
}
