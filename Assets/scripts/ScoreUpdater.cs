using UnityEngine;
using DG.Tweening;
using TMPro;

public class moneyUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;

    private float _cMoney = 0;

    private void Update()
    {
        DOTween.To(() => _cMoney, x => _cMoney = x, PublicData.money, (PublicData.money+1)/2).SetEase(Ease.Linear).SetSpeedBased(true);
        MoneyText.text =((int) _cMoney).ToString();
    }
}
