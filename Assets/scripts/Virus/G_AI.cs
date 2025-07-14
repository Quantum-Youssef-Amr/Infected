using UnityEngine;

public class G_AI : MonoBehaviour
{
    [SerializeField] protected VirusHealth health;
    [SerializeField] protected float Speed, rotationSpeed;
    [SerializeField] public float Damage;
    public float Value = 10f;
    private GameObject _player;
    private Transform _pt, _t;
    protected float _rotation;
    protected Rigidbody2D _rb;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _pt = _player.transform;
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rotation = Random.Range(-rotationSpeed, rotationSpeed);
    }

    protected Vector2 m_playerdir;

    private void Update()
    {
        m_playerdir = _pt.position - _t.position;
        Move();
    }

    public virtual void Move()
    {
        if (PublicData.pause || PublicData.gameover) return;
        _rb.AddForce(m_playerdir.normalized * Speed * Time.deltaTime * 10000f, ForceMode2D.Force);
        _rb.AddTorque(_rotation, ForceMode2D.Force);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Health>().Take_damage(Damage);
            }
        }
        catch (System.Exception)
        {

        }

        if(collision.gameObject.tag == "Player")
        {
            health.die();
        }
            
    }

}
