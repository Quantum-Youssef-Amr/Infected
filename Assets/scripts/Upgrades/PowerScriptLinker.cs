using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerScriptLinker", menuName = "Scriptable Objects/PowerScriptLinker")]
public class PowerScriptLinker : ScriptableObject
{
    public List<link> links = new List<link>();

    private Dictionary<Card.power, IUpgrade> powersToScript;

    void Awake()
    {
        powersToScript = new Dictionary<Card.power, IUpgrade>();
        foreach (link link in links)
        {
            if (!powersToScript.ContainsKey(link.power))
            {
                powersToScript.Add(link.power, link.Script);
            }
        }
    }

    public IUpgrade GetScript(Card.power power)
    {
        return powersToScript[power];
    }
}
[Serializable]
public class link
{
    public Card.power power;
    public IUpgrade Script;
}