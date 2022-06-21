using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private int m_maximumHealth = 10;

    private int m_currentHeatlh;

    private void Start()
    {
        m_currentHeatlh = m_maximumHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage();

        Destroy(other.gameObject);
    }

    private void TakeDamage()
    {
        m_currentHeatlh -= 1;

        if(m_currentHeatlh <= 0)
        {
            DestroyShip();
        }
    }

    private void DestroyShip()
    {
        Destroy(gameObject);
    }
}
