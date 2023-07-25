using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool open = false;
    public void SetOpen(bool open_)
    {
        open = open_;
    }
    public void NextLevel()
    {
        if (!open)
            return;

        int id = SceneManager.GetActiveScene().buildIndex+1;

        if (SceneUtility.GetScenePathByBuildIndex(id) != "")
            LevelManager.instance.LoadLevel(id);
        else
            LevelManager.instance.LoadLevel(0);
    }
}
