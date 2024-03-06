using UnityEngine;

public class Missile : Bullet
{
    public float explosionRadius = 5f;

    public override void HitTarget()
    {
        //check if we hit an enemy
        Enemy eScript;
        if (target.TryGetComponent<Enemy>(out eScript))
        {
            //if so, check if any enemy colliders are within the
            //explosion radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(Collider c in colliders)
            {
                if (c.gameObject.TryGetComponent<Enemy>(out eScript))
                {
                    //make all these enemies take damage
                    eScript.TakeDamage(damage);
                }
            }
            Debug.Log("Hit");
        }
    }
}
