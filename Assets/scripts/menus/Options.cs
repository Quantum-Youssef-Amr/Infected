using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class Options : MonoBehaviour
{
    [SerializeField] private Image Music, sfx;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Slider MusicSlider, SfxSlider;
    [SerializeField] private Toggle Crt, HighGraphics;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Material CrtEffect;

    private float musicValue, sfxValue;
    void Start()
    {
        MusicSlider.value = PublicData.Music;
        SfxSlider.value = PublicData.Sfx;
        Crt.isOn = PublicData.CrtEffect;
        HighGraphics.isOn = PublicData.UseHighGraphics;
    }

    void Update()
    {
        changeMusicVolume(MusicSlider.value);
        Music.sprite = MusicSlider.value > -80 ? sprites[0] : sprites[1];
        changeSfxVolume(SfxSlider.value);
        sfx.sprite = SfxSlider.value > -80 ? sprites[2] : sprites[3];

        setGraphics(HighGraphics.isOn);
        ChangeCrt(Crt.isOn);

        PublicData.UseHighGraphics = HighGraphics.isOn;
        PublicData.CrtEffect = Crt.isOn;

        PublicData.Music = MusicSlider.value;
        PublicData.Sfx = SfxSlider.value;

    }

    public void ChangeCrt(bool state){
        CrtEffect.SetFloat("_UseLines", state ? 1.0f : 0.0f);
    }

    public void setGraphics(bool state){
        PublicData.UseHighGraphics = Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing = state;
    }

    public void changeMusicVolume(float new_value){
        audioMixer.SetFloat("musicV", new_value);
    }

    public void changeSfxVolume(float new_value){
        audioMixer.SetFloat("sfxV", new_value);
    }
}
