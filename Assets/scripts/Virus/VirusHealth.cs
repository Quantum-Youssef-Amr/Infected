using UnityEngine;
using System.Collections;

public class VirusHealth : Health
{
    [SerializeField] protected GameObject DeathParticals;
    [SerializeField] protected AudioSource deathSound;

    private bool _d;

    private void Start()
    {
        MaxHealth += (PublicData.waveNum + 1) * MaxHealth * 0.25f;
        _chealth = MaxHealth;
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            if (!_d)
            {
                shooting.Killed++;
                die(true);
            }
                
            _d = true;
        }
    }

    public override void die(bool add = false)
    {
        Instantiate(DeathParticals, transform.position, Quaternion.identity);
        if (add)
        {
            PublicData.money += Mathf.Floor(GetComponent<G_AI>().Value * (transform.position.magnitude / 3));
        }

        deathSound.Play();

        if (deathSound)
            StartCoroutine(sound());
        else
            Destroy(gameObject);
    }


    protected IEnumerator sound()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitUntil(() => { return !deathSound.isPlaying; });
        Destroy(gameObject);
    }

}
