using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneControls : MonoBehaviour
{
    public void ControlScenes(int SceneNum)
    {
        Loader.Scenes scene = (Loader.Scenes)SceneNum;
        Loader.Load(scene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
