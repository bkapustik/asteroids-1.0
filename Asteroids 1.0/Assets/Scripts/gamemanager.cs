using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{

    public int score = 0;
    public float split_speed = 5.0f;
    public float untouchable_time = 3.0f;
    public float respawn_time = 1.0f;
    public player Player;
    public ParticleSystem explosion;
    private int number_of_lives = 3;
    public Text score_text;
    public Text gameover_text;
    public Text lifes_text;
    public Text gameoverscore_text;
    public Button button;
    public spawner Spawner;

    private void Start()
    {
        gameover_text.gameObject.SetActive(false);
        gameoverscore_text.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    private void Update()
    {
        score_text.text = "Score: " + score;
        lifes_text.text = "Lifes: " + number_of_lives;
    }

    public void AsteroidDestroyed(asteroid Asteroid)
    {
        explosion.transform.position = Asteroid.transform.position;
        explosion.Play();
        if (Asteroid.size < 1.0f) score += 100;
        else if (Asteroid.size < 1.25f) score += 50;
        else score += 25;
    }

    public void PlayerDied()
    {
        explosion.transform.position = Player.transform.position;
        explosion.Play();
        if (number_of_lives > 0)
        {
            number_of_lives--;
            Invoke(nameof(Respawn), respawn_time);
        }
        else GameOver();
    }

    public void Respawn()
    {
        
        Player.gameObject.layer = 9;
        Player.transform.position = new Vector3(0, 0, 0);
        Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        Player.gameObject.SetActive(true);
        Invoke(nameof(ChangeLayer), untouchable_time);
    }

    private void ChangeLayer()
    {
        Player.gameObject.layer = 7;
    }

    private void GameOver()
    {
        gameover_text.gameObject.SetActive(true);
        gameoverscore_text.gameObject.SetActive(true);
        score_text.gameObject.SetActive(false);
        lifes_text.gameObject.SetActive(false);
        gameover_text.text = "Game Over \n";
        gameoverscore_text.text = "Your final score is: " + score;
        button.gameObject.SetActive(true);
    }

    public void ButtonClick()
    {
        
        Respawn();
        Application.LoadLevel(Application.loadedLevel);
    }

}
