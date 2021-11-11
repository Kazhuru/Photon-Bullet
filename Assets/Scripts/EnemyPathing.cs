using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Object variables
    private int waypointToGo;

    // Cached references
    private List<Transform> waypoints;
    private WaveConfig waveConfig;

    // Start is called before the first frame update
    void Start()
    {
        waypointToGo = 1;
        if (waveConfig != null)
        {
            waypoints = waveConfig.GetWaypoints();
            transform.position = new Vector2(waypoints.First().position.x, waypoints.First().position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveConfig != null)
        {
            if (waypointToGo <= waypoints.Count - 1)
            {
                float step = waveConfig.GetMoveSpeed() * Time.deltaTime;
                Vector2 positionToMove = waypoints[waypointToGo].position;

                transform.position = Vector2.MoveTowards(transform.position, positionToMove, step);

                if (Vector2.Distance(transform.position, positionToMove) < 0.001f)
                    waypointToGo++;
            }
            else
                Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig wave)
    {
        waveConfig = wave;
    }
}
