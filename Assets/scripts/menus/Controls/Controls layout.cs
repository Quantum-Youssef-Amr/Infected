using System.Collections.Generic;
using UnityEngine;

public class Controlslayout : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject[] btns;

    void Start()
    {
        setUp();
    }

    public void setUp()
    {
        switch (PublicData.platform.PlatformType)
        {
            case PlatformType.PC:
                gameObject.SetActive(false);
                break;

            case PlatformType.Mobile:
                transform.localScale = PublicData.setting.left_handed ? new Vector3(-1, 1, 1) : Vector3.one;

                joystick.transform.localPosition = PublicData.setting.joystickLocation;

                for (int i = 0; i < btns.Length; i++)
                {
                    btns[i].transform.localPosition = PublicData.setting.btnsLocations[i];
                    btns[i].transform.localScale = PublicData.setting.btnsSize[i] * Vector3.one;
                }
                break;
        }
    }

    public (Vector2, Vector2[]) getLocations()
    {
        List<Vector2> locs = new List<Vector2>();

        for (int btn = 0; btn < btns.Length; btn++)
        {
            locs.Add(btns[btn].transform.localPosition);
        }

        return (joystick.transform.localPosition, locs.ToArray());
    }

    public float[] getSizes()
    {
        List<float> Sizes = new List<float>();

        for (int btn = 0; btn < btns.Length; btn++)
        {
            Sizes.Add(btns[btn].transform.localScale.x);
        }
        return Sizes.ToArray();
    }

    public List<GameObject> getAllInputs()
    {
        List<GameObject> _objects = new List<GameObject> { joystick };
        for (int i = 0; i < btns.Length; i++)
        {
            _objects.Add(btns[i]);
        }
        return _objects;
    }
}
