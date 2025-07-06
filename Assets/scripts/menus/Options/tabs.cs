using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class tabs : MonoBehaviour
{
    [SerializeField] private Color ActiveColor, DeactiveColor;
    [SerializeField] private GameObject[] Tabs;
    [SerializeField] private GameObject[] contants;

    void Start()
    {
        SetUp();
        selectTab(0);
    }

    public void SetUp()
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            int index = i;
            Button btn = Tabs[i].AddComponent<Button>();
            btn.name = index.ToString();
            btn.onClick.AddListener(() => selectTab(index));
        }
    }

    private void selectTab(int index)
    {
        for (int i = 0; i < Tabs.Length; i++)
        {
            Tabs[i].GetComponent<Image>().color = i == index ? ActiveColor : DeactiveColor;
            Tabs[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = i == index ? Color.white : DeactiveColor;
            contants[i].SetActive(i == index);
        }
    }
}
