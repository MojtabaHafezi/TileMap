using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        LevelManager.instance.LoadNextLevel();
    }

    public void LoadLevel(int levelIndex)
    {
        LevelManager.instance.LoadLevel(levelIndex);
    }

    public void LoadLevel(string levelName)
    {
        LevelManager.instance.LoadLevel(levelName);
    }

    public void LoadMainMenu()
    {
        LevelManager.instance.LoadMainMenu();
    }

    public void LoadOptions()
    {
        LevelManager.instance.LoadOptions();
    }

    public void LoadLevelMap()
    {
        LevelManager.instance.LoadLevelMap();
    }

    public void ReloadLevel()
    {
        LevelManager.instance.ReloadLevel();
    }
}