using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class Enemy : MonoBehaviour
{
    public Canvas deathCanvas;
    FirstPersonController player;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>())
        {
            deathCanvas.enabled = true;
        }
        else return;
    }
}
