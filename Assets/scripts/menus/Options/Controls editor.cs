using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controlseditor : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private float Sensetavitiy = 0.01f, ZoomUpperlimit = 5f, ZoomLowerLimit = 0.5f;
    [SerializeField] private Color DeactiveColor, ActiveColor;
    private Controlslayout _controlslayout;
    private GameObject _selected_UI_element;
    private Vector2 offset;
    private bool _canMove;

    #region input
    private Input_system inputs;

    void Awake()
    {
        inputs = new Input_system();
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    #endregion

    private float _delta, _lastDistance;

    void Start()
    {
        _controlslayout = GetComponent<Controlslayout>();

        inputs.UI.Zoom.performed += delta =>
        {
            Vector2 pos1 = Input.touches[0].position;
            Vector2 pos2 = Input.touches[1].position;

            float dist = Vector2.Distance(pos1, pos2);
            if (_lastDistance == 0f)
            {
                _lastDistance = dist;
            }

            _delta = _lastDistance - dist;
            _lastDistance = dist;


            _selected_UI_element.transform.localScale -= _delta * Time.deltaTime * Sensetavitiy * Vector3.one;
            _selected_UI_element.transform.localScale = new Vector3(Mathf.Clamp(_selected_UI_element.transform.localScale.x, ZoomLowerLimit, ZoomUpperlimit),
                                                                    Mathf.Clamp(_selected_UI_element.transform.localScale.y, ZoomLowerLimit, ZoomUpperlimit),
                                                                    Mathf.Clamp(_selected_UI_element.transform.localScale.z, ZoomLowerLimit, ZoomUpperlimit));
        };
    }

    void Update()
    {
        _canMove = inputs.UI.Zoom.IsInProgress() ? false : true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_selected_UI_element != null && _canMove)
        {
            _selected_UI_element.transform.position = (Vector2)eventData.pointerCurrentRaycast.worldPosition + offset;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            return;
        }

        if (_controlslayout.getAllInputs().Contains(eventData.pointerCurrentRaycast.gameObject))
        {
            _selected_UI_element = eventData.pointerCurrentRaycast.gameObject;
        }
        offset = _selected_UI_element.transform.position - eventData.pointerCurrentRaycast.worldPosition;

        highlightSelected(_selected_UI_element);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            return;
        }

        if (_controlslayout.getAllInputs().Contains(eventData.pointerCurrentRaycast.gameObject))
        {
            _selected_UI_element = eventData.pointerCurrentRaycast.gameObject;
        }
        highlightSelected(_selected_UI_element);
    }

    private void highlightSelected(GameObject selected)
    {
        foreach (GameObject control in _controlslayout.getAllInputs())
        {
            if (control == selected)
            {
                control.GetComponent<Image>().color = ActiveColor;
                control.transform.GetChild(0).GetComponent<Image>().color = new Color(ActiveColor.r, ActiveColor.g, ActiveColor.b);
            }
            else
            {
                control.GetComponent<Image>().color = DeactiveColor;
                control.transform.GetChild(0).GetComponent<Image>().color = DeactiveColor;
            }
        }
    }
}
