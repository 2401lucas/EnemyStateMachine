using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Decisions/Arrived")]
public class ArrivedDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Arrived(controller);
        return targetVisible;
    }

    private bool Arrived(StateController controller)
    {
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
            !controller.navMeshAgent.pathPending)
            return true;
        else
            return false;
    }
}
