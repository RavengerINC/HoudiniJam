using UnityEngine;

public class MiniAsteroid : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Asteroid"))
        {
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
}
