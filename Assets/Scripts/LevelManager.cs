using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public InputActionReference input;
    public Animator transitionAnimator;
    public float delay;

    private void Awake()
    {
        instance = this;
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
            input.action.performed += _ => ReloadLevel();
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
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        input.action.Enable();
    }
    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        input.action.Disable();
    }
}
