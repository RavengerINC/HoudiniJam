using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject[] m_miniAsteroids;
    [SerializeField] VisualEffect asteroidCollisionVFX;

    private float m_spawnTime;
    public float SpawnTime { get { return m_spawnTime; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 position = Random.insideUnitSphere + transform.position;
                GameObject miniAsteroid = Instantiate(GetRandomMiniAsteroidPrefab(), position, Random.rotation);
                Vector3 velocity = (position - transform.position).normalized * Random.Range(3.0f, 5.0f);

                Rigidbody rb = miniAsteroid.GetComponent<Rigidbody>();
                rb.velocity = velocity;
                rb.rotation = Random.rotation;
            }

            //StartCoroutine(CollisionVFX());

            VisualEffect vfx = Instantiate(asteroidCollisionVFX, transform.position, transform.rotation);
            vfx.SendEvent("OnPlay");

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

    private GameObject GetRandomMiniAsteroidPrefab()
    {
        int randomIndex = Random.Range(0, m_miniAsteroids.Length);

        return m_miniAsteroids[randomIndex];
    }

    //private IEnumerator CollisionVFX()
    //{
    //    GetComponent<Collider>().enabled = false;
    //    GetComponentInChildren<MeshRenderer>().enabled = false;

    //    asteroidCollisionVFX.SendEvent("OnPlay");

    //    yield return new WaitForSeconds(3.0f);

    //    Destroy(gameObject);
    //}
}
