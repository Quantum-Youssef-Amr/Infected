using UnityEngine;
public abstract class IUpgrade : ScriptableObject
{
    public abstract void applyUpgrade(float amount, bool addar = false);
}
