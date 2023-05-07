using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    #region Variables, Awake and Start
    public float speed;
    public Vector3 offset;

    [SerializeField] Transform target;
    #endregion
    #region Update, FixedUpdate
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed*Time.fixedDeltaTime);
    }
    #endregion
    #region Custom Functions
    #endregion
}
