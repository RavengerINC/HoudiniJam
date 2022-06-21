using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_asteroidPrefab;
    [SerializeField] float m_initialSpawnRate = 2.0f;
    [SerializeField] float m_spawnRateIncrease = 0.05f;

    private float m_spawnRate;
    private Collider m_collider;
    private float m_BEX, m_BEZ;
    private Vector3 m_centre;

    private void Start()
    {
        m_collider = GetComponent<Collider>();

        CalcBounds();

        m_spawnRate = m_initialSpawnRate;

        StartCoroutine(SpawnContinualAsteroids());
    }

    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid = other.GetComponent<Asteroid>();
        if(asteroid.SpawnTime + 2.0f < Time.time)
        {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpawnContinualAsteroids()
    {
        while (true)
        {
            SpawnAsteroid();

            yield return new WaitForSeconds(m_spawnRate);

            m_spawnRate = Mathf.Clamp(m_spawnRate - m_spawnRateIncrease, 0.1f, m_initialSpawnRate);
        }
    }

    private void SpawnAsteroid()
    {
        Vector3 position = GetRandomPositionInCollider();
        GameObject asteroid = GetRandomAsteroidPrefab();

        GameObject go = Instantiate(asteroid, position, Quaternion.identity);

        go.GetComponent<Rigidbody>().velocity = CalculateInitialTrajectory(position);
        go.GetComponent<Asteroid>().SetSpawnTime(Time.time);
    }

    private void CalcBounds()
    {
        m_BEX = m_collider.bounds.extents.x;
        m_BEZ = m_collider.bounds.extents.z;

        m_centre = m_collider.bounds.center;
    }

    private Vector3 GetRandomPositionInCollider()
    {
        float offsetX = Random.Range(-m_BEX, m_BEX);
        float offsetY = 0.0f;
        float offsetZ = Random.Range(-m_BEZ, m_BEZ);

        return m_centre + new Vector3(offsetX, offsetY, offsetZ);
    }

    private GameObject GetRandomAsteroidPrefab()
    {
        int randomIndex = Random.Range(0, m_asteroidPrefab.Length);

        return m_asteroidPrefab[randomIndex];
    }



    private Vector3 CalculateInitialTrajectory(Vector3 startingPosition)
    {
        return -startingPosition.normalized * 5.0f;
    }
}
