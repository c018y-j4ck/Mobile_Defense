using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    private float fireTimer = 0f;
    private string enemyTag = "Enemy";

    public Transform rotatingPart;
    public Transform firePoint;
    public GameObject bullet;
    public AudioClip shootSound;
    private AudioSource aS;

    [Header("Attributes")]
    public float range = 15f;
    public float rotationSpeed = 10f;
    [Range(0.1f, 60f)] public float fireRate = 1f;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject e in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, e.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = e;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else target = null;
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
        fireTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("Shooting");
        aS.PlayOneShot(shootSound);
        GameObject b = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet bScript = b.GetComponent<Bullet>();
        if (bScript != null) bScript.SetTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
