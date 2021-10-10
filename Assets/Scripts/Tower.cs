using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int _cost = 75;

    public bool CreateTower(Tower _tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= _cost)
        {
            Instantiate(_tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(_cost);
            return true;
        }

        return false;
        
    }
}
