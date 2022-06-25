using System.Collections;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject m_victoryPanel;
    [SerializeField] private GameObject m_defeatPanel;

    [SerializeField] private float m_gameTime = 10.0f;
    [SerializeField] private TMP_Text m_timerText;

    private bool m_timerIsRunning;
    private float m_timeRemaining;

    private bool m_gameFinished = false;
    public bool GameFinished { get { return m_gameFinished; } }

    private void Awake()
    {
        m_victoryPanel.SetActive(false);
        m_defeatPanel.SetActive(false);

        m_timerIsRunning = true;
        m_timeRemaining = m_gameTime;

        UpdateTimerText();
    }

    private void Update()
    {
        Timer();
    }

    public void ShipDefeated()
    {
        if (m_gameFinished)
            return;

        m_gameFinished = true;

        StartCoroutine(ShipDefeatedSequence());
    }

    public void Victory()
    {
        if (m_gameFinished)
            return;

        m_gameFinished = true;
        m_victoryPanel.SetActive(true);
        StartCoroutine(LoadMenuAfterDelay());
    }

    private IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(4.0f);
        GetComponent<MenuController>().LoadMenu();
    }

    private IEnumerator ShipDefeatedSequence()
    {
        yield return new WaitForSeconds(2.0f);

        m_defeatPanel.SetActive(true);

        StartCoroutine(LoadMenuAfterDelay());
    }

    private void Timer()
    {
        if (m_timerIsRunning && !m_gameFinished)
        {
            if (m_timeRemaining > 0)
            {
                m_timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                m_timeRemaining = 0;
                UpdateTimerText();
                m_timerIsRunning = false;
                Victory();
            }
        }
    }

    private void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(m_timeRemaining / 60);
        float seconds = Mathf.FloorToInt(m_timeRemaining % 60);
        m_timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
