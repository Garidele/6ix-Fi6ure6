using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;

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

    public void DrainStamina(float amount)
    {
        if (amount <= 0f) return;
        currentStamina = Mathf.Clamp(currentStamina - amount, 0f, maxStamina);
        UpdateUI();
    }

    public void RestoreStamina(float amount)
    {
        if (amount <= 0f) return;
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        UpdateUI();
    }

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
