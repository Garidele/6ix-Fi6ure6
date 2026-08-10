using UnityEngine;
using TMPro;

// Attach this to your Canvas (or any persistent UI manager object).
// Any pickup can call PickupPromptUI.Instance.Show("message") / .Hide()
// without needing its own dedicated UI element.
public class PickupPromptUI : MonoBehaviour
{
    public static PickupPromptUI Instance { get; private set; }

    [Tooltip("Drag the InteractPrompt TextMeshProUGUI element here.")]
    public TextMeshProUGUI promptText;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string message)
    {
        if (promptText == null) return;
        promptText.text = message;
        promptText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (promptText == null) return;
        promptText.gameObject.SetActive(false);
    }
}
