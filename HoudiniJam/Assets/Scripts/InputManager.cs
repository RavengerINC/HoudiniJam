using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("SpawnGrid"))
                {
                    Instantiate(minePrefab, hit.point, Quaternion.identity);
                }
                else if (hit.transform.CompareTag("Mine"))
                {
                    hit.transform.GetComponent<Mine>().OnClick();
                }
            }
        }
    }
}
