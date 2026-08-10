using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// Attach to the SAME GameObject as StaminaSystem (e.g. PlayerCapsule).
// Picking up a StaminaPickup adds to packCount (no restore).
// Pressing E consumes one pack and restores stamina.
[RequireComponent(typeof(StaminaSystem))]
public class StaminaInventory : MonoBehaviour
{
    public static StaminaInventory Instance { get; private set; }

    [Header("Inventory")]
    [SerializeField] private int packCount = 0;
    public int PackCount => packCount;

    [Header("Eating Settings")]
    public bool fullRestorePerPack = true;
    [Tooltip("Only used if Full Restore Per Pack is unchecked.")]
    public float staminaPerPack = 50f;

    [Header("UI")]
    [Tooltip("Drag the bottom-right PackCounter TextMeshProUGUI here.")]
    public TextMeshProUGUI countText;

    [Header("Feedback (optional)")]
    public AudioClip eatSound;

    private StaminaSystem stamina;

    void Awake()
    {
        Instance = this;
        stamina = GetComponent<StaminaSystem>();
        UpdateUI();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryEatPack();
        }
    }

    /// <summary>
    /// Called by StaminaPickup when the player collects an item (F key).
    /// Does NOT restore stamina - just adds to the count.
    /// </summary>
    public void AddPack(int amount = 1)
    {
        packCount += amount;
        UpdateUI();
    }

    /// <summary>
    /// Called when the player presses E. Consumes one pack if available.
    /// </summary>
    public bool TryEatPack()
    {
        if (packCount <= 0) return false;

        packCount--;

        if (fullRestorePerPack)
            stamina.RestoreFull();
        else
            stamina.RestoreStamina(staminaPerPack);

        if (eatSound != null)
            AudioSource.PlayClipAtPoint(eatSound, transform.position);

        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        if (countText != null)
            countText.text = packCount.ToString();
    }
}
