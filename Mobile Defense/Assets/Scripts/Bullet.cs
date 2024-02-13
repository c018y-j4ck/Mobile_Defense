using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Range(10f, 100f)] public float speed = 70f;
<<<<<<< HEAD
    [Range(10f, 100f)] public float damage = 25f;
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            HitTarget();
<<<<<<< HEAD
            Destroy(gameObject);
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void HitTarget()
    {
<<<<<<< HEAD
        Enemy eScript;
        if (target.TryGetComponent<Enemy>(out eScript)) eScript.TakeDamage(damage);
=======
        Debug.Log("Hit");
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    }
}
