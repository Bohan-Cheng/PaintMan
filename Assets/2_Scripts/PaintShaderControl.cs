using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintShaderControl : MonoBehaviour {

    [SerializeField] float loopTime = 1.8f;
    [SerializeField] bool useRandomTime;
    [SerializeField] bool useRandomImpactPoint;
    [SerializeField] Vector4 ImpactPoint;
    Renderer modelRenderer;
    float controlTime;

    // Use this for initialization
    void Start () {
        if(useRandomImpactPoint)
        {
            ImpactPoint.x = Random.Range(-5.0f, 5.0f);
            ImpactPoint.y = Random.Range(-5.0f, 5.0f);
            ImpactPoint.z = Random.Range(-5.0f, 5.0f);
        }
        if(useRandomTime)
        {
            controlTime = Random.Range(0.0f, loopTime);
        }
        modelRenderer = GetComponent<MeshRenderer>();
        Vector4 pos = transform.position;
        modelRenderer.material.SetVector("_ModelOrigin", pos);
        modelRenderer.material.SetVector("_ImpactOrigin", ImpactPoint + pos);
    }

    // Update is called once per frame
    void Update () {
        if (controlTime >= loopTime)
        {
            controlTime = 0;
        }
        controlTime += Time.deltaTime;
        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}
}
