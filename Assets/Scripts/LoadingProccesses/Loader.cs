using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scenes
    {
        TestScene, MainMenu, HQ, LoadingScrene
    }
    public static void Load(Scenes scene)
    {
        // загрузка требуемой сцены
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };
        SceneManager.LoadScene(Scenes.LoadingScrene.ToString()); //экран загрузки
    }

    private static Action onLoaderCallback;

    public static void LoaderCallback()
    {
        // запуститься после триггера isFirstUpdate который обновляет сцену
        //активитует загрузчик который загрузит нужную сцену
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
