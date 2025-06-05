using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Text;
using UnityEngine;


public class Filehandler{

    private string DataPath;
    private string DataSaveName;
    private bool UseEncryption = false;

    private readonly string Key = "OrPitaStudiosCEOIsTheSoloDiv";

    public Filehandler(string DataPath, string DataSaveName, bool UseEncryption = true){
        this.DataPath = DataPath;
        this.DataSaveName = DataSaveName;
        this.UseEncryption = UseEncryption;
    }

    public Data load(){
        string fullpath = Path.Combine(DataPath, DataSaveName);
        Data loadedData = null;
        if(File.Exists(fullpath)){
            try{
                string datatoload = "";
                using(FileStream stream = new FileStream(fullpath, FileMode.Open)){
                    using(StreamReader reader = new StreamReader(stream)){
                        datatoload = reader.ReadToEnd();
                    }
                }

                if(UseEncryption)
                    datatoload = Encrept(datatoload);

                loadedData = JsonUtility.FromJson<Data>(datatoload);
            }
            catch(Exception){}
        }
        return loadedData;
    }


    public void Save(Data data){
        string fullpath = Path.Combine(DataPath, DataSaveName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            string datatoStore = JsonUtility.ToJson(data,true);

            if(UseEncryption)
                datatoStore = Encrept(datatoStore);
            
            using(FileStream stream = new FileStream(fullpath, FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(datatoStore);
                }
            }

        }
        catch (Exception){}
    }

    public bool isSaved(){
        if(File.Exists(Path.Combine(DataPath, DataSaveName)))
            return true;
        return false;
    }

    private string Encrept(string Data)
    {
        string modifiedData = "";
        for (int i = 0; i < Data.Length; i++)
        {
            modifiedData += (char)(Data[i] ^ Key[i % Key.Length]);
        }
        return modifiedData;
    }
}