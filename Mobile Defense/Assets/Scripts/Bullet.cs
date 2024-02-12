using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Range(10f, 100f)] public float speed = 70f;

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
        Debug.Log("Hit");
    }
}
