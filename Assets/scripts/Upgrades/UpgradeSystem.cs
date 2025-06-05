using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private int CardNum = 1, MaxCards = 4;
    [SerializeField] private List<Card> cards = new List<Card>();
    [SerializeField] private GameObject UpgradeUi, UpgradeCard, upgradeScreen;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bult;
    [SerializeField] private Spowner spowner;
    [SerializeField] private G_AI ViursAI; 

    private List<string> _selectedcards = new List<string>();
    private playerHealth _ph;
    private PlayerMovement _pm;
    private shooting _pg;
    private bult _bd;
    private Card card;
    public Card _selected;

    private void Start()
    {
        _ph = player.GetComponent<playerHealth>();
        _pg = player.GetComponent<shooting>();
        _pm = player.GetComponent<PlayerMovement>();
        _bd = bult.GetComponent<bult>();
        GameObject.FindGameObjectWithTag("discriptaion").GetComponent<TextMeshProUGUI>().text = "";
    }


    public void ShowUpgrades()
    {
        if(CardNum == MaxCards)
            foreach(Card c in cards)
            {
                if (c.Title == "one more")
                {
                    cards.Remove(c);
                    break;
                }
            }

        for (int i = 0; i < UpgradeUi.transform.childCount; i++)
        {
            Destroy(UpgradeUi.transform.GetChild(i).gameObject);
        }

        _selectedcards.Clear();

        for (int i = 0; i < CardNum; i++)
        {
            GameObject m_Card = Instantiate(UpgradeCard, UpgradeUi.transform);
            chooseCard();

            while (_selectedcards.Contains(card.description))
            {
                chooseCard();
            }

            _selectedcards.Add(card.description);

            m_Card.transform.GetChild(0).GetComponent<Image>().sprite = card.image;
            m_Card.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = card.Title;
            m_Card.GetComponent<cardHolder>().Card = card;
            m_Card.GetComponent<Button>().name = card.Title;

            m_Card.GetComponent<Button>().onClick.AddListener(() => {
                if (Application.isMobilePlatform)
                {
                    if(_selected != null)
                    {
                        if (_selected.name == m_Card.GetComponent<cardHolder>().Card.name)
                        {
                            HideAndApply(m_Card.GetComponent<cardHolder>().Card);
                            PublicData.upgradeing = false;
                        }
                        else
                        {
                            _selected = m_Card.GetComponent<cardHolder>().Card;
                        }
                    }
                    else
                    {
                        _selected = m_Card.GetComponent<cardHolder>().Card;
                    }
                }
                else
                {
                    HideAndApply(m_Card.GetComponent<cardHolder>().Card);
                    PublicData.upgradeing = false;
                }
            });

        }
        UpgradeUi.SetActive(true);
        upgradeScreen.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void chooseCard()
    {
        card = cards[Random.Range(0, cards.Count)];
    }

    // TODO: rework this part for more costumizable system
    public void HideAndApply(Card card)
    {
        Card m_card = card;
        UpgradeUi.SetActive(false);

        switch (m_card.ablte)
        {
            case Card.power.damage:
                switch(m_card.adder)
                {
                    case true: _bd.Damage += m_card.amount; break;
                    case false: _bd.Damage *= m_card.amount; break;
                }
                break;

            case Card.power.GunSpeed:
                switch (m_card.adder)
                {
                    case true: _pg.Cooldown -= m_card.amount; break;
                    case false: _pg.Cooldown /= m_card.amount; break;
                }
                break;

            case Card.power.PlayerSpeed:
                switch (m_card.adder)
                {
                    case true: _pm.TurningSpeed -= m_card.amount;   break;
                    case false: _pm.TurningSpeed /= m_card.amount;  break;
                }
                break;


            case Card.power.money:
                ViursAI.Value *= m_card.amount;
                break;

            case Card.power.health:
                switch (m_card.adder)
                {
                    case true: _ph.addhealth(m_card.amount);    break;
                    case false: _ph.MaxHealth *= m_card.amount; break;
                }
                break;

            case Card.power.upgrade:
                CardNum++;
                break;

            case Card.power.zoom:
                Camera.main.orthographicSize *= m_card.amount;
                break;

            case Card.power.MaxHealth:  _ph.MaxHealth += m_card.amount; break;
            case Card.power.bolts: _pg.bolts += 2 * _pg.boltsInRound;   break;
        }

        upgradeScreen.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        _selected = null;
    }

}
