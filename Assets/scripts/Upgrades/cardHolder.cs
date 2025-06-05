using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class cardHolder : MonoBehaviour
{
    public Card Card;

    public GameObject text;
    private Button _b;
    private void Start()
    {
        _b = GetComponent<Button>();
        text = GameObject.FindGameObjectWithTag("discriptaion");
        

        if (Application.isMobilePlatform)
            _b.onClick.AddListener(() =>
            {
                updateToolTip();
            });

    }

    private void OnMouseEnter()
    {
        if(!Application.isMobilePlatform)
            updateToolTip();
    }

    private void OnMouseExit()
    {
        if (!Application.isMobilePlatform)
        {
            text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            text.SetActive(false);
        }
            
    }

    private void updateToolTip()
    {
        text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Card.description;
        text.SetActive(true);
    }

}
