using UnityEngine;

[CreateAssetMenu(fileName = "IPlayerSpeed", menuName = "Scriptable Objects/IPlayerSpeed")]
public class IPlayerSpeed : IUpgrade
{
    public override void applyUpgrade(float amount, bool addar = false)
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (addar)
        {
            playerMovement.TurningSpeed += amount;
            return;
        }
        playerMovement.TurningSpeed *= amount;
    }
}
