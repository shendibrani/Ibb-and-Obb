using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Linq;

public class EnemyController : WaypointSystem //should probably just a be a composite instead of inheritance
{
    private Transform _enemySpirit;

    private void Start()
    {
        base.Start();
        _enemySpirit = transform.GetChild(1);
    }

    void Update()
    {
        base.Update();
        MirrorMovement();
    }

    private void MirrorMovement()
    {
        _enemySpirit.transform.position = new Vector3(TargetToMove.position.x, -TargetToMove.position.y, 0);
    }
}
