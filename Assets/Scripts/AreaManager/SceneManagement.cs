using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : SingleTon<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
}