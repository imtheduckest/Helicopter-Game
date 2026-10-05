using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HangarValues : MonoBehaviour
{
    public static HangarValues Instance;

    public string targetLevelName;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Запрещаем Unity удалять объект при смене сцен
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Levels menu
    public void SelectLevel(string sceneName)
    {
        targetLevelName = sceneName;
        SceneManager.LoadScene("Hangar");
    }

    // Battle button
    public void LaunchLevel()
    {
        SceneManager.LoadScene(targetLevelName);
    }
}
