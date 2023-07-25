using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketBase : MonoBehaviour
{
    public Transform target;
    public float speed;
    public LayerMask mask;
    public LayerMask playerMask;
    public UnityEvent deathEvent;

    bool spawnDelay;
    bool crRunning;

    public float spawnOffset, rocketSpawnDelay;
    public GameObject rocketGO;
    Rocket rocket;

    private void FixedUpdate()
    {
        if (target == null)
            return;
        Vector2 start = new Vector2(transform.position.x, transform.position.y);
        Vector2 end = new Vector2(target.position.x, target.position.y);
        Vector2 dir = end - start;

        Debug.DrawRay(start, dir, Color.green, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 100f, mask);

        if (playerMask == (playerMask | (1 << hit.collider.gameObject.layer)) && rocket == null && !crRunning)
        {
            StartCoroutine(CreateRocket());
        }
    }

    IEnumerator CreateRocket()
    {
        crRunning = true;
        if(spawnDelay==true)
        {
            yield return new WaitForSeconds(rocketSpawnDelay);
        }
        spawnDelay = true;
        rocket = Instantiate(rocketGO, transform.position + spawnOffset * transform.up, Quaternion.identity).GetComponent<Rocket>();
        rocket.transform.rotation = transform.rotation;

        rocket.Setup(target, speed, deathEvent);

        crRunning = false;
        StopCoroutine(CreateRocket());
    }
}
