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
        if(Vector2.Dot(_t.up.normalized, vec.normalized) <= 0.999){
            _cangel += Mathf.Sign(Vector2.SignedAngle(_t.up, vec)) * TurningSpeed;
            _t.rotation = Quaternion.Euler(0,0, _cangel);
        }
    }

    // for movements in the future -^\(-_-)/^-
    private Vector2 getInput(){
        float x_axis = (Input.GetKey(KeyCode.D) ? 1:0) - (Input.GetKey(KeyCode.A) ? 1:0);
        float y_axis = (Input.GetKey(KeyCode.W) ? 1:0) - (Input.GetKey(KeyCode.S) ? 1:0);
        return new Vector2(x_axis, y_axis).normalized;
    }
}
