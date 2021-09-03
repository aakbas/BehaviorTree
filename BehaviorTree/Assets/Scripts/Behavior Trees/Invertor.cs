using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invertor : Node
{

    protected Node node;

    public Invertor(Node node)
    {
        this.node = node;
    }




    public override NodeState Evaluete()
    {

        switch (node.Evaluete())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCES:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCES;
                break;
            default:
                break;

        }
        return _nodeState;
    }

}
