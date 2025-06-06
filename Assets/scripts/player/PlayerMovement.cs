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
            RotatePlayer(controler.vec);
        }
        else
        {
            RotatePlayer(_main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    float _cangel;
    private void RotatePlayer(Vector2 vec){
        if(vec == Vector2.zero) return;
        if(Vector2.Dot(_t.up.normalized, vec.normalized) <= 0.9999){
            _cangel += Mathf.Sign(Vector2.SignedAngle(_t.up, vec)) * TurningSpeed;
            _t.rotation = Quaternion.Euler(0,0, _cangel);
        }else{
            _t.rotation = Quaternion.Euler(0,0,Vector2.SignedAngle(Vector2.up, vec));
        }
    }
}
