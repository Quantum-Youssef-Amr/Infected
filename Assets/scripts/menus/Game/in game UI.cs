using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Wave;
    [SerializeField] private Slider prograss;
    [SerializeField] private Spowner spowner;
    private float _cMoney = 0;

    private void Update()
    {
        DOTween.To(() => _cMoney, x => _cMoney = x, PublicData.money, (PublicData.money + 1) / 2).SetEase(Ease.Linear).SetSpeedBased(true);
        Score.text = ((int)_cMoney).ToString();

        Wave.text = $"Wave {PublicData.waveNum}";
        prograss.value = spowner.prograss;
    }
}
