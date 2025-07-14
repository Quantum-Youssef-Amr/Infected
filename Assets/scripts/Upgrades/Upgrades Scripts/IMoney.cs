using UnityEngine;

[CreateAssetMenu(fileName = "IMoney", menuName = "Scriptable Objects/IMoney")]
public class IMoney : IUpgrade
{
    [SerializeField] private GameObject mainVires;
    public override void applyUpgrade(float amount, bool addar = false)
    {
        G_AI g_AI = mainVires.GetComponent<G_AI>();
        if (addar)
        {
            g_AI.Value += amount;
            return;
        }
        g_AI.Value *= amount;
    }
}
