using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class LocalMouse : MonoBehaviour
{
    private Image _sr;
    #region inputs
    private Input_system inputs;

    private void Awake()
    {
        inputs = new Input_system();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    #endregion
    void Start()
    {
        if (PublicData.platform.PlatformType == PlatformType.Mobile)
        {
            gameObject.SetActive(false);
            return;
        }

        _sr = GetComponent<Image>();
        Cursor.visible = false;

        inputs.UI.MousePOS.performed += v =>
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(v.ReadValue<Vector2>());
        };
    }
    void Update()
    {
        if (inputs.UI.ControlerMousePOS.IsInProgress())
        {
            transform.position += (Vector3)inputs.UI.ControlerMousePOS.ReadValue<Vector2>() * Time.deltaTime * 5f;
            Mouse.current.position.QueueValueChange(Camera.main.WorldToScreenPoint(transform.position));
            Mouse.current.position.ApplyParameterChanges();
        }

        if (PublicData.upgradeing || PublicData.pause || PublicData.gameover)
        {
            //not playing
            _sr.enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //playing
            _sr.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
