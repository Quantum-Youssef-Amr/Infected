using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class controler : MonoBehaviour
{
    [SerializeField] RectTransform Control, ControlNob;

    public static float vecAng, vecMag, conMag;
    public static Vector2 vec;

    private float _control_size;
    private bool _CanControl;
    public int TouchId;

    private void Start()
    {
        EnbleContol();
        _control_size = Control.rect.size.x / 2;
    }


    private void Update()
    {
        UpdateVisuals();
        if (!_CanControl)
        {
            vec = Vector2.zero;
            return;
        }
        Calculate_vec();
    }

    private void Calculate_vec()
    {
        vec = Camera.main.ScreenToWorldPoint(Input.GetTouch(TouchId).position) -  Control.position;
        vecMag = vec.magnitude;
        conMag = ControlNob.localPosition.magnitude / _control_size;
        vecAng = Vector2.SignedAngle(Vector2.up, vec);
    }

    private void UpdateVisuals()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ControlNob.localPosition = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            if(vecMag > _control_size)
            {
                vec.Normalize();
                vec *= _control_size;
            }
            ControlNob.localPosition = vec;
        }
    }

    public void setpos()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            TouchId = Input.GetTouch(i).position.magnitude < Input.GetTouch(TouchId).position.magnitude ? i : TouchId;
        }

        Control.position = Input.GetTouch(TouchId).position;
    }

    public void EnbleContol()
    {
        _CanControl = true;
    }

    public void DisableControl()
    {
        _CanControl = false;
    }

}
