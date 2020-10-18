using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Actions/Search")]
public class SearchAction : Action
{
    public override void Act(StateController controller)
    {
        Search(controller);
    }

    private void Search(StateController controller)
    {
        controller.navMeshAgent.isStopped = false;
    }
}
