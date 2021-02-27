using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintShaderControl : MonoBehaviour {

    [SerializeField]float loopTime = 1.8f;
    Renderer modelRenderer;
    float controlTime;

    // Use this for initialization
    void Start () {
        modelRenderer = GetComponent<MeshRenderer>();
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
