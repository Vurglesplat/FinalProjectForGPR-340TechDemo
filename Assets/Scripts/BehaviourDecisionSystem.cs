using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourDecisionSystem : MonoBehaviour
{

    //[SerializeField] GameObject heldWheat = null;
    [SerializeField] GameObject TV;
    [SerializeField] GameObject oven;
    [SerializeField] GameObject wheatfield;
    [SerializeField] UnityEngine.UI.Slider hungerSlider;
    [SerializeField] UnityEngine.UI.Slider energySlider;
    [Space] [Space]
    [SerializeField] EvaluationTree evalTree = new EvaluationTree(); 
    [Space] [Space]
    [Range(0.0f,1.0f)] public float hunger = 1.0f;
    [Range(0.0f, 1.0f)] public float energy  = 1.0f;

    [SerializeField] float hungerFallRate = 0.0f;
    [SerializeField] float energyFallRate = 0.0f;

    [HideInInspector] public NPCMovementScript movementScript;
    
    bool isAwake = true;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = this.gameObject.GetComponent<NPCMovementScript>();
        evalTree.behaveSys = this;
        evalTree.setup(TV);
    }

    // Update is called once per frame
    void Update()
    {
        checkForNewActionConditions();

        updateStatValues();
        BehaviorSnippet currentSnippet = evalTree.FindCurrentAction();

        if (currentSnippet != null)
        {
            currentSnippet.updateBehavior(this.gameObject);
            movementScript.targetObj = currentSnippet.target;
        }
    }

    void updateStatValues()
    {
        // decay values
        if (isAwake)
        {
            hunger -= hungerFallRate * 0.0001f;
            energy -= energyFallRate * 0.0001f;
        }


        //update sliders
        hungerSlider.value = hunger;
        energySlider.value = energy;
    }
    void checkForNewActionConditions()
    {
        if (hunger < 0.7f)
        {
            Debug.Log("I am Hungry");

            if (GameObject.FindGameObjectWithTag("Bread"))
            {
                Debug.Log("Got Bread, will eat.");
                if (evalTree.currActionsShortlist[(int)UtilityValues.GET_BREAD] == false)
                {
                    evalTree.currActionsShortlist[(int)UtilityValues.GET_BREAD] = true;
                    evalTree.availableActions.Add(new GetBread(GameObject.FindGameObjectWithTag("Bread")));
                }
            }
            else
            {
                Debug.Log("No bread found, I'll make some");
                if (evalTree.currActionsShortlist[(int)UtilityValues.MAKE_BREAD] == false)
                {
                    evalTree.currActionsShortlist[(int)UtilityValues.MAKE_BREAD] = true;
                    evalTree.availableActions.Add(new MakeBread(oven, wheatfield));
                }
            }
        }
        else
        {
            Debug.Log("No longer hungry");
            if (evalTree.currActionsShortlist[(int)UtilityValues.MAKE_BREAD] || evalTree.currActionsShortlist[(int)UtilityValues.GET_BREAD])
            {
                evalTree.availableActions.RemoveAll(isHungerRelated);
                evalTree.currActionsShortlist[(int)UtilityValues.MAKE_BREAD] = false;
                evalTree.currActionsShortlist[(int)UtilityValues.GET_BREAD] = false;

            }

        }
    }

    static bool isHungerRelated (BehaviorSnippet x)
    {
        if (x.utilityValue == (int)UtilityValues.MAKE_BREAD || x.utilityValue == (int)UtilityValues.GET_BREAD)
            return true;
        else
            return false;
    }
}
