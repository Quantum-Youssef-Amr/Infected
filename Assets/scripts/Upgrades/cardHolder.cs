using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class cardHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card Card;

    private TextMeshProUGUI text;
    private Button _b;

    private void Start()
    {
        _b = GetComponent<Button>();
        text = GameObject.FindGameObjectWithTag("discriptaion").transform.GetChild(0).GetComponent<TextMeshProUGUI>();


        if (PublicData.platform.PlatformType == PlatformType.Mobile)
            _b.onClick.AddListener(() =>
            {
                updateToolTip();
            });
    }

    private void updateToolTip()
    {
        text.text = Card.description;
        text.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PublicData.platform.PlatformType == PlatformType.PC)
        {
            text.text = "";
            text.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(PublicData.platform.PlatformType == PlatformType.PC)
            updateToolTip();
    }
}
