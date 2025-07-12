using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IMaxHealth"), System.Serializable]
public class IMaxHealth : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        playerHealth health = (playerHealth)target;

        health.MaxHealth += amount;
       
    }
}
