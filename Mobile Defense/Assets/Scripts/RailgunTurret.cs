using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RailgunTurret : Turret
{
    [Header("Laser Visuals")]
    public LineRenderer lineRenderer;
    private Ray ray;
    RaycastHit[] rayHits;

    public override void Shoot()
    {
        aS.PlayOneShot(shootSound);

        ray = new Ray();
        ray.origin = transform.position + Vector3.up * 0.2f;
        ray.direction = rotatingPart.forward;
        rayHits = Physics.RaycastAll(ray);

        laserWidth = 1f;

        UnityEngine.Debug.DrawRay(transform.position, rotatingPart.forward * 5f, Color.green, 0.5f);
        if (rayHits.Length > 0)
        {
            Enemy eScript;
            foreach (RaycastHit hit in rayHits)
            {
                if (hit.collider.gameObject.TryGetComponent<Enemy>(out eScript))
                {
                    eScript.TakeDamage(40f);
                }
            }
        }
    }

    public override void RenderLaser()
    {
        base.RenderLaser();

        lineRenderer.widthMultiplier = laserWidth;

        if (target == null)
        {
            return;
        }
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * 100f);
    }
}
