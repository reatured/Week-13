using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtonInteractable : XRBaseInteractable
{
    public List<Material> skybox = new List<Material>();
    public int count = 0;

    
    // Start is called before the first frame update
    void startPush()
    {
        RenderSettings.skybox = skybox[count];
        count++;
        if(count > 2)
        {
            count = 0;
        }
    }

    void endPush()
    {


    }

    public void selectedTest()
    {
        startPush();
        Debug.Log("selected");
    }
}
