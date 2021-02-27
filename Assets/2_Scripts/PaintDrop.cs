using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EPaintColor
{
    BLUE,
    GREEN,
    RED,
    YELLOW,
    DEFUALT
}

[Serializable]
struct ColorMap
{
    public EPaintColor colorType;
    public Material colorMat;
}

public class PaintDrop : MonoBehaviour
{
    [SerializeField] bool RandomColor = true;
    [SerializeField] EPaintColor paintColor = EPaintColor.DEFUALT;
    [SerializeField] ColorMap[] colorMap;
    [SerializeField] ParticleSystem SplashParticle;

    void Start()
    {
        if(RandomColor)
        {
            paintColor = (EPaintColor)UnityEngine.Random.Range(0, (int)EPaintColor.DEFUALT);
        }
        GetComponent<Renderer>().material = SplashParticle.GetComponent<ParticleSystemRenderer>().material = FindMat(paintColor);
    }

    Material FindMat(EPaintColor color)
    {
        foreach (ColorMap c in colorMap)
        {
            if(c.colorType == color)
            {
                return c.colorMat;
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SplashOnCam();
            SplashParticle.Play();
            GetComponent<AudioSource>().Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
            pc.meshRenderer.material.color = (pc.meshRenderer.material.color + FindMat(paintColor).color)/2;
            Invoke("KillSelf", SplashParticle.main.duration);
        }
    }

    void KillSelf()
    {
        Destroy(gameObject);
    }

    void SplashOnCam()
    {
        Camera.main.gameObject.GetComponent<CamScript>().SplashColor(FindMat(paintColor).color);
    }
}
