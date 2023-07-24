using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    #region Variables, Awake and Start
    public float speed;
    public Vector3 offset;

    private Vector3 lastKnownPoint;

    [SerializeField] Transform target;
    #endregion
    #region Update, FixedUpdate
    private void FixedUpdate()
    {
        if(target!=null)
            lastKnownPoint = target.position;
        transform.position = Vector3.Lerp(transform.position, lastKnownPoint + offset, speed*Time.fixedDeltaTime);
    }
    #endregion
    #region Custom Functions
    #endregion
}
