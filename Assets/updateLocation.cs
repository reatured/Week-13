using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateLocation : MonoBehaviour
{
    public Transform cameraTrans;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
   
    {
        transform.position = cameraTrans.position + new Vector3(0, -1, -0.5f);
    }
}
