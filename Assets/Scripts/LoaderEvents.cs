using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderEvents : MonoBehaviour
{
    public GameObject LoaderImage;

   public void turnOff()
    {
        LoaderImage.SetActive(false);        
    }

    public void turnOn()
    {
        LoaderImage.SetActive(true);
    }
}
