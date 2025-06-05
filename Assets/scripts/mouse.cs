using UnityEngine;
using UnityEngine.UI;

public class mouse : MonoBehaviour
{
    [SerializeField] private float Eazing;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Spowner Spowner;

    private Transform _t;
    private Camera _main;
    private Image Image;
    private Transform _pt;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _pt = GameObject.FindGameObjectWithTag("Player").transform;
        _t = transform;
        _main = Camera.main;
        Image = GetComponent<Image>();
    }


    private void FixedUpdate()
    {
        if (Application.isMobilePlatform)
        {
            _t.position = _pt.transform.up * 3;
            Image.sprite = sprites[0];
            gameObject.SetActive(!PublicData.gameover || !PublicData.pause || !PublicData.upgradeing);
            gameObject.transform.SetSiblingIndex(1);
        }
        else
        {
            _t.position = Vector3.Lerp(_t.position, (Vector2)_main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * Eazing);
            Image.sprite = PublicData.upgradeing || PublicData.pause || PublicData.gameover ? sprites[1] : sprites[0];
        }
    }
}
