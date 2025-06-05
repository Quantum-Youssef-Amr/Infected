using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += scenechanged;
    }

    private void scenechanged(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex == 0)
            Destroy(gameObject);
    }
}
