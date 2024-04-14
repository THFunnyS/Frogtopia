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
        // �������� ��������� �����
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };
        SceneManager.LoadScene(Scenes.LoadingScrene.ToString()); //����� ��������
    }

    private static Action onLoaderCallback;

    public static void LoaderCallback()
    {
        // ����������� ����� �������� isFirstUpdate ������� ��������� �����
        //���������� ��������� ������� �������� ������ �����
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
