using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{

    public static bool zoomActive = false;

    public Camera cam;

    public float speed;


    void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        if (PlayerTriggers.inTriggerZoom == true)
        {
            ZoomIn();
        }
        else if (PlayerTriggers.inTriggerZoom == false)
        {
            ZoomOut();
        }
        
        if(PlayerTriggers.ampliedZoom == true)
        {
            AmpliarTela();
        }

        else
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,3,speed);
    }
    
    public void ZoomOut()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,5,speed);
    }

    public void AmpliarTela()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,7,speed);
    }
}
