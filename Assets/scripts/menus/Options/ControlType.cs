using UnityEngine;

public class ControlType : MonoBehaviour
{
    [SerializeField] private GameObject PcControls, MobileControls;

    void Start()
    {
        PcControls.SetActive(PublicData.platform.PlatformType == PlatformType.PC);
        MobileControls.SetActive(PublicData.platform.PlatformType == PlatformType.Mobile);
    }
}
