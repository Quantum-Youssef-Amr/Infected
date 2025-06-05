using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float MaxHealth;
    [SerializeField] public AudioSource Hurt;
    public bool dead { get; private set; }

    public float _chealth;

    private void Start()
    {
        _chealth = MaxHealth;
    }

    public void Take_damage(float damage)
    {
        _chealth -= damage;
        if (Hurt) Hurt.Play();

        if (_chealth <= 0)
        {
            dead = true;
        }
    }

    public virtual void die(bool add = false)
    {
        Destroy(gameObject);
    }

}
