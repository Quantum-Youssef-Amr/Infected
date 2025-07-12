using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IMoney"), System.Serializable]
public class IMoney : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        G_AI Virus = (G_AI)target;

        Virus.Value += amount;

    }
}
