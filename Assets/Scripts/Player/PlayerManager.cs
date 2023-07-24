using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour, IKillable
{
    public ParticleSystem deathParticles;
    public UnityEvent deathEvent, pauseEvent, playEvent;
    public Vector3 offset;
    private MasterInput input;
    bool paused = false;
    private void Awake()
    {
        input = new();
    }
    public void Kill()
    {
        // Play a sound

        Instantiate(deathParticles, transform.position + offset, Quaternion.identity).Play();

        deathEvent.Invoke();

        Destroy(gameObject);
    }

    private void Update()
    {
        input.InGame.Pause.performed += ctx => Pause();
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
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    #endregion
}
