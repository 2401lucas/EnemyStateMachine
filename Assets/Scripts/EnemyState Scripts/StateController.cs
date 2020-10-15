using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public EnemyStats enemyStats;
    public State remainState;
    //Create an Empty GameObject and position infront of Enemy for calculations
    public Transform eyes;


    [HideInInspector] public NavMeshAgent navMeshAgent;
    //[HideInInspector] public Complete.TankShooting tankShooting;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetupAI();
    }

    public void SetupAI(/*bool aiActivationFromGameManager, List<Transform> wayPointsFromGameManager*/)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("PatrolPoint");
        for (int i = 0; i < go.Length; i++)
        {
            wayPointList.Add(go[i].transform);

        }
        print("Waypoint list count" + wayPointList.Count);
        aiActive = true; //aiActivationFromGameManager;
        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
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