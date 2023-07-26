using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IKillable
{
    public ParticleSystem deathParticles;
    public UnityEvent deathEvent, pauseEvent, playEvent;
    public Vector3 offset;
    bool paused = false;
    public InputActionReference input;

    public void Kill()
    {
        // Play a sound

        Instantiate(deathParticles, transform.position + offset, Quaternion.identity).Play();

        deathEvent.Invoke();

        Destroy(gameObject);
    }

    private void Update()
    {
        input.action.performed += ctx => Pause();
    }

    void Pause()
    {
        if (paused)
        {
            playEvent.Invoke();
            paused = false;
        }
        else if(!paused)
        {
            pauseEvent.Invoke();
            paused = true;
        }
    }
    #region Enable Disable
    private void OnEnable()
    {
        input.action.Enable();
    }
    private void OnDisable()
    {
        input.action.Disable();
    }
    #endregion
}
