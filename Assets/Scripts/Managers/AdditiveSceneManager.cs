using System;
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

    /// <summary>
    /// Loads an additive scene. 
    /// 
    /// Additive scene can be either [Scenes.CONSTRUCT] or [Scenes.HUD].
    /// </summary>
    /// <param name="beforeSceneUnload">Handler executed before scene is loaded. If null, default handler is executed.</param>
    /// <param name="onSceneUnload">Handler executed after scene is loaded. If null, default handler is executed.</param>
    public void LoadScene(Scenes scene, BeforeSceneLoadDelegate beforeSceneLoad, OnSceneLoadDelegate onSceneLoad)
    {
        if(currentScene != Scenes.NONE)
        {
            throw new Exception("There is another scene loaded. Unload first.");
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
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
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

    /// <summary>
    /// Unloads an additive scene. 
    /// 
    /// Additive scene can be either [Scenes.CONSTRUCT] or [Scenes.HUD].
    /// </summary>
    /// <param name="beforeSceneUnload">Handler executed before scene is unloaded. If null, default handler is executed.</param>
    /// <param name="onSceneUnload">Handler executed after scene is unloaded. If null, default handler is executed.</param>
    public void UnloadScene(BeforeSceneUnloadDelegate beforeSceneUnload, OnSceneUnloadDelegate onSceneUnload)
    {
        if (currentScene == Scenes.NONE)
        {
            throw new Exception("There is no scene loaded.");
        }

        String sceneName = SceneNameForScene(currentScene);
        if (sceneName != "")
        {
            if (beforeSceneUnload != null) {
                beforeSceneUnload();
            } else { 
                DefaultBeforeSceneUnload();
            }
            SceneManager.UnloadSceneAsync(sceneName);
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
