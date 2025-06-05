using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SingltonSceneManager : MonoBehaviour, IDataPersestant
{

    void Start()
    {
       loadMainMenu();
    }

    public void play(){
        SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        AsyncOperation async = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        async.completed += updateVisuals;
    }

    public void MainMenu(){
        SceneManager.UnloadSceneAsync(2, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        loadMainMenu();
    }

    private void loadMainMenu(){
        AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        async.completed += updateVisuals;
    }
    private void updateVisuals(AsyncOperation operation){
        operation.allowSceneActivation = true;
        FindAnyObjectByType<Camera>().GetUniversalAdditionalCameraData().renderPostProcessing = PublicData.UseHighGraphics;
    }

    public void loadData(Data data)
    {
        PublicData.Sfx = data.sound;
        PublicData.Music = data.Music;
        PublicData.CrtEffect = data.CRT_effect;
        PublicData.UseHighGraphics = data.HighGraphics;
    }

    public void SaveData(ref Data data)
    {
        data.sound = PublicData.Sfx;
        data.Music = PublicData.Music;
        data.CRT_effect = PublicData.CrtEffect;
        data.HighGraphics = PublicData.UseHighGraphics;
    }
}
