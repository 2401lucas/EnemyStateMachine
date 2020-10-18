using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStateMachine/Decisions/Look")]
public class LookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;

        //Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);
        //if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange) && hit.collider.CompareTag("Player"))
        //{
        //    controller.chaseTarget = hit.transform;
        //    return true;
        //}
        int layerMask = 1 << 8;
        Collider[] hitColliders = Physics.OverlapSphere(controller.eyes.position, controller.enemyStats.areaSphereOverlapRadius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.DrawRay(controller.eyes.position + new Vector3(0, 1, 0), (hitCollider.transform.position - controller.transform.position).normalized * controller.enemyStats.lookRange, Color.green);
                if (Physics.Raycast(controller.eyes.position + new Vector3(0, 1, 0), (hitCollider.transform.position - controller.transform.position).normalized, out hit, layerMask) && hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player Spotted");
                    controller.chaseTarget = hitCollider.transform;
                    return true;
                }
                else
                    Debug.Log("No line of sight");
            }
        }
        return false;
    }
}

//private bool Look(StateController controller)
//{
//RaycastHit hit;
//Debug.DrawRay(controller.eyes.position, controller.eyes.forward * controller.enemyStats.lookRange, Color.green);

//if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange) && hit.collider.CompareTag("Player"))
//{
//    RaycastHit tempHit;
//    if (Physics.Raycast(controller.eyes.position, (hit.transform.position - controller.transform.position).normalized, out tempHit) &&
//        tempHit.collider.CompareTag("Player"))
//    {

//        Debug.Log("Spotted With Raycast");
//        controller.chaseTarget = hit.transform;
//        return true;
//    }
//}

//hit = new RaycastHit();

//Collider[] hitColliders = Physics.OverlapSphere(controller.eyes.position, controller.enemyStats.areaSphereOverlapRadius);
//    foreach (var hitCollider in hitColliders)
//{
//    if (hitCollider.CompareTag("Player"))
//    {
//        if (Physics.Raycast(controller.eyes.position, (hitCollider.transform.position - controller.transform.position).normalized, out hit) && hit.collider.CompareTag("Player"))
//        {
//            Debug.Log("Player Spotted");
//            controller.chaseTarget = hitCollider.transform;
//            return true;
//        }
//        else
//            Debug.Log("No line of sight");
//    }
//}
//return false;
//}
//}