using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChaser : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The player's main transform (root object).")]
    public Transform player;

    [Tooltip("Empty child object on the PLAYER, placed at camera/eye height.")]
    public Transform playerEyes;

    [Tooltip("Empty child object on the ENEMY, placed at a visible point (chest/head).")]
    public Transform enemyEyes;

    [Header("Detection Settings")]
    [Tooltip("Everything the raycast should treat as a solid obstacle. Do NOT include Player or Enemy layers here.")]
    public LayerMask obstacleMask;

    [Tooltip("Should match your player camera's FOV.")]
    [Range(1f, 179f)] public float playerFOV = 60f;

    [Tooltip("How long the enemy must be continuously visible before it freezes.")]
    public float freezeDelay = 0.1f;

    [Tooltip("How long the enemy must be continuously hidden before it resumes moving.")]
    public float resumeDelay = 0.3f;


    private NavMeshAgent agent;
    private bool isFrozen = false;
    private float seenTimer = 0f;
    private float unseenTimer = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool visible = IsVisibleToPlayer();

        if (visible)
        {
            seenTimer += Time.deltaTime;
            unseenTimer = 0f;

            if (!isFrozen && seenTimer >= freezeDelay)
            {
                Freeze();
            }
        }
        else
        {
            unseenTimer += Time.deltaTime;
            seenTimer = 0f;

            if (isFrozen && unseenTimer >= resumeDelay)
            {
                Unfreeze();
            }
        }

        if (!isFrozen && player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    bool IsVisibleToPlayer()
    {
        if (player == null || playerEyes == null || enemyEyes == null)
            return false;

        Vector3 dirToEnemy = enemyEyes.position - playerEyes.position;
        float distance = dirToEnemy.magnitude;

        float angle = Vector3.Angle(playerEyes.forward, dirToEnemy);
        if (angle > playerFOV * 0.5f)
            return false;
        
        int combinedMask = obstacleMask | (1 << enemyEyes.gameObject.layer);

        if (Physics.Raycast(playerEyes.position, dirToEnemy.normalized, out RaycastHit hit, distance, combinedMask))
        {
            if (hit.transform.root != transform.root)
            {
                return false;
            }
        }

        return true;
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
