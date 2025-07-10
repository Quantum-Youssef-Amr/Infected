using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SingltonSceneManager : MonoBehaviour, IDataPersestant
{

    #region inputs
    private Input_system inputs;

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void Osable()
    {
        inputs.Disable();
    }

    #endregion

    void Awake()
    {
        Application.targetFrameRate = 120;
        inputs = new Input_system();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MainMenu();
    }

    public void play()
    {
        PublicData.pause = false;
        AsyncOperation async = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        async.completed += updateVisuals;
    }

    public void MainMenu(){
        PublicData.pause = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        async.completed += updateVisuals;
    }
    
    private void updateVisuals(AsyncOperation operation){
        operation.allowSceneActivation = true;
        FindAnyObjectByType<Camera>().GetUniversalAdditionalCameraData().renderPostProcessing = PublicData.setting.UseHighGraphics;
    }

    public void loadData(Data data)
    {
        PublicData.setting.Sfx = data.sound;
        PublicData.setting.Music = data.Music;

        PublicData.setting.CrtEffect = data.CRT_effect;
        PublicData.setting.UseHighGraphics = data.HighGraphics;
        PublicData.setting.Quality = (QuiltyLevels)data.Quality;

        PublicData.setting.MouseSenesitevity = data.MouseSenesitevity;
        PublicData.setting.left_handed = data.left_handed;

        PublicData.setting.btnsLocations = data.btnsLocations;
        PublicData.setting.btnsSize = data.btnsSize;
        PublicData.setting.joystickLocation = data.joystickLocation;
        PublicData.setting.holdtofire = data.holdTofire;
        
    }

    public void SaveData(ref Data data)
    {
        data.sound = PublicData.setting.Sfx;
        data.Music = PublicData.setting.Music;

        data.CRT_effect = PublicData.setting.CrtEffect;
        data.HighGraphics = PublicData.setting.UseHighGraphics;
        data.Quality = (int)PublicData.setting.Quality;

        data.MouseSenesitevity = PublicData.setting.MouseSenesitevity;
        data.left_handed = PublicData.setting.left_handed;

        data.btnsLocations = PublicData.setting.btnsLocations;
        data.btnsSize = PublicData.setting.btnsSize;
        data.joystickLocation = PublicData.setting.joystickLocation;
        data.holdTofire = PublicData.setting.holdtofire;
        
    }
}
