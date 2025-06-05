using UnityEngine;
using System.Collections;

public class TeleporterAI : G_AI
{
    [SerializeField] protected float TeleportCoolDown = 2f, TeleportDistance = 3f, TelepotingSpeed = 1f;
    [SerializeField] private GameObject TeleportingParticals;

    protected bool _telp = true;
    private Vector2 _dir;

    private Coroutine co;

    private void Start()
    {
        _rotation = Random.Range(-rotationSpeed, rotationSpeed);
        co = StartCoroutine(telp());
    }

    public override void Move()
    {
        if (PublicData.pause || PublicData.gameover) return;
        if (!_telp)
           base.Move();
    }

    private IEnumerator telp()
    {
        _telp = false;
        if (PublicData.pause || PublicData.gameover) yield return new WaitUntil(() => { return !PublicData.pause || !PublicData.gameover; });
        yield return new WaitForSeconds(TeleportCoolDown);
        _telp = true;

        //hide the teleporter   TODO:make it pritty
        GetComponent<SpriteRenderer>().enabled = GetComponent<CircleCollider2D>().enabled = false;

        // calculate the teleportion diraction
        _dir = Random.insideUnitCircle + (0.1f * m_playerdir) + 0.5f * Vector2.Perpendicular(Random.Range(-1, 1) * m_playerdir);
        _dir = _dir.normalized * TeleportDistance;

        //add the telelportion particals
        GameObject g = null;
        if(g == null) g = Instantiate(TeleportingParticals, transform.position, Quaternion.identity);
        transform.position += (Vector3) _dir;
        
        // teleporte
        var main = g.GetComponent<ParticleSystem>().main;
        main.duration = TelepotingSpeed / TeleportDistance;
        g.GetComponent<ParticleSystem>().Play();
        
        //add the particals and wait for it to finish
        StartCoroutine(g.GetComponent<TelelportingParticals>().MoveTowrad(transform.position, TelepotingSpeed));
        yield return new WaitForSeconds((g.transform.position - transform.position).magnitude / TelepotingSpeed);

        //return to the show state
        GetComponent<SpriteRenderer>().enabled = GetComponent<CircleCollider2D>().enabled = true;

        co = StartCoroutine(telp());
    }
}
