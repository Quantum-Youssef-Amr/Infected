using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private int CardNum = 1, MaxCards = 4;
    [SerializeField] private List<Card> cards = new List<Card>();
    [SerializeField] private GameObject UpgradeUi, UpgradeCard, upgradeScreen, AndriodUI;
    [SerializeField] private PowerScriptLinker linker;
    private List<string> _selectedcards = new List<string>();
    private Card card;
    private Card _selected;

    void Start()
    {
        PublicData.OnUpgradeBegin += ShowUpgrades;
        PublicData.OnUpgrade += card => HideAndApply(card);
        PublicData.OnUpgradeEnd += HideUpgradeScreen;
    }

    public void ShowUpgrades()
    {
        Random.InitState(Random.Range(0, 1000000));

        if (CardNum == MaxCards)
            cards.Remove(cards.Where(card => card.Title == "one more").First());

        for (int child = 0; child < UpgradeUi.transform.childCount; child++)
            Destroy(UpgradeUi.transform.GetChild(child).gameObject);

        _selectedcards.Clear();

        for (int i = 0; i < CardNum; i++)
        {
            GameObject m_Card = Instantiate(UpgradeCard, UpgradeUi.transform);
            Card cardData = m_Card.GetComponent<cardHolder>().Card;
            chooseCard();

            // if the card is genrated twice go back and don't add it
            if (_selectedcards.Contains(card.name))
            {
                i--;
                continue;
            }

            _selectedcards.Add(card.name);

            // set up the card apperance
            m_Card.transform.GetChild(0).GetComponent<Image>().sprite = card.image;
            m_Card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.Title;
            m_Card.GetComponent<cardHolder>().Card = card;
            m_Card.GetComponent<Button>().name = card.Title;

            // what happens when the card is clicked
            m_Card.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (PublicData.platform.PlatformType == PlatformType.Mobile)
                {
                    // Mobile controls if the player tab once mark the selected card if more apply the card
                    if (_selected != null)
                    {
                        if (_selected == card)
                        {
                            PublicData.OnUpgrade?.Invoke(card);
                        }
                        else
                        {
                            _selected = card;
                        }
                    }
                    else
                    {
                        _selected = card;
                    }
                    //------------------------------------------------------------------------
                }
                else
                {
                    // Pc controls apply the card once clicked, on mouse over mark the card
                    PublicData.OnUpgrade?.Invoke(card);
                }
            });
        }
        ShowUpgradeScreen();
    }

    private void chooseCard()
    {
        card = cards[Random.Range(0, cards.Count)];
    }

    // TODO: rework this part for more costumizable system
    /* 
        done by using the Resources folder as a container for each upgrade logic script
        When making a new upgrade:
        1. make a new value in the enum power in card script
        2. make a new script and inharit it form the IUpgrade class and implament the logic
        3. make a new object from the new script and add it to the linker
    */

    public void HideAndApply(Card card)
    {
        ApplyUpgrade(card);
        PublicData.OnUpgradeEnd?.Invoke();
    }

    private void ApplyUpgrade(Card card)
    {
        // if we want a number of effects we will move this code in a loop
        IUpgrade upgrade = linker.GetScript(card.ability);
        upgrade?.applyUpgrade(card.amount, card.adder);
    }

    private void ShowUpgradeScreen()
    {
        UpgradeUi.SetActive(true);
        upgradeScreen.SetActive(true);
        if (PublicData.platform.PlatformType == PlatformType.Mobile)
            AndriodUI.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void HideUpgradeScreen()
    {
        UpgradeUi.SetActive(false);
        upgradeScreen.SetActive(false);

        if (PublicData.platform.PlatformType == PlatformType.Mobile)
            AndriodUI.SetActive(true);

        transform.GetChild(0).gameObject.SetActive(true);
        _selected = null;
    }

    public void addCard()
    {
        CardNum++;
    }
}
