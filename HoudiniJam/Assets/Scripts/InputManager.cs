using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject m_minePrefab;
    [SerializeField] private Controller m_controller;

    private void Update()
    {
        if (m_controller.GameFinished)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("SpawnGrid"))
                {
                    if (!CanSpawnMine(hit.point))
                        return;

                    Instantiate(m_minePrefab, hit.point, Quaternion.identity);
                }
                else if (hit.transform.CompareTag("Mine"))
                {
                    hit.transform.GetComponent<Mine>().OnClick();
                }
            }
        }
    }

    private bool CanSpawnMine(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 4);

        var foundColliders = hitColliders.Where(hitColliders => hitColliders.gameObject.CompareTag("Mine")).Select(hc => hc.gameObject);

        return foundColliders.Count() <= 0;
    }
}
