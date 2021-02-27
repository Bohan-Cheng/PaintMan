using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintShaderControl : MonoBehaviour {

    Renderer modelRenderer;
    float controlTime;

    // Use this for initialization
    void Start () {
        modelRenderer = GetComponent<MeshRenderer>();
    }

	// Update is called once per frame
	void Update () {
        controlTime += Time.deltaTime;

        modelRenderer.material.SetFloat("_ControlTime", controlTime);
	}
}
