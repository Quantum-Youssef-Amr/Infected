using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoltsUpdater : MonoBehaviour
{
    [SerializeField] private shooting playerGun;
    [SerializeField] private Image boltsCounter;

    private float _animation_fill_amount;
    private Coroutine co;
    void Update()
    {
        if (playerGun._reloading)
        {
            if (co == null) co = StartCoroutine(reload());
        }
        else
        {
            boltsCounter.fillAmount = (float)(playerGun.boltsInRound - playerGun._bolts_shoot) / playerGun.boltsInRound;
        }
    }


    public IEnumerator reload()
    {
        _animation_fill_amount = boltsCounter.fillAmount;
        yield return new WaitUntil(() =>
        {
            DOTween.To(() => _animation_fill_amount, x => _animation_fill_amount = x, 1f, playerGun.reloading_Time).SetEase(Ease.Linear);
            boltsCounter.fillAmount = _animation_fill_amount;
            return _animation_fill_amount >= 0.99f;
        });
        co = null;
    }

}
