using UnityEngine;

public class slimeHealth : VirusHealth
{
    [SerializeField] protected GameObject ChildVireses;
    [SerializeField] protected int Count = 2;
    [SerializeField] protected float SpownDestance = 0.5f;

    private Vector2 r;
    public override void die(bool add = false)
    {
        base.die(add);
        
        for (int i = 0; i < Count; i++)
        {
            if (i % 2 == 0)
            {
                r = Random.insideUnitCircle;
                r.Normalize();
            }
            GameObject b = Instantiate(ChildVireses,(Vector2) transform.position + r * SpownDestance * (i%2==0?1:-1), Quaternion.identity);
        }
    }

}
