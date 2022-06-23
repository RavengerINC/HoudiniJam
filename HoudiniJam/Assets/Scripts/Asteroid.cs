using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject miniAsteroids;

    private float m_spawnTime;
    public float SpawnTime { get { return m_spawnTime; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 position = Random.insideUnitSphere + transform.position;
                GameObject miniAsteroid = Instantiate(miniAsteroids, position, Random.rotation);
                Vector3 velocity = (position - transform.position).normalized * Random.Range(3.0f, 5.0f);

                Rigidbody rb = miniAsteroid.GetComponent<Rigidbody>();
                rb.velocity = velocity;
                rb.rotation = Random.rotation;
            }

            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("MiniAsteroid"))
        {
            return;
        }
        else if (collision.transform.CompareTag("Spaceship"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnTime(float time)
    {
        m_spawnTime = time;
    }

}
