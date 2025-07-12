using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Health"), System.Serializable]
public class IHealth : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        playerHealth health = (playerHealth)target;

        if (Adder)
        {
            health.addhealth(amount);
        }
        else
        {
            health.MaxHealth *= amount;
        }

    }
}
