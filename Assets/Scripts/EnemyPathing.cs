using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointindex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointindex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointindex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointindex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                waypointindex++;
            }
        }
        else
            Destroy(gameObject);
    }
}
