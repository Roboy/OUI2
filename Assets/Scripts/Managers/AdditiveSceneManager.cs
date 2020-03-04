﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    NONE,
    HUD,
    CONSTRUCT
}

public class AdditiveSceneManager : MonoBehaviour
{
    static Scenes currentScene = Scenes.NONE;
    public delegate void BeforeSceneLoadDelegate();
    public delegate void OnSceneLoadDelegate();
    public delegate void BeforeSceneUnloadDelegate();
    public delegate void OnSceneUnloadDelegate();

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    public static Scenes GetCurrentScene()
    {
        return currentScene;
    }
    
    /// <summary>
    /// Delegate that executes everytime a scene is loaded to update currentScene variable.
    /// </summary>
    /// <param name="scene">laoded scene</param>
    /// <param name="mode">mode used to load the sene</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "FinalScene_Construct":
                currentScene = Scenes.CONSTRUCT;
                break;
            case "FinalScene_HUD":
                currentScene = Scenes.HUD;
                break;
        }
   }

    /// <summary>
    /// Finds scene name for scene
    /// </summary>
    /// <param name="scene">scene</param>
    /// <returns>name of scene</returns>
    String SceneNameForScene(Scenes scene)
    {
        switch(scene)
        {
            case Scenes.HUD:
                return "FinalScene_HUD";
            case Scenes.CONSTRUCT:
                return "FinalScene_Construct";
            default:
                return "";
        }
    }

    public void TriggerLoadScene(Scenes scene, BeforeSceneLoadDelegate beforeSceneLoad, OnSceneLoadDelegate onSceneLoad)
    {
        StartCoroutine(LoadScene(scene, beforeSceneLoad, onSceneLoad));
    }

    /// <summary>
    /// Loads an additive scene. 
    /// 
    /// Additive scene can be either [Scenes.CONSTRUCT] or [Scenes.HUD].
    /// </summary>
    /// <param name="beforeSceneUnload">Handler executed before scene is loaded. If null, default handler is executed.</param>
    /// <param name="onSceneUnload">Handler executed after scene is loaded. If null, default handler is executed.</param>
    IEnumerator LoadScene(Scenes scene, BeforeSceneLoadDelegate beforeSceneLoad, OnSceneLoadDelegate onSceneLoad)
    {
        if(currentScene != Scenes.NONE)
        {
            CoroutineManager.WaitCoroutine(UnloadScene(null, null));
            //StartCoroutine(UnloadScene(null, null));
            //throw new Exception("There is another scene loaded. Unload first.");
        }

        String sceneName = SceneNameForScene(scene);
        if(sceneName != "")
        {
            if (beforeSceneLoad != null)
            {
                beforeSceneLoad();
            }
            else
            {
                DefaultBeforeSceneLoad();
            }
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while(!asyncLoad.isDone)
            {
                yield return null;
            }
            if (onSceneLoad != null)
            {
                onSceneLoad();
            }
            else
            {
                DefaultOnSceneLoad();
            }
        }
    }

    public void TriggerUnloadScene(BeforeSceneUnloadDelegate beforeSceneUnload, OnSceneUnloadDelegate onSceneUnload)
    {
        StartCoroutine(UnloadScene(beforeSceneUnload, onSceneUnload));
    }

    /// <summary>
    /// Unloads an additive scene. 
    /// 
    /// Additive scene can be either [Scenes.CONSTRUCT] or [Scenes.HUD].
    /// </summary>
    /// <param name="beforeSceneUnload">Handler executed before scene is unloaded. If null, default handler is executed.</param>
    /// <param name="onSceneUnload">Handler executed after scene is unloaded. If null, default handler is executed.</param>
    IEnumerator UnloadScene(BeforeSceneUnloadDelegate beforeSceneUnload, OnSceneUnloadDelegate onSceneUnload)
    {
        if (currentScene == Scenes.NONE)
        {
            //throw new Exception("There is no scene loaded.");
            yield break;
        }

        String sceneName = SceneNameForScene(currentScene);
        if (sceneName != "")
        {
            if (beforeSceneUnload != null) {
                beforeSceneUnload();
            } else { 
                DefaultBeforeSceneUnload();
            }
            // bool unloadCompleted = SceneManager.UnloadScene(sceneName);
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            /*while (!asyncUnload.isDone)
            {
                yield return null;
            }*/

            if (onSceneUnload != null)
            {
                onSceneUnload();
            }
            else
            {
                DefaultOnSceneUnload();
            }
        }

        currentScene = Scenes.NONE;
    }

    /// <summary>
    /// Default handler executed before scene is loaded.
    /// </summary>
    private void DefaultBeforeSceneLoad() { }
    /// <summary>
    /// Default handler executed after scene is loaded.
    /// </summary>
    private void DefaultOnSceneLoad() { }
    /// <summary>
    /// Default handler executed before scene is unloaded.
    /// </summary>
    private void DefaultBeforeSceneUnload() { }
    /// <summary>
    /// Default handler executed after scene is unloaded.
    /// </summary>
    private void DefaultOnSceneUnload() { }

    /// <summary>
    /// Demo method to show how delegates work.
    /// </summary>
    private void DelegateDemo()
    {
        Debug.Log("Delegate!");
    }
}
