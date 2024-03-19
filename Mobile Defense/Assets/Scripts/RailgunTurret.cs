using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RailgunTurret : Turret
{
    RaycastHit hit;
    public override void Shoot()
    {
        aS.PlayOneShot(shootSound);
        RaycastHit[] rayHits = Physics.RaycastAll(transform.position + Vector3.up * 0.2f, rotatingPart.forward, 100f);
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
}
