using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IKillable
{
    public ParticleSystem deathParticles;
    public Vector3 offset;
    public void Kill()
    {
        Debug.Log("You Died");

        // Destroy Player sprite

        // Play a sound

        Instantiate(deathParticles, transform.position + offset, Quaternion.identity).Play();
        Destroy(gameObject);
        // Open UI death menu

    }
}
