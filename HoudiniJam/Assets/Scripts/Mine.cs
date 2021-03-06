using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class Mine : MonoBehaviour
{
    private bool m_isArming = false;
    private bool m_isDamaged = false;
    private bool m_isRepairing = false;
    private bool m_isExploding = false;

    [SerializeField] private float m_armingTime = 1.5f;
    [SerializeField] private float m_repairTime = 2.0f;

    [SerializeField] private float m_explosionRadius = 5.0f;
    [SerializeField] GameObject m_mineExpldeVFX;
    [SerializeField] private AudioClip m_mineExplodeAudio;

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
        m_renderer = GetComponentInChildren<MeshRenderer>();

        StartCoroutine(Arm());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("MiniAsteroid") || collision.transform.CompareTag("Asteroid"))
        {
            if (!m_isDamaged)
            {
                StopAllCoroutines();

                m_isArming = false;
                m_isRepairing = false;

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
            StopAllCoroutines();
            m_isRepairing = false;
            m_isArming = false;
            StartCoroutine(Repair());
        }
        else
        {
            StopAllCoroutines();
            m_isRepairing = false;
            m_isArming = false;
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
        ChangeEmissionColour(Color.red);
    }

    private IEnumerator Arm()
    {
        ChangeEmissionColour(Color.yellow);

        yield return new WaitForSeconds(m_armingTime);

        m_isArming = false;
        ChangeEmissionColour(Color.green);
    }

    private IEnumerator Repair()
    {
        m_isRepairing = true;
        ChangeEmissionColour(Color.yellow);

        yield return new WaitForSeconds(1.0f);

        m_isRepairing = false;
        m_isDamaged = false;
        ChangeEmissionColour(Color.green);
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, m_explosionRadius);

        var asteroids = hitColliders.Where(hitColliders => hitColliders.gameObject.CompareTag("Asteroid") || hitColliders.gameObject.CompareTag("MiniAsteroid")).Select(hc => hc.gameObject);

        foreach(var asteroid in asteroids)
        {
            asteroid.GetComponent<Asteroid>().ExplodeAsteroid();
        }

        GameObject vfx = Instantiate(m_mineExpldeVFX, transform.position, Random.rotation);

        AudioController.Instance.Play(m_mineExplodeAudio);

        Destroy(gameObject);
    }

    private void ChangeEmissionColour(Color color)
    {
        m_renderer.material.SetColor("_Emission", color);
    }
}
