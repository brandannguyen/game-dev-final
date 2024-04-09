using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HoverColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private TextMeshProUGUI text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.cyan;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.white;
    }
}