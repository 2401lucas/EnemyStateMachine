using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStates{Patrol, Attack, Search, Chase}
    public EnemyStates enemyState = EnemyStates.Patrol;

    private GameObject[] patrolPoints;
    private int currentPointOfPatrol = 0;

    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 lastTarget = new Vector3(0, 0, 0);
    public Vector3 lastPlayerTransform = new Vector3(0,0,0);

    public Material[] colours;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyState == EnemyStates.Patrol || enemyState == EnemyStates.Search || enemyState == EnemyStates.Attack)
        {
            bool foundPlayer = false;
            Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale * 4, Quaternion.identity);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].CompareTag("Player"))
                {
                    enemyState = EnemyStates.Chase;
                    foundPlayer = true;
                    lastPlayerTransform = hitColliders[i].transform.position;
                    target = lastPlayerTransform;
                    break;
                }
            }
            if (!foundPlayer && enemyState == EnemyStates.Attack)
            {
                enemyState = EnemyStates.Search;
                target = lastPlayerTransform;
            }
        }

        switch (enemyState)
        {
            case EnemyStates.Patrol:
                //Sets the colour
                mr.material = colours[0];
                //Gets a random patrolPoint and sets the target
                if (target == Vector3.zero)
                {
                    while (true)
                    {
                        target = patrolPoints[Random.Range(0, patrolPoints.Length)].transform.position;

                        if (target != lastTarget)
                        {
                            lastTarget = target;
                            break;
                        }
                    }
                }

                //If within range of the checkpoint, remove target
                if (Vector3.Distance(target, transform.position) < 3)
                {
                    target = Vector3.zero;
                }
                break;
            case EnemyStates.Chase:
                //Sets the colour
                mr.material = colours[1];
                //If enemy is out of range, switch to search state
                if (Vector3.Distance(target, transform.position) > 40)
                {
                    target = Vector3.zero;
                    enemyState = EnemyStates.Search;
                }
                //If enemy is in range to attack, switch to attack state
                else if (Vector3.Distance(target, transform.position) < 1 )
                {
                    enemyState = EnemyStates.Attack;
                }
                break;
            case EnemyStates.Search:
                //Sets the colour
                mr.material = colours[2];
                if (Vector3.Distance(lastPlayerTransform, transform.position) < 3)
                {
                    target = Vector3.zero;
                    enemyState = EnemyStates.Patrol;
                }

                break;
            case EnemyStates.Attack:
                //Sets the colour
                mr.material = colours[3];
                if (Vector3.Distance(target, transform.position) > 1)
                {
                    enemyState = EnemyStates.Chase;
                }
                break;
        }

        Debug.Log("Enemy State: " + enemyState.ToString());
        if (target != null)
        {
            GetComponent<NavMeshAgent>().SetDestination(target);
            Debug.Log("Target Position: " + target);
        }
    }
}
