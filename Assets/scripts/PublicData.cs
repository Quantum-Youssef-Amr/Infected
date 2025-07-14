using System;
using System.Security;
using NUnit.Framework.Constraints;
using UnityEngine;

public struct PublicData
{
    // Genral Data
    public static float money = 0f;
    public static int waveNum = 1;

    // game state
    public static bool pause = false, upgradeing = false, gameover = false;

    // manager objects
    public static Platform platform = new Platform(false);
    public static setting setting = new setting();

    // events

    public static Action OnUpgradeBegin;
    public static Action<Card> OnUpgrade;
    public static Action OnUpgradeEnd;
    public static Action OnGameOver;
    public static Action<bool> Onpause;
}

public class setting
{
    // visuals
    public QuiltyLevels Quality;
    public bool UseHighGraphics, CrtEffect;

    //audio
    public float Music, Sfx;

    // accessbility
    public float MouseSenesitevity;
    public bool left_handed;
    
    // controls
    public Vector2[] btnsLocations;
    public float[] btnsSize;
    public Vector2 joystickLocation;

    public bool holdtofire;
    public setting()
    {
        Quality = QuiltyLevels.Med;
        UseHighGraphics = CrtEffect = true;

        Music = Sfx = 0;

        MouseSenesitevity = 2f;
        left_handed = false;

        joystickLocation = Vector2.zero;
        btnsLocations = new Vector2[2];
        btnsSize = new float[2];
        holdtofire = true;
    }
}

public struct Platform
{
    public PlatformType PlatformType;
    public Platform(bool _)
    {
        PlatformType = Application.isMobilePlatform ? PlatformType.Mobile : PlatformType.PC;
    }
}


public enum PlatformType
{
    PC = 0,
    Mobile = 1
}

public enum QuiltyLevels
{
    low = 0,
    Med = 1,
    High = 2,
    Ultra = 3
}
    