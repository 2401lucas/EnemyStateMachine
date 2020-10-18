using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStates{Patrol, Chase, Attack, Search }
    public EnemyStates enemyState;

    //Information For Patrolling
    private GameObject[] patrolPoints;
    private int currentPointOfPatrol = 0;

    //Target
    private Vector3 target = new Vector3(0, 0, 0);
    private Vector3 lastTarget = new Vector3(0, 0, 0);
    public Vector3 lastPlayerTransform = new Vector3(0,0,0);

    //Player
    private GameObject Player;

    //Debug
    public Material[] colours;
    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        Player = GameManager.Instance.player;

    }

    // Update is called once per frame
    void Update()
    {
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
        }

        Debug.Log("Enemy State: " + enemyState.ToString());
        if (target != null)
        {
            GetComponent<NavMeshAgent>().SetDestination(target);
            Debug.Log("Target Position: " + target);
        }
    }
}
