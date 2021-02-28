using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    [SerializeField] PaintDrop[] Drops;
    // Start is called before the first frame update
    void Start()
    {
        ResetDrops();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetDrops()
    {
        foreach (PaintDrop drop in Drops)
        {
            drop.ResetDrop();
        }
        Drops[Random.Range(0, Drops.Length)].HideDrop();
    }
}
