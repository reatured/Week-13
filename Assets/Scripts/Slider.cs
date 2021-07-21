using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform startPosition = null;
    public Transform endPosition = null;

    MeshRenderer meshRenderer = null;

    public Vector3 position;

    private void Start()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();

    }

    public void onSliderStart()
    {
        Debug.Log("Changing Colors");
        meshRenderer.material.SetColor("_Color", Color.red);

    }
    public void onSliderStop()
    {

        meshRenderer.material.SetColor("_Color", Color.white);

    }

    public void UpdateSlider(float percent)
    {
        
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, percent);
        Debug.Log("Update position:: " + transform.position);
        position = transform.position;
    }

}
