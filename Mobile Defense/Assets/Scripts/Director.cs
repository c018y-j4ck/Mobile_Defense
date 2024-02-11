using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    /// <summary>
    /// Prefab of the enemy.
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// The point in world space where enemies will spawn from.
    /// </summary>
    public Transform spawnPoint;

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
}
