using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHitPoint = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int _difficultyRamp;

    int _currentHitPoints = 0;

    Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        _currentHitPoints = _maxHitPoint;
    }


    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;

        if (_currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            _maxHitPoint += _difficultyRamp;
            if(_enemy == null) { return; }
            _enemy.RewardGold();
        }
    }
}
