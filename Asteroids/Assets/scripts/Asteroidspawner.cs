using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidspawner : MonoBehaviour
{
    public asteroid asteroidprefab;
    public float spawnRate = 2.0f;
    public float spawnAmount = 1;
    public float spawnDistance = 60.0f;
    public float trajectoryVariance = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnAmount);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            // Insideunitcircle.normalized znamena na okraji toho kruhu

            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);

            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);


            asteroid asteroid1 = Instantiate(this.asteroidprefab, spawnPoint, rotation);
            asteroid1.size = Random.Range(asteroid1.minSize, asteroid1.maxSize);
            asteroid1.SetTrajectory(rotation * -spawnDirection);
        }


    }
}
