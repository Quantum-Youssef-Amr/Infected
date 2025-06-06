using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Material fullscreen;
    [SerializeField] private Slider loading;
    [SerializeField] private ParticleSystem viruses;
    [SerializeField] private GameObject splashScren, gMainMenu, Options, cridets;
    [SerializeField] private AudioSource powerup;
    [SerializeField] private TextMeshProUGUI version;
    
    private AudioSource _musicSource;

    private void Start()
    {
        version.text = $"v{Application.version}";

        fullscreen.SetFloat("_offset", 0.2f);
        StartCoroutine(GameStart());
        _musicSource = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !splashScren.activeSelf)
        {
            back();
        }
    }


    public void Play()
    {
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SingltonSceneManager>().play();
    }

    public void cridet()
    {
        cridets.SetActive(true);
        gMainMenu.SetActive(false);
    }

    public void ONOptions(){
        Options.SetActive(true);
        gMainMenu.SetActive(false);
    }

    public void back()
    {
        cridets.SetActive(false);
        Options.SetActive(false);
        gMainMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator GameStart()
    {
        loading.value = 10f;
        yield return new WaitForSeconds(0.5f);

        loading.value = 60f;
        yield return new WaitForSeconds(1f);

        loading.value = 90f;
        yield return new WaitForSeconds(0.5f);

        loading.value = 100f;
        yield return new WaitForSeconds(1f);
        StartCoroutine(SplashScreen());
    }

    public IEnumerator SplashScreen()
    {
        powerup.Stop();
        loading.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        splashScren.SetActive(false);
        viruses.Play();
        _musicSource.Play();
        yield return new WaitForSecondsRealtime(1f);
        gMainMenu.SetActive(true);
    }

}
