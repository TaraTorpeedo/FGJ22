using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class shadertest : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;

    bool doIt = false;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        var time = Time.time * Mathf.PI * 0.25f;

        float height = transform.position.y;
        height += Mathf.Sin(time) * (objectHeight / 2.0f);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            doIt = true;
        }
        if (doIt)
        {
            Debug.Log("asd");
            SetHeight(height);

        }
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}

