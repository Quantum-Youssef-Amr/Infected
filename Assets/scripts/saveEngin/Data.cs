using UnityEngine;


public class Data{

    public float Music, sound, MouseSenesitevity;
    public bool CRT_effect = true, HighGraphics =  true, left_handed;
    public int Quality;
    public Vector2 joystickLocation;
    public Vector2[] btnsLocations;
    public float[] btnsSize;


    public Data()
    {
        Quality = 4;
        CRT_effect = HighGraphics = true;

        Music = sound = 0f;
        
        left_handed = false;
        MouseSenesitevity = 3f;

        joystickLocation = new Vector2(-340, -193);
        btnsLocations = new Vector2[] {
            new Vector2(291, -258),
            new Vector2(194, -129)
        };

        btnsSize = new float[]
        {
            1,
            3
        };
    }

}