using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float seconds = 2.0f;

    private void OnEnable()
    {
        Destroy(gameObject, seconds);
    }
}
