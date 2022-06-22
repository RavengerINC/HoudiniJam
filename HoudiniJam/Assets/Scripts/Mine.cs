using System.Collections;
using System.Linq;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private bool m_isArming = false;
    private bool m_isDamaged = false;
    private bool m_isRepairing = false;
    private bool m_isExploding = false;

    [SerializeField] private float m_armingTime = 1.5f;
    [SerializeField] private float m_repairTime = 2.0f;

    [SerializeField] private float m_explosionRadius = 5.0f;

    private Collider m_collider;
    private MeshRenderer m_renderer;

    private IEnumerator runningCoroutine;

    public bool CanExplode
    {
        get { return !m_isArming && !m_isDamaged && !m_isRepairing && !m_isExploding; }
    }

    private void OnEnable()
    {
        m_isArming = true;

        m_collider = GetComponent<Collider>();
        m_renderer = GetComponent<MeshRenderer>();

        runningCoroutine = Arm();
        StartCoroutine(runningCoroutine);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("MiniAsteroid") || collision.transform.CompareTag("Asteroid"))
        {
            if (!m_isDamaged)
            {
                StopCoroutine(runningCoroutine);
                DamageMine();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }

    public void OnClick()
    {
        if (m_isRepairing || m_isExploding || m_isArming)
            return;

        if (m_isDamaged)
        {
            StartCoroutine(Repair());
        }
        else
        {
            StartCoroutine(ExplodeMine());
        }
    }

    private IEnumerator ExplodeMine()
    {
        m_isExploding = true;

        m_collider.enabled = false;

        Explode();

        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);
    }

    private void DamageMine()
    {
        m_isDamaged = true;
        m_renderer.material.color = Color.red;
    }

    private IEnumerator Arm()
    {
        m_renderer.material.color = Color.yellow;

        yield return new WaitForSeconds(m_armingTime);

        m_isArming = false;
        m_renderer.material.color = Color.white;
    }

    private IEnumerator Repair()
    {
        m_isRepairing = true;

        yield return new WaitForSeconds(2.0f);

        m_isRepairing = false;
        m_renderer.material.color = Color.white;
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_explosionRadius);

        var asteroids = hitColliders.Where(hitColliders => hitColliders.gameObject.CompareTag("Asteroid") || hitColliders.gameObject.CompareTag("MiniAsteroid")).Select(hc => hc.gameObject);

        foreach(var asteroid in asteroids)
        {
            Destroy(asteroid.gameObject);
        }

        Destroy(gameObject);
    }
}
