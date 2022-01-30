using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class shadertest : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;

    bool show = false;
    bool hide = false;
    bool showH = true;
    bool hideH = true;

    float height = 0;// = transform.position.y;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {


        if (gameObject.name == "Icosphere")
        {

            if (hide)
            {
                var time = Time.time * Mathf.PI * 0.25f;
                height = transform.position.y + Mathf.Sin(time) * (objectHeight / 2.0f);
                if (height + 2.4f < transform.position.y)
                {
                    hide = false;
                }
                SetHeight(height);
            }

            if (show)
            {
                var time = Time.time * Mathf.PI * 0.25f;
                height = transform.position.y + Mathf.Sin(time) * (objectHeight / 2.0f);
                if (height - 1f > transform.position.y)
                {
                    show = false;
                }
                SetHeight(height);
            }
        }
        else
        {
            if (hideH)
            {
                var time = Time.time * Mathf.PI * 0.25f;
                height = transform.position.y + Mathf.Sin(time) * (objectHeight / 2.0f);
                if (height + 0.8f  < transform.position.y)
                {
                    Debug.Log("alhaalla");
                    hideH = false;
                }
                SetHeight(height);
            }
            if (showH)
            {
                var time = Time.time * Mathf.PI * 0.25f;
                height = transform.position.y + Mathf.Sin(time) * (objectHeight / 2.0f);
                if (height - 0.7f > transform.position.y)
                {
                    Debug.Log("Ylhaalla");
                    showH = false;
                }
                SetHeight(height);
            }
        }

        
     //  if(Input.GetKeyDown(KeyCode.DownArrow))
     //      Hide();
     //  if (Input.GetKeyDown(KeyCode.UpArrow))
     //      ShowUp();

    }

    public void Hide()
    {
        hide = true;
        hideH = true;
        show = false;
        showH = false;
    }
    public void ShowUp()
    {
        hide = false;
        hideH = false;
        show = true;
        showH = true;
    }

    public void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}

