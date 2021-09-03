using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyAI ai;

    public ShootNode(NavMeshAgent agent, EnemyAI ai)
    {
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluete()
    {
        agent.isStopped = false;
        ai.SetColor(Color.yellow);
        return NodeState.RUNNING;
    }
}
