using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public LayerMask mask;
    public LayerMask playerMask;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;

        if (target == null)
            return;
        Vector2 start = new Vector2(transform.position.x, transform.position.y);
        Vector2 end = new Vector2(target.position.x, target.position.y);
        Vector2 dir = end - start;

        Debug.DrawRay(start, dir, Color.green, 0.1f);

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 100f, mask);

        if (playerMask == (playerMask | (1 << hit.collider.gameObject.layer)))
        {
            Vector3 dir_ = new Vector3(dir.x, dir.y, 0);
            rb.velocity = dir_.normalized * speed * Time.fixedDeltaTime;
        }
        else
            rb.velocity = Vector3.zero;
    }
}
