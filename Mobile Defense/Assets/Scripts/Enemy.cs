<<<<<<< HEAD
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    [Range(1f, 100f)] public float speed = 4f;

    public const float maxHealth = 100f; //remove const later
    public float health;

    [Header("Used Objects")]
    public Image healthBar;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1f, 100f)] public float speed = 4f;

    private Transform target;
    private int waypointIndex = 0;
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.waypoints[0];
<<<<<<< HEAD
        health = maxHealth;
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

<<<<<<< HEAD
        if (dir.magnitude <= 0.2f)
=======
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
        {
            GetNextWaypoint();
        }
    }

<<<<<<< HEAD
    public void TakeDamage(float deltaHealth)
    {
        health -= deltaHealth;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        return;
    }

=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
<<<<<<< HEAD
            ReachEndNode();
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
<<<<<<< HEAD

    void ReachEndNode()
    {
        Director.LoseLife();
    }
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
}
