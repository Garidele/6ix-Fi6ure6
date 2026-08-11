using UnityEngine;
using UnityEngine.AI;

public class IeleleChaser : MonoBehaviour
{

    private StaminaSystem stamina;
    public GameObject playerS;

    public GameObject ielele;
    private NavMeshAgent agent;
    private bool isFrozen = true;

    void Awake()
    {
        agent = ielele.GetComponent<NavMeshAgent>();
        stamina = playerS.GetComponent<StaminaSystem>();
        // activate = ielele.GetComponent<GameObject>();
        isFrozen = true;
        ielele.SetActive(false);
    }

    void Update()
    {
        //Debug.Log("void update");
        
        if (isFrozen && stamina.currentStamina <= 40)
        {
            Unfreeze();
            ielele.SetActive(true);
        }
        if(!isFrozen && stamina.currentStamina > 40)
        {
            Freeze();
            ielele.SetActive(false);
        }
        if (!isFrozen && playerS != null)
        {
            agent.SetDestination(playerS.transform.position);
            
        }
        
    }

    void Freeze()
    {
        isFrozen = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        // animations
        // audio
    }

    void Unfreeze()
    {
        isFrozen = false;
        agent.isStopped = false;

        // animations
        // audio
    }
}
