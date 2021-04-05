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

    public void LoadMainMenu()
    {
        LevelManager.instance.LoadMainMenu();
    }


}