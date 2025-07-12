using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/IZoom"), System.Serializable]
public class IZoom : AbilityInterface
{
    public override void ApplyAbility(Object target, float amount, bool Adder = false)
    {
        Camera cam = (Camera)target;

        cam.orthographicSize += amount;

    }
}
