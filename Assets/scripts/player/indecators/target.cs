using UnityEngine;

public class target : MonoBehaviour
{
    [SerializeField] private Transform player;
    private RaycastHit2D hit2D;
    private Transform _t;
    private SpriteRenderer _sr;
    void Start()
    {
        _t = transform;
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        hit2D = Physics2D.Raycast(player.position + player.up, player.up, Mathf.Infinity, LayerMask.GetMask("virsus"));

        if (!hit2D.collider) { _sr.enabled = false; return;}

        _t.rotation = Quaternion.identity;
        _t.position = hit2D.point;
        _sr.enabled = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.position + player.up, hit2D.point);
    }
}
