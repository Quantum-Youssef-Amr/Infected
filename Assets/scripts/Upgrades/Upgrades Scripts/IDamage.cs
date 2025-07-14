using UnityEngine;

[CreateAssetMenu(fileName = "IDamage", menuName = "Scriptable Objects/IDamage")]
public class IDamage : IUpgrade
{
    public override void applyUpgrade(float amount, bool addar)
    {
        bult bult = GameObject.FindGameObjectWithTag("Player").GetComponent<shooting>().getBult().GetComponent<bult>();

        if (addar)
        {
            bult.Damage += amount;
            return;
        }
        bult.Damage *= amount;
    }
}
