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
        StartCoroutine(LevelTransition(id));
    }
    public void ReloadLevel() 
    {
        StartCoroutine(LevelTransition(SceneManager.GetActiveScene().buildIndex));
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

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    { 
        input.Disable();
    }
}
