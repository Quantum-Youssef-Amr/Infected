using UnityEngine;

public class Facing : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    private Transform _t;

    void Start()
    {
        _t = transform;
    }
    void Update()
    {
        float angle = Vector2.SignedAngle(Vector2.up, player.dir);
        transform.position = new Vector3(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
