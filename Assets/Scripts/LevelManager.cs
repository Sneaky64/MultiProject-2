using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private MasterInput input;
    public Animator transitionAnimator;
    public float delay;

    private void Awake()
    {
        instance = this;

        input = new();
    }
    public void LoadLevel(int id)
    {
        Time.timeScale = 1f;
        StartCoroutine(LevelTransition(id));
    }
    public void ReloadLevel() 
    {
        StartCoroutine(LevelTransition(SceneManager.GetActiveScene().buildIndex));
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            input.InGame.Restart.performed += _ => ReloadLevel();
        }
    }

    IEnumerator LevelTransition(int id)
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(id);
    }

    private void OnLevelWasLoaded(int level)
    {
        LeanTween.reset();
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    { 
        input.Disable();
    }
}
