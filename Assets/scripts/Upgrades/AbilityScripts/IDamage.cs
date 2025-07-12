using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IDamage"), System.Serializable]
public class IDamage : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        bult bult = (bult)target;

        if (Adder)
        {
            bult.Damage += amount;
        }
        else
        {
            bult.Damage *= amount;
        }

    }
}
