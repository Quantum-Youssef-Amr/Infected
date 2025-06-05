using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoltsUpdater : MonoBehaviour
{
    [SerializeField] private shooting playerGun;
    [SerializeField] private Image boltsCounter;

    private float _animation_fill_amount;
    void Update()
    {
        if(playerGun._reloading){
            DOTween.To(() => _animation_fill_amount, x => _animation_fill_amount = x, 1f, playerGun.reloading_Time).SetEase(Ease.Linear).OnComplete(() => {_animation_fill_amount = 0;});
            boltsCounter.fillAmount = _animation_fill_amount;
        }else{
            boltsCounter.fillAmount = (float) (playerGun.boltsInRound - playerGun._bolts_shoot) / playerGun.boltsInRound;
        }
    }

}
