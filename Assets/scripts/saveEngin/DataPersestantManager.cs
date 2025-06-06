using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersestantManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] bool useEncription = false;

    private Data GameData;
    private List<IDataPersestant> dataPersestantsObjects;
    private Filehandler DataHandler;

    public static DataPersestantManager instace { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instace != null)
            Debug.LogError("found more then one Data Persestant Manager in the scene");

        instace = this;

        this.DataHandler = new Filehandler(Application.persistentDataPath, fileName, useEncription);
        this.dataPersestantsObjects = getAllDataPersestantObjects();

        loadGame();
    }

    public void NewGame()
    {
        this.GameData = new Data();
    }

    public void saveGame()
    {

        foreach (IDataPersestant dataPersestant in dataPersestantsObjects)
        {
            dataPersestant.SaveData(ref GameData);
        }

        DataHandler.Save(GameData);

    }


    public void loadGame()
    {
        this.GameData = DataHandler.load();

        if (this.GameData == null)
            NewGame();

        foreach (IDataPersestant dataPersestant in dataPersestantsObjects)
        {
            dataPersestant.loadData(GameData);
        }
    }

    public bool isSaved()
    {
        return DataHandler.isSaved();
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }

    private List<IDataPersestant> getAllDataPersestantObjects()
    {
        IEnumerable<IDataPersestant> dataPersestants = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None).OfType<IDataPersestant>();

        return new List<IDataPersestant>(dataPersestants);

    }

}
