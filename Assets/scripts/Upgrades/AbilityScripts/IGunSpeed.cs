using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IGunSpeed"), System.Serializable]
public class IGunSpeed : AbilityInterface
{
    public override void ApplyAbility(Object target,  float amount, bool Adder = false)
    {
        shooting shoot = (shooting)target;

        if (Adder)
        {
            shoot.Cooldown -= amount;
        }
        else
        {
            shoot.Cooldown /= amount;
        }

    }
}
