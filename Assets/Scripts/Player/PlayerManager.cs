using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour, IKillable
{
    public ParticleSystem deathParticles;
    public UnityEvent deathEvent;
    public Vector3 offset;
    public void Kill()
    {
        Debug.Log("You Died");

        // Play a sound

        Instantiate(deathParticles, transform.position + offset, Quaternion.identity).Play();

        deathEvent.Invoke();

        Destroy(gameObject);
    }
}
