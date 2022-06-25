using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private int m_maximumHealth = 10;
    [SerializeField] private GameObject m_DestroyedEffects;
    [SerializeField] private Controller m_controller;

    private int m_currentHeatlh;
    public int CurrentHealth { get { return m_currentHeatlh; } }

    private void Start()
    {
        m_currentHeatlh = m_maximumHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Asteroid") || other.transform.CompareTag("MiniAsteroid"))
        {
            other.GetComponent<Asteroid>().ExplodeOnShip();
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        m_currentHeatlh -= 1;

        if(m_currentHeatlh <= 0)
        {
            m_currentHeatlh = 0;
            DestroyShip();
        }
    }

    private void DestroyShip()
    {
        m_controller.ShipDefeated();

        Instantiate(m_DestroyedEffects, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
