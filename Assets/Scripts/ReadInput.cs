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

    private void Awake() 
    {
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
        scoreToSave = score;
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
