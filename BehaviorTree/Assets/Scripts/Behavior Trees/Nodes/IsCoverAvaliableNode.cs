using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoverAvaliableNode : Node
{
    private Cover[] avaliableCovers;
    private Transform target;
    private EnemyAI ai;

    public IsCoverAvaliableNode(Cover[] avaliableCovers, Transform target, EnemyAI ai)
    {
        this.avaliableCovers = avaliableCovers;
        this.target = target;
        this.ai = ai;
    }

    public override NodeState Evaluete()
    {
        Transform bestSpot = FindCoverSpot();
        ai.SetBestCoverSpot(bestSpot);
        return bestSpot != null ? NodeState.SUCCES : NodeState.FAILURE;
    }

    private Transform FindCoverSpot()
    {
        float minAngle = 90f;
        Transform bestSpot = null;
       
        for (int i = 0; i < avaliableCovers.Length; i++)
        {
            Transform bestSpotInCover = FinBestSpotInCover(avaliableCovers[i], ref minAngle);
            if (bestSpotInCover!=null)
            {
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;

    }

    private Transform FinBestSpotInCover(Cover cover, ref float minAngle)
    {
        Transform[] avaliableSpots = cover.GetCoverSpots();
        Transform bestspot = null;
        

        for (int i = 0; i < avaliableSpots.Length; i++)
        {
            if (CheckSpotIsValid(avaliableSpots[i]))
            {
                Vector3 direction = target.position - avaliableSpots[i].transform.position;
                float angle = Vector3.Angle(avaliableSpots[i].forward, direction);
                if (angle<minAngle)
                {
                    bestspot = avaliableSpots[i];
                }
            }
        }
        return bestspot;

    }

    private bool CheckSpotIsValid(Transform spot)
    {
        RaycastHit hit;
        Vector3 direction = target.position - spot.position;
        if (Physics.Raycast(spot.position,direction,out hit))
        {
            if (hit.collider.transform!=target)
            {
                return true;
            }
        }
        return false;
    }
}
