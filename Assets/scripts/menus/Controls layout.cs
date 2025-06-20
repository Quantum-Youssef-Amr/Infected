using UnityEngine;

public class Controlslayout : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject[] btns;

    void Start()
    {
        if (PublicData.platform.PlatformType == PlatformType.PC)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.localScale = PublicData.setting.left_handed ? new Vector3(-1, 1, 1) : Vector3.one;

        joystick.transform.localPosition = PublicData.setting.joystickLocation;

        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].transform.localPosition = PublicData.setting.btnsLocations[i];
            btns[i].transform.localScale = PublicData.setting.btnsSize[i] * Vector3.one;
        }
    }
}
