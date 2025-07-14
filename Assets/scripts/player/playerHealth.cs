using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : Health
{
    [SerializeField] private Material fullscreen;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private AudioSource deathsound;

    private float _persantage;
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        _chealth = MaxHealth;
    }

    private void Update()
    {
        HealthBar.maxValue = MaxHealth;
        _persantage = (MaxHealth - _chealth) / MaxHealth;
        _persantage = Mathf.Clamp01(_persantage);
        fullscreen.SetFloat("_offset", _persantage);

        HealthBar.value = _chealth;

        if (dead)
            die();

        if (dead)
            PublicData.gameover = true;
        else
            PublicData.gameover = false;

    }

    public override void die(bool add = false)
    {
        GameObject.FindGameObjectWithTag("musicManager").SetActive(false);
        deathsound.Play();
        StartCoroutine(GetComponent<CameraShake>().Camerashake());
        PublicData.gameover = true;
        PublicData.OnGameOver?.Invoke();
    }

    public void addhealth(float health)
    {
        _chealth += health;
    }

}
