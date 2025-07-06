using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controlseditor : MonoBehaviour, IDragHandler ,IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float Sensetavitiy = 0.01f, ZoomUpperlimit = 5f, ZoomLowerLimit = 0.5f;
    [SerializeField] private Color DeactiveColor, ActiveColor;
    private enum controlState
    {
        None, moveingObject, resizingObject
    }
    [SerializeField] private controlState controlstate;

    private Controlslayout _controlslayout;
    private GameObject _selected_UI_element;
    private Vector2 offset;

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

    void Start()
    {
        _controlslayout = GetComponent<Controlslayout>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            controlstate = controlState.resizingObject;
            return;
        }
        else
        {
            if (isInput(eventData))
            {
                controlstate = controlState.moveingObject;
                _selected_UI_element = eventData.pointerCurrentRaycast.gameObject;
                offset = _selected_UI_element.transform.position - eventData.pointerCurrentRaycast.worldPosition;
                highlightSelected(_selected_UI_element);
                return;
            }

            controlstate = controlState.None;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (controlstate)
        {
            case controlState.moveingObject:
                _selected_UI_element.transform.position = (Vector2)eventData.pointerCurrentRaycast.worldPosition != Vector2.zero ? (Vector2)eventData.pointerCurrentRaycast.worldPosition + offset : _selected_UI_element.transform.position;
                break;

            case controlState.resizingObject:
                _selected_UI_element.transform.localScale += eventData.delta.y * Time.deltaTime * Sensetavitiy * Vector3.one;
                _selected_UI_element.transform.localScale = new Vector3(Mathf.Clamp(_selected_UI_element.transform.localScale.x, ZoomLowerLimit, ZoomUpperlimit),
                                                                        Mathf.Clamp(_selected_UI_element.transform.localScale.y, ZoomLowerLimit, ZoomUpperlimit),
                                                                        Mathf.Clamp(_selected_UI_element.transform.localScale.z, ZoomLowerLimit, ZoomUpperlimit));
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controlstate = controlState.None;
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

    private bool isInput(PointerEventData eventData)
    {
        return _controlslayout.getAllInputs().Contains(eventData.pointerCurrentRaycast.gameObject);
    }
}
