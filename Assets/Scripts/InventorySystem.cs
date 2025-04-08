using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InventorySystem : MonoBehaviour
{
    public Canvas canvas;
    public GameObject GameObject; 
  
    public void Start()
    {
        if(canvas && GameObject == null)
        {
            canvas = GetComponent<Canvas>();
            GameObject = GetComponent<GameObject>();
        }
    }
}
