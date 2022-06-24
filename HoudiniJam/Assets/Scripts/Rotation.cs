using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 rotation;

    [SerializeField] private float m_degreesPerSecond = 2.0f;

    private void Start()
    {
        rotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    void Update()
    {
        transform.Rotate(rotation * m_degreesPerSecond * Time.deltaTime);
    }
}
