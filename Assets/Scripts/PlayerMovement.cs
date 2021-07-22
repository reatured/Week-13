using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    public void playerRotate(float angle)
    {
        float yAngle = map(angle, 0, 1, -0.5f, 0.5f);
        transform.Rotate(0, yAngle, 0, Space.Self);
    }
}
