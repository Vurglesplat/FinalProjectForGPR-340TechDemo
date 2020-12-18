using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class EvaluationTree
{

    [HideInInspector] public BehaviourDecisionSystem behaveSys;

    
    public List<BehaviorSnippet> availableActions = new List<BehaviorSnippet>();
    //private void Method<BehaviorSnippet>(List<BehaviourSnippet> foos)
    public bool[] currActionsShortlist = new bool[(int)UtilityValues.TOTAL_UTILITY_VALUES];


    public void setup(GameObject TV)
    {
        for (int i = 0; i < (int)UtilityValues.TOTAL_UTILITY_VALUES; i++)
        {
            currActionsShortlist[i] = false;
        }

        currActionsShortlist[(int)UtilityValues.WATCHING_TV] = true;
        availableActions.Add(new WatchTV(TV));
    }

    public BehaviorSnippet FindCurrentAction()
    {
        BehaviorSnippet currentHighest = null;
        foreach(BehaviorSnippet current in availableActions)
        {
            if (currentHighest == null)
            {
                currentHighest = current;
            }

            if (current.utilityValue > currentHighest.utilityValue)
            {
                currentHighest = current;
            }
        }

        return currentHighest;
    }

    public void UpdateTree()
    {

    }
}

