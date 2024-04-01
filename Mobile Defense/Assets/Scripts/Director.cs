using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    /// <summary>
    /// Prefab of the enemy.
    /// </summary>

    //public GameObject enemy;

    public Wave[] waves;

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
    public int waveGoal;

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
    private int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume%");

        float sizePercent = PlayerPrefs.GetFloat("text%");
        Text[] allText = FindObjectsOfType<Text>();
        foreach (Text i in allText)
        {
            i.fontSize = (int)sizePercent * i.fontSize;
        }

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
            if (waveIndex >= waveGoal)
            {
                Debug.Log("Game Won!");
                SceneManager.LoadScene("GameWon");
            }
            else
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Spawning a wave\n" +
            "Wave " + waveIndex);
        waveCount.text = "Wave " + (waveIndex + 1);

        //set the current wave. If we've reached
        //the end of the wave list, default to the first wave
        Wave wave;
        if (waves.Length > 0)
        {
            if (waveIndex < waves.Length) wave = waves[waveIndex];
            else wave = waves[0];
        }
        //if no waves are defined, this warning will display
        else
        {
            Debug.LogWarning("Warning: The director has no waves defined! " +
            "You can create waves for the director in the inspector");
            yield break;
        }

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
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
