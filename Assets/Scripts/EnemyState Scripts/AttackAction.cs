using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        if (Vector3.Distance(controller.chaseTarget.position, controller.transform.position) < 3)
        {
            if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate))
            {
                controller.chaseTarget.SendMessage("TakeDamage", controller.enemyStats.attackDamage);
            }
        }
    }
}
