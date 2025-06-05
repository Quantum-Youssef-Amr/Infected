using UnityEngine;

public class sbakyhealth : VirusHealth
{
    [SerializeField] private GameObject[] GunPoints;
    [SerializeField] private GameObject bullet;

    public override void die(bool add = false)
    {
        if (PublicData.pause) return;

        Instantiate(DeathParticals, transform.position, Quaternion.identity);
        if (add)
        {
            PublicData.money += Mathf.Floor(GetComponent<G_AI>().Value * (transform.position.magnitude / 3));
        }

        for (int i = 0; i < GunPoints.Length; i++)
        {
            GameObject b = Instantiate(bullet ,GunPoints[i].transform.position, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, GunPoints[i].transform.position - transform.position)));
        }

        deathSound.Play();

        if (deathSound)
            StartCoroutine(sound());
        else
            Destroy(gameObject);
    }

}
