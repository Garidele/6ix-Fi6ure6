using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(StaminaSystem))]
public class StaminaSprintGate : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Auto-filled from this GameObject if left empty.")]
    public StarterAssetsInputs starterInputs;

    [Header("Drain Settings")]
    public float staminaDrainPerSecond = 20f;

    private StaminaSystem stamina;

    void Awake()
    {
        stamina = GetComponent<StaminaSystem>();

        if (starterInputs == null)
            starterInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (starterInputs == null || stamina == null) return;

        bool isMoving = starterInputs.move.sqrMagnitude > 0.01f;
        bool wantsToSprint = starterInputs.sprint;

        if (wantsToSprint && isMoving && stamina.CanSprint)
        {
            stamina.DrainStamina(staminaDrainPerSecond * Time.deltaTime);
        }

        if (!stamina.CanSprint && starterInputs.sprint)
        {
            starterInputs.sprint = false;
        }
    }
}
