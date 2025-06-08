using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.UI;
using UnityEngine.EventSystems;

public class controler : MonoBehaviour, IPointerUpHandler, IDragHandler
{
    [SerializeField] RectTransform Control, ControlNob;
    [SerializeField] private float DeadZone = 0.6f;

    public static float vecAng, vecMag, conMag;
    public static float _DeadZone {get; private set;}
    public static Vector2 vec;

    private Vector2 _calcVec;
    private float _control_size;
    private bool _CanControl;
    public int TouchId = 0;

    private void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
        _DeadZone = DeadZone;
    }


    private void Update()
    {
        if(PublicData.pause || PublicData.gameover) return;
        if(PublicData.upgradeing) {gameObject.SetActive(false); return;}

        _control_size = ((Control.rect.size.x / 2) * (Camera.main.orthographicSize / 5)) / 100;
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
        _calcVec = (Vector2) Camera.main.ScreenToWorldPoint(Input.GetTouch(TouchId).position) - (Vector2) Control.position;
        if ((_calcVec.magnitude / _control_size) > DeadZone)
            vec = _calcVec;
        vecMag = vec.magnitude;
        conMag = ControlNob.localPosition.magnitude / (Control.rect.size.x/2);
        vecAng = Vector2.SignedAngle(Vector2.up, vec);
    }

    private void UpdateVisuals()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnbleContol();
            setpos();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ControlNob.localPosition = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            if(_calcVec.magnitude > _control_size)
            {
                _calcVec = _calcVec.normalized;
                _calcVec *= _control_size;
            }
            ControlNob.position = _calcVec + (Vector2) Control.position;
        }
    }

    public void setpos()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            TouchId = Input.GetTouch(i).position.magnitude < Input.GetTouch(TouchId).position.magnitude ? i : TouchId;
        }

        Vector2 t = Camera.main.ScreenToWorldPoint(Input.GetTouch(TouchId).position) - Control.position;
        if (t.magnitude < 5)
        {
            Control.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(TouchId).position);
            print(t.magnitude);
        }
        
    }

    public void EnbleContol()
    {
        _CanControl = true;
    }

    public void DisableControl()
    {
        _CanControl = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_CanControl)
        {
            EnbleContol();
            setpos();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DisableControl();
    }

}
