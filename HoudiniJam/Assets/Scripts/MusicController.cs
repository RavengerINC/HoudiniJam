using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip m_backgroundMusic;

    void Start()
    {
        AudioController.Instance.PlayMusic(m_backgroundMusic);
    }
}
