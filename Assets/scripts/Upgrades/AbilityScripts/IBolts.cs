using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IBolts"), System.Serializable]
public class IBolts : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        shooting shoot = (shooting)target;

        shoot.bolts += (int)amount * shoot.boltsInRound;

    }
}
