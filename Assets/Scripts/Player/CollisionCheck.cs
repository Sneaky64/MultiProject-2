using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCheck : MonoBehaviour
{
    #region Variables, Awake and Start
    public PlayerMovement player;

    public UnityEvent StayEvent, ExitEvent, EnterEvent;

    #endregion
    #region Custom Functions

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        StayEvent.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        ExitEvent.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        EnterEvent.Invoke();
    }

    #endregion
}
