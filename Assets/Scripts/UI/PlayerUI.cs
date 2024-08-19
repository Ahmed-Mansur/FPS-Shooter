using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    
    public void SetMessage(string message)
    {
        messageText.text = message;
    }
}
