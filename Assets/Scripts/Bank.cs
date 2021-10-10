using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int _startingBalance = 150;
    [SerializeField] int _currentBalance;

    [SerializeField] TextMeshProUGUI _goldText;

    public int CurrentBalance { get { return _currentBalance; } }

    private void Awake()
    {
        _currentBalance = _startingBalance;
        GoldToText();
    }

    public void Deposite(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        GoldToText();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        GoldToText();

        if (_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void GoldToText()
    {
        _goldText.text ="GOLD: " + _currentBalance.ToString();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
