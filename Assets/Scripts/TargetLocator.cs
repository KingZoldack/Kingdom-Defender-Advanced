using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform _weapon;
    [SerializeField] ParticleSystem _projectileParticles;
    [SerializeField] float _range = 15f;
    Transform _target;


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon(); 
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform cloestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                cloestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = cloestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, _target.position);

        _weapon.LookAt(_target);

        if (targetDistance <= _range)
        {
            Attack(true);
        }

        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emission = _projectileParticles.emission;
        emission.enabled = isActive;
    }
}
