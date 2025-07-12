using UnityEngine;


public abstract class AbilityInterface : ScriptableObject
{
    public  abstract void  ApplyAbility(Object target,float amount,bool Adder = false);
}
