using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Transform model;
    public GameObject lookTransform;
    public AudioClip spawnSound;
    public AudioClip damageSound;
    public AudioSource soundSource;

    [Range(1f, 100f)] public float speed = 4f;
    Vector3 tiltAdditive = Vector3.zero;

    public const float maxHealth = 100f; //remove const later
    public float health;

    [Header("Used Objects")]
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //director = GameObject.Find("GameManager").GetComponent<Director>();
        soundSource = GetComponent<AudioSource>();
        soundSource.PlayOneShot(spawnSound);
        target = Waypoints.waypoints[0];
        health = maxHealth;
        model = transform.Find("Model").transform;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        tiltAdditive = new Vector3(Input.acceleration.x, 0f, Input.acceleration.y);
#endif

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * (speed + Vector3.Dot(dir.normalized, tiltAdditive) * speed) * Time.deltaTime, Space.World);
        if (lookTransform != null)
        {
            lookTransform.transform.LookAt(target.position);
            model.rotation = Quaternion.Lerp(model.rotation, lookTransform.transform.rotation, 0.05f);
        }

        if (dir.magnitude <= 0.2f)
        {
            GetNextWaypoint();
        }
    }
    public void TakeDamage(float deltaHealth)
    {
        health -= deltaHealth;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0f)
        {
            Die();
            Director.AddScore(5);
        }
    }

    void Die()
    {
        Destroy(gameObject);
        return;
    }
    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            ReachEndNode();
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
    void ReachEndNode()
    {
        soundSource.PlayOneShot(damageSound);
        Director.LoseLife();
    }
}
