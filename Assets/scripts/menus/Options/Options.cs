using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject OptionsPanal;
    [SerializeField] private GameObject ControlsInstance;

    [Header("Audio")]
    [SerializeField] private Image Music;
    [SerializeField] private Image sfx;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Slider MusicSlider, SfxSlider;

    [Space(8), Header("Graphics")]
    [SerializeField] private Slider QuiltySlider;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private Toggle Crt, HighGraphics;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Material CrtEffect;

    [Space(8), Header("accessbility")]
    [SerializeField] private Slider MouseSensetivitySlider;
    [SerializeField] private Toggle Lefthanded;

    [Space(8), Header("Controls")]
    [SerializeField] private Toggle holdtofire;

    void Start()
    {
        //Audio
        MusicSlider.value = PublicData.setting.Music;
        Music.sprite = MusicSlider.value > -80 ? sprites[0] : sprites[1];
        changeMusicVolume(PublicData.setting.Music);

        SfxSlider.value = PublicData.setting.Sfx;
        sfx.sprite = SfxSlider.value > -80 ? sprites[2] : sprites[3];
        changeSfxVolume(PublicData.setting.Sfx);

        MusicSlider.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.Music = MusicSlider.value;
            Music.sprite = MusicSlider.value > -80 ? sprites[0] : sprites[1];
            changeMusicVolume(PublicData.setting.Music);
        });

        SfxSlider.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.Sfx = SfxSlider.value;
            sfx.sprite = SfxSlider.value > -80 ? sprites[2] : sprites[3];
            changeSfxVolume(PublicData.setting.Sfx);
        });


        //Graphics
        QuiltySlider.value = (int)PublicData.setting.Quality;
        QualitySettings.SetQualityLevel((int)PublicData.setting.Quality);
        LevelText.text = PublicData.setting.Quality.ToString();

        Crt.isOn = PublicData.setting.CrtEffect;
        HighGraphics.isOn = PublicData.setting.UseHighGraphics;

        Crt.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.CrtEffect = Crt.isOn;
            ChangeCrt(PublicData.setting.CrtEffect);
        });

        HighGraphics.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.UseHighGraphics = HighGraphics.isOn;
            setGraphics(PublicData.setting.UseHighGraphics);
        });

        QuiltySlider.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.Quality = (QuiltyLevels)QuiltySlider.value;
            LevelText.text = PublicData.setting.Quality.ToString();
            QualitySettings.SetQualityLevel((int)PublicData.setting.Quality);
        });


        //accessbility
        MouseSensetivitySlider.value = PublicData.setting.MouseSenesitevity;
        Lefthanded.isOn = PublicData.setting.left_handed;

        MouseSensetivitySlider.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.MouseSenesitevity = MouseSensetivitySlider.value;
        });

        Lefthanded.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.left_handed = Lefthanded.isOn;
        });

        //controls
        holdtofire.isOn = PublicData.setting.holdtofire;

        holdtofire.onValueChanged.AddListener((_) =>
        {
            PublicData.setting.holdtofire = holdtofire.isOn;
        });
    }

    public void EditControls()
    {
        OptionsPanal.SetActive(false);
        ControlsInstance.SetActive(true);

        ControlsInstance.GetComponent<Controlslayout>().setUp();
        
    }

    public void SaveControlsEdit()
    {
        Controlslayout _cl = ControlsInstance.GetComponent<Controlslayout>();

        PublicData.setting.joystickLocation = _cl.getLocations().Item1;
        PublicData.setting.btnsLocations = _cl.getLocations().Item2;

        PublicData.setting.btnsSize = _cl.getSizes();

        ControlsInstance.SetActive(false);
        OptionsPanal.SetActive(true);
    } 

    public void ChangeCrt(bool state)
    {
        CrtEffect.SetFloat("_UseLines", state ? 1.0f : 0.0f);
    }

    public void setGraphics(bool state){
        PublicData.setting.UseHighGraphics = Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = state;
    }

    public void changeMusicVolume(float new_value){
        audioMixer.SetFloat("musicV", new_value);
    }

    public void changeSfxVolume(float new_value){
        audioMixer.SetFloat("sfxV", new_value);
    }
}
