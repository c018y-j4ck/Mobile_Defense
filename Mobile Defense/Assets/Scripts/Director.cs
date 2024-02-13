using System.Collections;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04

public class Director : MonoBehaviour
{
    /// <summary>
    /// Prefab of the enemy.
    /// </summary>
    public GameObject enemy;

<<<<<<< HEAD
    public GameObject endNode;

=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    /// <summary>
    /// The point in world space where enemies will spawn from.
    /// </summary>
    public Transform spawnPoint;

<<<<<<< HEAD
    public Text waveCount;
    private static Text livesCount;

    public static int lives = 25;

    public float turretYOffset = 0.5f;

    private GameObject turret;

=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    /// <summary>
    /// The time between each wave.
    /// </summary>
    public float timeBetweenWaves = 3f;

    /// <summary>
    /// Time before the first wave begins.
    /// </summary>
    private float countdown = 2f;

    /// <summary>
    /// The current wave number.
    /// </summary>
    private int wave = 0;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        livesCount = GameObject.Find("LivesCount").GetComponent<Text>();
        livesCount.text = "Lives: " + lives;
=======
        
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        wave++;
        Debug.Log("Spawning a wave\n" +
            "Wave " + wave);
<<<<<<< HEAD
        waveCount.text = "Wave " + wave;
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04

        for (int i = 0; i < wave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
<<<<<<< HEAD

    public static void LoseLife()
    {
        lives--;
        livesCount.text = "Lives: " + lives;
        if (lives <= 0) Debug.Log("You died!");
    }
=======
>>>>>>> 2e595e0bd6570489cdd7fdd8cc90be1e2485ed04
}
