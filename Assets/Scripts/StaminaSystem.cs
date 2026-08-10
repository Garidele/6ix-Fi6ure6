using UnityEngine;
using UnityEngine.UI;

// Attach this to the Player GameObject.
// Holds stamina state and updates the UI bar.
// IMPORTANT: There is no passive regeneration anywhere in this script.
// Stamina only goes up when RestoreStamina() or RestoreFull() is called
// externally (by StaminaPickup.cs when the player collects an item).
public class StaminaSystem : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    [SerializeField] private float currentStamina;

    [Tooltip("How much stamina is drained per second while the drain source (e.g. sprinting) is active.")]
    public float drainRate = 20f;

    [Header("UI")]
    [Tooltip("Drag the StaminaBar Slider here.")]
    public Slider staminaSlider;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    // True as long as there's any stamina left to spend.
    public bool CanSprint => currentStamina > 0f;

    void Awake()
    {
        currentStamina = maxStamina;
        UpdateUI();
    }

    /// <summary>
    /// Call this every frame (scaled by Time.deltaTime) while the player
    /// is doing whatever costs stamina (sprinting, dodging, etc.)
    /// </summary>
    public void DrainStamina(float amount)
    {
        if (amount <= 0f) return;
        currentStamina = Mathf.Clamp(currentStamina - amount, 0f, maxStamina);
        UpdateUI();
    }

    /// <summary>
    /// Called ONLY by pickup items. Adds a specific amount.
    /// </summary>
    public void RestoreStamina(float amount)
    {
        if (amount <= 0f) return;
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        UpdateUI();
    }

    /// <summary>
    /// Called ONLY by pickup items. Fully refills the bar.
    /// </summary>
    public void RestoreFull()
    {
        currentStamina = maxStamina;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }
}
