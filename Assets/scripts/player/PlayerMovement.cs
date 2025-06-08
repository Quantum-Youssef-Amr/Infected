using DG.Tweening;
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

    float m_angel;
    Vector2 m_target;
    private void RotatePlayer(Vector2 vec){
        // if no vec return (dot of vector.zero ruslts to contant rotation)
        if(vec == Vector2.zero) return;
        else vec = vec.normalized;

        // if the target heading changed set the requird vars
        if(m_target != vec) {
            m_target = vec;
            m_angel = _t.rotation.eulerAngles.z;
        }

        // until the player point to target rotate it in the diraction of it
        // if there is a small margen -error (0.0001f)- just set the heading to the target
        if(Vector2.Dot(m_target, _t.up) < 0.9999f){
            m_angel += Mathf.Sign(Vector2.SignedAngle(_t.up, m_target)) * TurningSpeed * Time.deltaTime;
            _t.rotation = Quaternion.Euler(0, 0, m_angel);    
        }else{
            _t.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, m_target));
        }
        
    }
}
