using UnityEngine;
using TMPro;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private Spaceship m_spaceShip;

    private TMP_Text m_healthText;

    private void Start()
    {
        m_healthText = GetComponent<TMP_Text>();
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        m_healthText.text = new String('.', m_spaceShip.CurrentHealth);
    }

}
