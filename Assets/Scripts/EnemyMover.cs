using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] float _speed = 1f;

    Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        if (_enemy == null) { return; }
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag(Tags.PATH_TAG);

        foreach (Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

   IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            Vector3 startingPos = transform.position;
            Vector3 endingPos = waypoint.transform.position;
            float travelPercentage = 0f;

            transform.LookAt(endingPos);

            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startingPos, endingPos, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        _enemy.StealGold();
        gameObject.SetActive(false);
    }
}
