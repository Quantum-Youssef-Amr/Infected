using UnityEngine;

[CreateAssetMenu(fileName = "card", menuName = "card"), System.Serializable]
public class Card : ScriptableObject
{
    public string Title;
    public Sprite image;
    [TextArea()] public string description;

    public enum power
    {
        Damage,
        PlayerSpeed,
        GunSpeed,
        Money,
        Health,
        MaxHealth,
        Upgrade,
        Zoom,
        Bolts,
        BoltsSpeed
    }

    //this can be a list to apply more effect in the same time ( move it in a class first )
    public power ability;
    public float amount;
    public bool adder = false;
    //------

}

