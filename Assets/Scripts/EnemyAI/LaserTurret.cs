using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public LineRenderer laserRenderer;
    public float laserDelays, firingTime;
    public LayerMask laserVisualMask, playerMask;
    public Transform firePoint;

    bool active;

    public void Start()
    {
        StartCoroutine(LaserLogic());
    }
    private void Update()
    {
        if (active)
        {
            LaserVisuals();
            LaserHits();
        }
        else
            laserRenderer.SetPosition(1, Vector3.zero);
    }
    
    private void LaserHits()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.left), 100f, playerMask);
        if(hit.collider!=null)
        {
            hit.collider.gameObject.GetComponent<IKillable>()?.Kill();
        }
    }

    private void LaserVisuals()
    {
        Vector2 position = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.left), 100f, laserVisualMask);
       
        laserRenderer.SetPosition(1, new Vector3(hit.distance, 0, 0));
    }

    private IEnumerator LaserLogic()
    {
        while(true)
        {
            active = true;
            yield return new WaitForSeconds(firingTime);
            active = false;
            yield return new WaitForSeconds(laserDelays);
        }
    }
}
