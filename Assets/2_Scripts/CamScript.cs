using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    [SerializeField] Transform Target;
    Camera Cam;
    Color OrCol;
    float ZOff;

    void Start()
    {
        Cam = GetComponent<Camera>();
        OrCol = Cam.backgroundColor;
        ZOff = transform.position.z - Target.position.z;
    }

    void Update()
    {
        Cam.backgroundColor = Color.Lerp(Cam.backgroundColor, OrCol, 2 * Time.deltaTime);
        Vector3 Pos = transform.position;
        Pos.z = Target.position.z + ZOff;
        transform.position = Pos;
    }

    public void SplashColor(Color color)
    {
        Cam.backgroundColor = color;
    }
}
