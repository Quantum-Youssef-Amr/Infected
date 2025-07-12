using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/PlayerSpeed"), System.Serializable]
public class IPlayerSpeed : AbilityInterface
{
    public override void ApplyAbility(Object target,float amount, bool Adder = false)
    {
        PlayerMovement movement = (PlayerMovement)target;

        if (Adder)
        {
            movement.TurningSpeed += amount;
        }
        else
        {
            movement.TurningSpeed *= amount;
        }

    }
}
