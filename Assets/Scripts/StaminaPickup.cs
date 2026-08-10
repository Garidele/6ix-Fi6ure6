using UnityEngine;
using UnityEngine.InputSystem;

// Attach to the pickup item GameObject.
// The Collider acts as a PROXIMITY ZONE. Player must be in range and
// press F to collect. Collecting adds one pack to StaminaInventory -
// it does NOT restore stamina directly. Stamina is only restored later
// when the player presses E to eat a pack (see StaminaInventory.cs).
[RequireComponent(typeof(Collider))]
public class StaminaPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public bool destroyOnPickup = true;

    [Header("Interaction")]
    [Tooltip("Text shown to the player while they're in range.")]
    public string promptMessage = "Press F to pick up";

    [Header("Feedback (optional)")]
    public AudioClip pickupSound;

    private bool playerInRange = false;

    void Reset()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Collect();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (PickupPromptUI.Instance != null)
            PickupPromptUI.Instance.Show(promptMessage);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (PickupPromptUI.Instance != null)
            PickupPromptUI.Instance.Hide();
    }

    void Collect()
    {
        if (StaminaInventory.Instance != null)
            StaminaInventory.Instance.AddPack(1);

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        if (PickupPromptUI.Instance != null)
            PickupPromptUI.Instance.Hide();

        playerInRange = false;

        if (destroyOnPickup)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}