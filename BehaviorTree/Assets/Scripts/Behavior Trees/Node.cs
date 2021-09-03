using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    RUNNING,SUCCES,FAILURE
}


[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;

    public NodeState nodeState {get { return _nodeState; } }

    public abstract NodeState Evaluete();

}
