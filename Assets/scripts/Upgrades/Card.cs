using UnityEngine;

[CreateAssetMenu(fileName = "card", menuName ="card"), System.Serializable]
public class Card : ScriptableObject
{
    public string Title;
    public Sprite image;
    [TextArea()] public string description;

    public enum power { damage, PlayerSpeed, GunSpeed, money, health, MaxHealth, upgrade, zoom, bolts}
    public power ablte;
    public float amount;
    public bool adder = false;

}

