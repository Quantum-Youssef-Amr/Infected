using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class explosiveone : VirusHealth
{
    [SerializeField] private GameObject ExpoParticals;
    [SerializeField] private CircleCollider2D Explosive;
    [SerializeField] private CircleCollider2D body;
    [SerializeField] private float ExpoDamage = 10f, PlayerDamage = 10f;

    private List<Collider2D> colliders = new List<Collider2D>();
    public override void die(bool add = false)
    {
        Instantiate(DeathParticals, transform.position, Quaternion.identity);
        if (add)
        {
            PublicData.money += Mathf.Floor(GetComponent<G_AI>().Value * (transform.position.magnitude / 3));
        }


        Explosive.Overlap(colliders);
        Instantiate(ExpoParticals, transform.position, Quaternion.identity);
        foreach (Collider2D co in colliders)
        {
            if (co == null) continue;
            try
            {
                if(co.gameObject.tag == "Player")
                {
                    co.gameObject.GetComponent<Health>().Take_damage(PlayerDamage);
                    continue;
                }
                co.gameObject.GetComponent<Health>().Take_damage(ExpoDamage);
            }
            catch (System.Exception)
            {
                continue;
            }
        }

        deathSound.Play();

        if (deathSound)
            StartCoroutine(death());
        else
            Destroy(gameObject);
    }

    private IEnumerator death()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        body.enabled = false;
        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<CameraShake>().Camerashake());
        yield return new WaitUntil(() => { return !deathSound.isPlaying; });
        Destroy(gameObject);
    }
    

}
