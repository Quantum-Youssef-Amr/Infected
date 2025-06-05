using UnityEngine;
using UnityEngine.SocialPlatforms;

public class simpleMouse : MonoBehaviour
{
    [SerializeField] private float Eazing;
    [SerializeField] private Vector2 offset;

    private Transform _t;
    private Camera _main;

    void Start()
    {
        if (Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _t = transform;
        _main = Camera.main;
    }

    void Update()
    {
        _t.position = Vector3.Lerp(_t.position, (Vector2)_main.ScreenToWorldPoint((Vector2)Input.mousePosition + offset), Time.deltaTime * Eazing);
    }
}
