using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    /// <summary>
    /// Prefab of the enemy.
    /// </summary>
    public GameObject enemy;
    public static int numOfEnemies;

    public GameObject endNode;

    /// <summary>
    /// The point in world space where enemies will spawn from.
    /// </summary>
    public Transform spawnPoint;

    public Text waveCount;
    private static Text livesCount;
    private static Text scoreCount;
    

    public static int lives = 25;
    public static int score = 10;

    public float turretYOffset = 0.5f;

    private GameObject turret;
    /// <summary>
    /// The time between each wave.
    /// </summary>
    public float timeBetweenWaves = 10f;

    /// <summary>
    /// Time before the first wave begins.
    /// </summary>
    private float countdown = 4f;

    /// <summary>
    /// The current wave number.
    /// </summary>
    private int wave = 0;

    // Start is called before the first frame update
    void Start()
    {
        lives = 25;
        livesCount = GameObject.Find("LivesCount").GetComponent<Text>();
        livesCount.text = "Lives: " + lives;

        score = 10;
        scoreCount = GameObject.Find("ScoreCount").GetComponent<Text>();
        scoreCount.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        //wait to spawn more enemies until the enemies
        //currently alive are defeated
        if (numOfEnemies > 0) return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        wave++;
        Debug.Log("Spawning a wave\n" +
            "Wave " + wave);
        waveCount.text = "Wave " + wave;

        for (int i = 0; i < wave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        numOfEnemies++;
        
    }
    public static void LoseLife()
    {
        lives--;
        livesCount.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Debug.Log("You died!");
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void AddScore(int s)
    {
        score += s;
        scoreCount.text = "Score: " + score;
    }

    public static bool RemoveScore (int s)
    {
        if (score >= s)
        {
            score -= s;
            scoreCount.text = "Score: " + score;
            return true;
        }
        return false;
    }
}
