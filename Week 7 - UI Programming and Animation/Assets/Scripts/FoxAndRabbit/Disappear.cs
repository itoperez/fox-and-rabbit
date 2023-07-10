using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    private MeshRenderer objectRenderer;
    private Material objectMaterial;

    private Color tempColor;
    private bool collision;
    
    public float opacityStep;
    public float minOpacity;

    AudioSource audioData;

    void Start()
    {
        objectRenderer = gameObject.GetComponent<MeshRenderer>();
        objectMaterial = objectRenderer.material;

        tempColor = new Color(objectMaterial.color.r, objectMaterial.color.g, objectMaterial.color.b, objectMaterial.color.a);
        collision = false;

        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (collision)
        {
            tempColor.a -= opacityStep;
            objectMaterial.SetColor("_Color", tempColor);
        }

        if (tempColor.a < minOpacity)
        {
            gameObject.SetActive(false);
            gameObject.GetComponentInParent<RabbitMovement>().enabled = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            collision = true;
            audioData.Play();
        }            
    }
}
