using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    public Canvas finishCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<CharacterController>())
            return;
        Debug.Log("coliziune");
        finishCanvas.enabled = true;
    }
}
