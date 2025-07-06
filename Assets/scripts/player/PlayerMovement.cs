using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Tooltip("in degrees")] public float TurningSpeed;
    private Camera _main;
    private Transform _t;

    public Vector2 dir = Vector2.up;

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

    private void Osable()
    {
        inputs.Disable();
    }

    #endregion
    private void Start()
    {
        _main = Camera.main;
        _t = transform;
        inputs.Player.Move.performed += v =>
        {
            if (PublicData.pause || PublicData.gameover || PublicData.upgradeing) return;
            dir = PublicData.setting.left_handed ? new Vector2(-v.ReadValue<Vector2>().x, v.ReadValue<Vector2>().y) : v.ReadValue<Vector2>();
        };

        inputs.Player.PCmove.performed += v =>
        {
            if (PublicData.pause || PublicData.gameover || PublicData.upgradeing) return;
            dir = Quaternion.Euler(0, 0, -v.ReadValue<Vector2>().normalized.x * Time.deltaTime * TurningSpeed * PublicData.setting.MouseSenesitevity) * dir;
        };
    }

    private void FixedUpdate()
    {
        if (PublicData.pause || PublicData.gameover || PublicData.upgradeing) return;
        RotatePlayer(dir);
    }

    private void RotatePlayer(Vector2 vec)
    {
        if (vec == Vector2.zero) return;
        else vec = vec.normalized;

        _t.rotation = Quaternion.RotateTowards(_t.rotation, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, vec)), TurningSpeed * Time.deltaTime); ;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, 2 * dir);
    }

}
