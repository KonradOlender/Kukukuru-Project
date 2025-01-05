using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public string menuName;
    public string firstLevel;
    public List<string> levelNames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuName);
    }
   public void LoadScene()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void LoadNextLeve()
    {
        currentLevel++;
        SceneManager.LoadScene(levelNames[currentLevel]);
    }
}
