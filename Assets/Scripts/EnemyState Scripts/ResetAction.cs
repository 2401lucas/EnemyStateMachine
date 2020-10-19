using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Actions/Reset")]
public class ResetAction : Action
{
    public override void Act(StateController controller)
    {
        Reset(controller);
    }

    private void Reset(StateController controller)
    {
        controller.navMeshAgent.destination = GameManager.Instance.enemyResetTransform.position;        
    }
}
