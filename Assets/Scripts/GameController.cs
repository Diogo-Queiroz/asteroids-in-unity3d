using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * This script is responsible for the logic behind the asteroids spawnming, lives, score and managing the game per se
 */
public class GameController : MonoBehaviour
{
    public GameObject asteroid;

    private int _score;
    private int _highScore;
    private int _asteroidsRemaining;
    private int _lives;
    private int _wave;
    private int _increaseEachWave = 2;
    
    public Text _scoreText;
    public Text _livesText;
    public Text _waveText;
    public Text _highScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        _highScore = PlayerPrefs.GetInt("_highScore", 0);
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void BeginGame()
    {
        // Beginning values
        _score = 0;
        _lives = 3;
        _wave = 1;
        
        // The HUD information
        _scoreText.text = $"SCORE: {_score}";
        _highScoreText.text = $"HIGHSCORE: {_highScore}";
        _livesText.text = $"LIVES: {_lives}";
        _waveText.text = $"WAVE: {_wave}";

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        // Just in case there is any asteroids in the game
        DestroyExistingAsteroids();

        _asteroidsRemaining = (_wave * _increaseEachWave);

        for (int i = 0; i < _asteroidsRemaining; i++) // Spawn the quantity for that wave
        {
            Instantiate(asteroid, // Prefab to instantiace
                new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0), // Random position between 9 and -9 to X, and 6 to -6 for Y
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f))); // Rotation of the asteroid.
        }
        _waveText.text = $"WAVE: {_wave}";
    }

    public void IncrementScore()
    {
        _score++;
        _scoreText.text = $"SCORE: {_score}";

        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.text = $"HIGHSCORE: {_highScore}";
            
            // Save the new High Score
            PlayerPrefs.SetInt("highScore", _highScore);
        }

        // If the player has destroyed every asteroid
        if (_asteroidsRemaining < 1)
        {
            // Call next wave
            _wave++;
            SpawnAsteroids();
        }
    }

    public void ReduceLives()
    {
        _lives--;
        _livesText.text = $"LIVES: {_lives}";

        // Player has died three times?
        if (_lives < 1)
        {
            BeginGame();
        }
    }

    // Modulate the methods
    public void DecrementAsteroids()
    {
        _asteroidsRemaining--;
    }

    public void SplitAsteroids()
    {
        // The split takes 1 large asteroid into 3 small asteroids
        _asteroidsRemaining += 2;
    }

    void DestroyExistingAsteroids()
    {
        GameObject[] largeAsteroids = GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject largeAsteroid in largeAsteroids)
        {
            GameObject.Destroy(largeAsteroid);
        }

        GameObject[] smallAsteroids = GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject smallAsteroid in smallAsteroids)
        {
            GameObject.Destroy(smallAsteroid);
        }
    }
}
