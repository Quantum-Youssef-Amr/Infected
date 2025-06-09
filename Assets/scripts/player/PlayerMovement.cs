using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Tooltip("in degrees")] public float TurningSpeed;
    private Camera _main;
    private Transform _t;

    private void Start()
    {
        _main = Camera.main;
        _t = transform;
    }

    private void FixedUpdate()
    {
        if (PublicData.pause || PublicData.gameover || PublicData.upgradeing) return;

        if (Application.isMobilePlatform)
        {
            RotatePlayer(controler.vecMag > controler._DeadZone ? controler.vec : Vector2.zero);
        }
        else
        {
            RotatePlayer(_main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void RotatePlayer(Vector2 vec){
        if(vec == Vector2.zero) return;
        else vec = vec.normalized;

        _t.rotation = Quaternion.RotateTowards(_t.rotation, Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up, vec)), TurningSpeed * Time.deltaTime);;
    }
}
