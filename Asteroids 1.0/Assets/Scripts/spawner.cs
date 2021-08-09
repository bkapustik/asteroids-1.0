using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public asteroid asteroidprefab;

    public float spawnrate = 1.0f;
    public float spawnstart = 1.0f;
    public float distance = 25.0f;
    public float angle_variance = 15.0f;
    public gamemanager Gamemanager;
    public int CompareScore = 500;

    
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn),spawnstart,spawnrate);

    }

    private void Spawn()
    {
        Vector3 relative_spawn_position = Random.insideUnitCircle.normalized * distance;

        Vector3 actual_spawn_position = relative_spawn_position + transform.position;

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-angle_variance,angle_variance),new Vector3(0,0,1));

        asteroid Asteroid = Instantiate(asteroidprefab, actual_spawn_position, rotation);

        Asteroid.size = Random.Range(Asteroid.min_size, Asteroid.max_size);

        Asteroid.SetDirection(rotation * -actual_spawn_position);

    }

    private void Update()
    {
        if (Gamemanager.score > CompareScore)
        {
            spawnrate *= 0.5f;
            CompareScore += 500;
        }
    }
}
