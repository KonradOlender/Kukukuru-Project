using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string menuName;
    public string firstLevel;
    public string nextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(menuName);
    }
   public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevel);
    }
    public void LoadNextLeve()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
