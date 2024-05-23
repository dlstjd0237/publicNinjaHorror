using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText = null;
    public void UpdateText(string prompt)
    {
        _promptText.text = prompt;
    }
}