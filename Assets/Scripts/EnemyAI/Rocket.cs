using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    float moveSpeed;
    Rigidbody2D rb;
    public float rotationSpeed;
    Transform target;
    UnityEvent deathEvent;
    public LayerMask playerMask;
    public LayerMask collisionMask;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (target == null)
            Destroy(gameObject);
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);


        rb.velocity = transform.up.normalized * moveSpeed*Time.deltaTime;
    }

    public void Setup(Transform target_, float speed_, UnityEvent event_)
    {
        target = target_;
        moveSpeed = speed_;
        deathEvent = event_;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionMask == (collisionMask | (1 << collision.gameObject.layer)))
        {
            if (playerMask == (playerMask | (1 << collision.gameObject.layer)))
            {
                deathEvent.Invoke();
            }
            Destroy(gameObject);
        }
    }
}
