using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [HideInInspector] public SaveData _data;
    string _filepath;                            
    string fileName = "Data.json";             
    void Awake()
    {
        // パス名取得
        _filepath = Path.Combine(Application.persistentDataPath, fileName);
        if(!File.Exists(_filepath))
        {
            _data = new SaveData();
            Save(_data);
        }
        else
        {
            _data = Load(_filepath);
        }
    }

    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data); 
        StreamWriter sw = new StreamWriter(_filepath,false);
        sw.WriteLine(json);
        sw.Close();
    }
    public SaveData Load (string path)
    {
        StreamReader sr = new StreamReader(path);
        string json = sr.ReadToEnd();
        sr.Close();
        
        return JsonUtility.FromJson<SaveData>(json);
    }
    public void OnSave()
    {
        Save(_data);
    }
    public void OnLoad ()
    {
        _data = Load(_filepath);
    }

    //// Update is called once per frame
    //void OnDestroy()
    //{
    //    Save(_data);
    //}
}
