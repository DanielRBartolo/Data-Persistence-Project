using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance;
    public int pontos = 0;
    public string playerName;
    public string nameInput;
    public InputField nameText;
    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
        //Debug.Log(Application.persistentDataPath);
    }

    public void GetName() 
    {
        nameInput = nameText.text;
    }

    [System.Serializable]
    class SaveData
    {
        public int pontos;
        public string playerName;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.pontos = pontos;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "persistenceSava.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "persistenceSava.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            pontos = data.pontos;
            playerName = data.playerName;
        }
    }
}
