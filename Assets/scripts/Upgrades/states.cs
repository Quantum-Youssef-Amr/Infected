using UnityEngine;
using TMPro;
public class states : MonoBehaviour
{
    [SerializeField] private playerHealth health;
    [SerializeField] private GameObject bult;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private shooting shoot;
    [SerializeField] private TextMeshProUGUI s1, s2, s3,s4;

    private void Update()
    {
        if (PublicData.upgradeing)
        {
            s1.text = $"{health._chealth} / {health.MaxHealth}";
            s2.text = $"{bult.GetComponent<bult>().Damage} hp";
            s3.text = $"{movement.TurningSpeed} deg/s";
            s4.text = $"{shoot.Rate} r/s";
        }
    }



}
