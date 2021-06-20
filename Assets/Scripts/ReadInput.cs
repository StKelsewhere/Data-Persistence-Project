using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReadInput : MonoBehaviour
{
    public static ReadInput Instance;
    public string playerName;
    public int scoreToSave;
    public int highScore;

    private void Awake() 
    {
        LoadHighScore();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReadStringInput(string s)
    {
        playerName = s;
    }

    public void ContinueLoadMainGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GetFinalScore(int score)
    {
        if (score > highScore)
        {
            scoreToSave = score;
        } else
        {
            scoreToSave = highScore;
        }
    }

    public void Exit()
    {
        SaveHighScore();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    [System.Serializable]
    // ALL VARIABLES PUT INTO THIS SAVEDATA CLASS WILL BE SAVED BETWEEN SESSIONS
    class SaveData
    {
        public int scoreToSave;
    }
    
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.scoreToSave = scoreToSave;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.scoreToSave;
        }
    }
}
