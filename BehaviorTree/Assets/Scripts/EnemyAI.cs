using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healtRestoreRate;
   

    [SerializeField] private float chasingRange;
    [SerializeField] private float _shootingRange;
    [SerializeField] private Transform playerTransform;

   

    [SerializeField] private Cover[] avaliableCovers;

    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;
    private float _currentHealth;
    private Node topNode;

    public float currentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }
    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _currentHealth = startingHealth;        
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(avaliableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        IsCoveredNode ýsCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRange= new RangeNode(_shootingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent,this);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRange, shootNode});
        
        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode});
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });

        Selector tryToTakeCoverSelector = new Selector(new List<Node> { ýsCoveredNode, findCoverSelector});

        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode,tryToTakeCoverSelector});

        topNode = new Selector(new List<Node> {mainCoverSequence,shootSequence,chaseSequence});

    }

    void Update()
    {
        topNode.Evaluete();
        if (topNode.nodeState==NodeState.FAILURE)
        {
            SetColor(Color.red);
        }
        currentHealth += Time.deltaTime * healtRestoreRate;
    }

    

    public void SetColor(Color color)
    {
        material.color = color;
    }

    public void SetBestCoverSpot(Transform bestSpot)
    {
        this.bestCoverSpot = bestSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot.transform;
    }

    private void OnMouseDown()
    {
        currentHealth -= 10f;
    }
}
