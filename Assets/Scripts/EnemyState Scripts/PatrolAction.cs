﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.destination = controller.patrolPointList[controller.nextPatrolPoint].position;
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            while (true)
            {
                int temp = Random.Range(0, controller.patrolPointList.Count);
                if (temp != controller.nextPatrolPoint)
                {
                    controller.nextPatrolPoint = temp;
                    break;
                }
            }
        }
    }
}