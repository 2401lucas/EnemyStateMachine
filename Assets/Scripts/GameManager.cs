using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Player
    public GameObject player;
    private Vector3 startPos;

    //Enemy
    [HideInInspector] public Transform enemyResetTransform;

    #region Singleton
    //Singleton Instantiation
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyResetTransform = GameObject.FindGameObjectWithTag("EnemyResetPosition").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
