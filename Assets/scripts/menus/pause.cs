using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class pause : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu, gameOver;
    [SerializeField] private playerHealth PlayerHealth;
    [SerializeField] private GameObject DeathParticals, d2;
    [SerializeField] private GameObject div;
    private bool dead;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PublicData.gameover)
        {
            pauseButton();
        }

        if (PlayerHealth.dead)
        {
            PublicData.gameover = true;

            if(dead == false)
                StartCoroutine(deathSec());

            dead = true;
        }
    }

    public void pauseButton()
    {
        PublicData.pause = !PublicData.pause;
        PauseMenu.SetActive(!PauseMenu.activeSelf);
    }

    public void restart()
    {
        PublicData.pause = false;
        PublicData.gameover = false;
        PublicData.upgradeing = false;
        PublicData.money = 0f;

        AsyncOperation scene = SceneManager.LoadSceneAsync(1);
        if (scene.isDone)
        {
            scene.allowSceneActivation = true;
            GameObject.FindGameObjectWithTag("musicManager").GetComponent<AudioSource>().Play();
        }
    }


    public void exit()
    {
        PublicData.pause = false;
        PublicData.gameover = false;
        PublicData.upgradeing = false;
        PublicData.money = 0f;

        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SingltonSceneManager>().MainMenu();
    }

    private IEnumerator deathSec()
    {

        Instantiate(d2, Vector3.zero, Quaternion.identity);
        Instantiate(DeathParticals, Vector3.zero, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.2f);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        shooting.Killed = 0;
        gameOver.SetActive(true);
    }

}
