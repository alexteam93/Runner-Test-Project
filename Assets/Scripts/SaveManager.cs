using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public class Save
{
    public int Coins;
}

public class SaveManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager GM;
    string filePath;

    public static SaveManager Instance;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        GM = FindObjectOfType<GameManager>();
        filePath = Application.persistentDataPath + "data.gamesave";

        LoadGame();
        SaveGame();
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();
        save.Coins = GM.coins;

        bf.Serialize(fs, save);
        fs.Close();

    }

    public void LoadGame()
    {
        if(!File.Exists(filePath))
            return;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);

        GM.coins = save.Coins;
        fs.Close();

        GM.RefreshCoinText();
    }
}
