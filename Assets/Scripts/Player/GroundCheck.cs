using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    #region Variables, Awake and Start
    public PlayerMovement player;
    #endregion
    #region Custom Functions

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        player.SetGroundedState(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        player.SetGroundedState(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
            return;
        player.SetGroundedState(true);
    }

    #endregion
}
