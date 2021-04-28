﻿using Assets.Scripts.Logging;
using Assets.Scripts.StringMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]
    private float transitionTime = 1f;

    private Animator myAnimator;

    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        MakeSingleton();
        myAnimator = GetComponent<Animator>();
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevelFade(currentSceneIndex + 1));
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(LoadLevelFade(levelIndex));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelFade(Levels.MainMenu));
    }

    public void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadLevel(currentScene.buildIndex);
    }

    public IEnumerator LoadLevelFade(int levelIndex)
    {
        if(levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
        {
            //Play animation
            instance.myAnimator.SetTrigger(States.Fade);
            //Wait
            yield return new WaitForSeconds(transitionTime);
            //Load Scene
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Logging.LogError("Invalid Scene Index specified!");
        }
    }

    public IEnumerator LoadLevelFade(string level)
    {
        if(Application.CanStreamedLevelBeLoaded(level))
        {
            //Play animation
            instance.myAnimator.SetTrigger(States.Fade);
            //Wait
            yield return new WaitForSeconds(transitionTime);
            //Load Scene
            SceneManager.LoadScene(level);
        }
        else
        {
            Logging.LogError("Invalid Scene Name specified!");
        }
    }
}