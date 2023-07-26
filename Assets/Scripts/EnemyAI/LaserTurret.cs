using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public LineRenderer laserRenderer;
    public float laserDelays, firingTime;
    public float activateTime;
    public LayerMask laserMask;
    public Transform firePoint;
    public float startDelay = 0f;

    bool active;

    public bool manual = false;

    public void Start()
    {
        StartCoroutine(LaserLogic());
    }
    private void Update()
    {
        if (!manual)
            LaserUpdate();
    }

    private void LaserUpdate()
    {
        if (active)
            ActivateLaser();
        else
            DeactivateLaser();
    }

    public void ActivateLaser()
    {
        Vector2 position = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.left), 100f, laserMask);

        if (hit.collider != null)
            hit.collider.gameObject.GetComponent<IKillable>()?.Kill();

        LeanTween.value(laserRenderer.GetPosition(1).x, -hit.distance, activateTime).setEaseOutQuint().setOnUpdate(SetLength).setIgnoreTimeScale(true);
        return;
    }

    public void DeactivateLaser()
    {
        LeanTween.value(laserRenderer.GetPosition(1).x, 0, activateTime).setEaseOutQuint().setOnUpdate(SetLength).setIgnoreTimeScale(true);
    }

    void SetLength(float value)
    {
        laserRenderer.SetPosition(1, new Vector3(value, 0, 0)); 
    }    

    private IEnumerator LaserLogic()
    {
        yield return new WaitForSeconds(startDelay);
        while(true)
        {
            active = true;
            yield return new WaitForSeconds(firingTime);
            active = false;
            yield return new WaitForSeconds(laserDelays);
        }
    }
}
