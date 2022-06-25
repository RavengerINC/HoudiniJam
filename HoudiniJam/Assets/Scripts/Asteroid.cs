using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject[] m_miniAsteroids;
    [SerializeField] GameObject m_asteroidExplosion;
    [SerializeField] GameObject m_shipExplosion;
    [SerializeField] private AudioClip m_asteroidCollideAudio;

    private float m_spawnTime;
    public float SpawnTime { get { return m_spawnTime; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Vector3 position = Random.insideUnitSphere + transform.position;
            //    GameObject miniAsteroid = Instantiate(GetRandomMiniAsteroidPrefab(), position, Random.rotation);
            //    Vector3 velocity = (position - transform.position).normalized * Random.Range(3.0f, 5.0f);

            //    Rigidbody rb = miniAsteroid.GetComponent<Rigidbody>();
            //    rb.velocity = velocity;
            //    rb.rotation = Random.rotation;
            //}

            AudioController.Instance.Play(m_asteroidCollideAudio);
            ExplodeAsteroid();
        }
        else if (collision.transform.CompareTag("MiniAsteroid"))
        {
            return;
        }
        else if (collision.transform.CompareTag("Spaceship"))
        {
            return;
        }
    }

    public void SetSpawnTime(float time)
    {
        m_spawnTime = time;
    }

    private GameObject GetRandomMiniAsteroidPrefab()
    {
        int randomIndex = Random.Range(0, m_miniAsteroids.Length);

        return m_miniAsteroids[randomIndex];
    }

    public void ExplodeOnShip()
    {
        Instantiate(m_shipExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void ExplodeAsteroid()
    {
        Instantiate(m_asteroidExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
