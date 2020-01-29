using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetorSpot : MonoBehaviour
{
    MeteorConfig meteorConfig;
    List<Transform> waypoints;
    int waypointindex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = meteorConfig.GetWaypoints();
        transform.position = waypoints[waypointindex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig( MeteorConfig meteorConfig)
    {
        this.meteorConfig = meteorConfig;
    }

    private void Move()
    {
        if (waypointindex <= waypoints.Count - 1)
        {
            //    var targetPosition = waypoints[waypointindex].transform.position;
            //    var movementThisFrame = meteorConfig.GetMoveSpeed() * Time.deltaTime;
            //    transform.position = Vector2.MoveTowards
            //        (transform.position, targetPosition, movementThisFrame);
            //    if (transform.position == targetPosition)
            //    {
            //        waypointindex++;
            //    }
            //}
            //else
            //    Destroy(gameObject);

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -meteorConfig.GetMoveSpeed());
        }
    }
}
