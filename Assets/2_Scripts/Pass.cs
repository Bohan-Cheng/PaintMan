using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    [SerializeField] AudioClip clip_Score, clip_Crash;
    [SerializeField] GameMana GM;
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
        GM = FindObjectOfType<GameMana>();
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
            bool CloseColor = CompareColor(GM.player.meshRenderer.material.color, Door1MR.material.color);
            if (CloseColor && GM.player.hasColor)
            {
                Score(GetScore(GM.player.meshRenderer.material.color));
            }
            else
            {
                Crash();
            }
            shouldReset = true;
            OpenDoor();
            Invoke("DoneReset", 5.0f);
            GM.player.ReColorPlayer();
        }
    }

    bool CompareColor(Color col1, Color col2)
    {
        return
           IsLargest(col1.r, col1) && IsLargest(col2.r, col2)
        || IsLargest(col1.g, col1) && IsLargest(col2.g, col2)
        || IsLargest(col1.b, col1) && IsLargest(col2.b, col2)
        || IsLargest(MidY(col1.r, col1.g), col1) && IsLargest(MidY(col2.r, col2.g), col2);
    }

    float MidY(float r, float g)
    {
        return (r + g) / 2;
    }

    bool IsLargest(float colV, Color col)
    {
        float max = col.r;
        if(col.g > max)
        {
            max = col.g;
        }
        if(col.b > max)
        {
            max = col.b;
        }
        if (MidY(col.r, col.g) > max)
        {
            max = MidY(col.r, col.g);
        }
        return colV == max;
    }

    int GetScore(Color col)
    {
        float max = col.r;
        if (col.g > max)
        {
            max = col.g;
        }
        if (col.b > max)
        {
            max = col.b;
        }
        if (MidY(col.r, col.g) > max)
        {
            max = MidY(col.r, col.g);
        }
        return (int)(max*100);
    }

    void DoneReset()
    {
        if (!GM.IsGameOver)
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
    }

    void ResetStartPass()
    {
        if(EndPass)
        {
            paintColor = EndPass.paintColor;
            OriColor = FindMat(paintColor).color;
        }
    }

    void Score(int score)
    {
        GetComponent<AudioSource>().PlayOneShot(clip_Score);
        GM.AddScore(score);
    }

    void Crash()
    {
        GetComponent<AudioSource>().PlayOneShot(clip_Crash);
        GM.player.Stuned();
        GM.Health--;
        GM.CheckHealth();
    }
}
