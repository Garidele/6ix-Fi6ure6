using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    public Canvas finishCanvas;

    void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<CharacterController>())
            return;
        Debug.Log("coliziune");
        SceneManager.LoadSceneAsync("RetryScreen");
    }
}
