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
    public float timeBetweenWaves = 3f;

    /// <summary>
    /// Time before the first wave begins.
    /// </summary>
    private float countdown = 2f;

    /// <summary>
    /// The current wave number.
    /// </summary>
    private int wave = 0;

    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    Vector3 touchStart;

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
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
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
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
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
