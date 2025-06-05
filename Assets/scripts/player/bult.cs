using UnityEngine;

public class bult : MonoBehaviour
{
    [SerializeField] public float Damage, speed, DespownAfter;


    private Rigidbody2D _rb;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        DespownAfter = Camera.main.orthographicSize;
        _rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (_timer < DespownAfter) _timer += Time.deltaTime;
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<Health>().Take_damage(Damage);
        }
        catch (System.Exception)
        {
            
        }
        Destroy(gameObject);
    }

}
