using UnityEngine;

[CreateAssetMenu(fileName = "IGunSpeed", menuName = "Scriptable Objects/IGunSpeed")]
public class IGunSpeed : IUpgrade
{
    public override void applyUpgrade(float amount, bool addar = false)
    {
        shooting shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<shooting>();

        if (addar)
        {
            shooting.Cooldown -= amount;
            return;
        }
        shooting.Cooldown *= amount;
    }
}
