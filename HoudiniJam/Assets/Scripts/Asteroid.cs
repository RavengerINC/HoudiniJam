using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject miniAsteroids;

    private float m_spawnTime;
    public float SpawnTime { get { return m_spawnTime; } }

    //private Rigidbody rb;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject miniAsteroid = Instantiate(miniAsteroids, Random.insideUnitSphere + transform.position, Quaternion.identity);
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
