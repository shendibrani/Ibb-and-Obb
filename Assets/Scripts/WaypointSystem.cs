using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointSystem : MonoBehaviour {

    public Vector3[] Waypoints;
    public float Lerp;
    public float SatisfactoryDistanceToWaypoint;

    protected Transform TargetToMove;

    private bool[] _hasReachedWaypoint;
    private Vector3 _startPosition;
    private Vector3 _endWaypoint;
    
    // Use this for initialization
    public void Start()
    {
        _hasReachedWaypoint = new bool[Waypoints.Length];
        _startPosition = transform.position;
        
        TargetToMove = transform.GetChild(0);
    }
    
    public void Update()
    {
        int nextWaypointIndex = _hasReachedWaypoint.ToList().FindIndex(x => x == false);

        for (int i = 0; i < Waypoints.Length; i++)
        {
            if (i != nextWaypointIndex)
                continue;

            TargetToMove.transform.position = Vector2.Lerp(new Vector2(TargetToMove.transform.position.x, TargetToMove.transform.position.y),
                new Vector2(Waypoints[i].x, Waypoints[i].y), Lerp * Time.deltaTime);

            if (Vector3.Distance(TargetToMove.transform.position, Waypoints[i]) < SatisfactoryDistanceToWaypoint)
            {
                if (i == Waypoints.Length - 1 && Waypoints[i] != _startPosition)
                {
                    _endWaypoint = Waypoints[i];
                    Waypoints[i] = _startPosition;
                    break;
                }

                _hasReachedWaypoint[i] = true;
            }
        }

        if (_hasReachedWaypoint[_hasReachedWaypoint.Length - 1])
        {
            _hasReachedWaypoint = new bool[Waypoints.Length];
            Waypoints[_hasReachedWaypoint.Length - 1] = _endWaypoint;
        }
    }

    private void EaseInto(Vector2 position)
    {
        Vector2 currentPos = new Vector2(TargetToMove.transform.position.x, TargetToMove.transform.position.y);
        Vector2 smoothedPosition = Vector2.Lerp(currentPos, position, Lerp * Time.deltaTime);
        TargetToMove.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, TargetToMove.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 thisPoint in Waypoints)
        {
            Debug.DrawLine(thisPoint.AddVectorX(-0.5f), thisPoint.AddVectorX(0.5f), Color.red);
            Debug.DrawLine(thisPoint.AddVectorY(-0.5f), thisPoint.AddVectorY(0.5f), Color.red);
        }
    }
}