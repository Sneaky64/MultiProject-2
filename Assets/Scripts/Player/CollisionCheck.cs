using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CollisionCheck : MonoBehaviour
{
    #region Variables, Awake and Start
    public UnityEvent StayEvent, ExitEvent, EnterEvent;

    public LayerMask mask;

    #endregion
    #region Custom Functions
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (mask == (mask | (1 << collision.gameObject.layer)))
            return;
        StayEvent.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mask == (mask | (1 << collision.gameObject.layer)))
            return;
        ExitEvent.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mask == (mask | (1 << collision.gameObject.layer)))
            return;
        EnterEvent.Invoke();
    }
    #endregion
}
