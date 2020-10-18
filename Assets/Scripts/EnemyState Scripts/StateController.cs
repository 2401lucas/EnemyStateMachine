using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;
    public EnemyStats enemyStats;
    //Create an Empty GameObject and position infront of Enemy for calculations
    public Transform eyes;

    //Public variables that aren't needed in 
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> patrolPointList;
    [HideInInspector] public int nextPatrolPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetupAI(); //Called from a GameManager or EnemyManager script
    }

    public void SetupAI(/*List<Transform> wayPointsFromGameManager*/)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("PatrolPoint"); //Will get list from EnemyManager or GameManger
        for (int i = 0; i < go.Length; i++)
        {
            patrolPointList.Add(go[i].transform);
        }
        print("Enemy Patrol point count " + patrolPointList.Count);
        nextPatrolPoint = Random.Range(0, patrolPointList.Count);
    }

    void Update()
    {
       currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.areaSphereOverlapRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
