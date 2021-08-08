using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    
    public player player1;
    public ParticleSystem explosion;

    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawninvencibletime = 3.0f;
    
    public int score = 0;

    public void AsteroidDestroyed(asteroid asteroid1)
    {
        this.explosion.transform.position = asteroid1.transform.position;
        this.explosion.Play();

        if (asteroid1.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid1.size < 1.25f)
        {
            score += 50;
        }
        else 
        {
            score += 25;
        }
    }


    public void PlayerDied()
    {
        this.explosion.transform.position = this.player1.transform.position;
        
        this.explosion.Play();
        
        this.lives--;

        if (this.lives <= 0)
        {
            Gameover();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player1.gameObject.layer = LayerMask.NameToLayer("ignore collisions");
        this.player1.transform.position = Vector3.zero;
        this.player1.gameObject.SetActive(true);
        
        Invoke(nameof(TurnOnCollisions), respawninvencibletime);
    }

    private void TurnOnCollisions()
    {
        this.player1.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Gameover()
    {
        this.lives = 3;
        this.score = 0;
        Invoke(nameof(Respawn), this.respawnTime);
    }
}
