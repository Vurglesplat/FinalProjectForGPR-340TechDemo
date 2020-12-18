using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum UtilityValues
{
    WATCHING_TV = 0,
    WASTING_ELECTRICITY,

    MAKE_BREAD,
    GET_BREAD,
    EXHAUSTED,

    TOTAL_UTILITY_VALUES
};

public class BehaviorSnippet
{
    public GameObject target;
    public int utilityValue = -1;


    public virtual void updateBehavior(GameObject character) { Debug.LogError("WARNING, DEFAULT SNIPPET UPDATE"); }

}
public class WatchTV : BehaviorSnippet
{

    public override void updateBehavior(GameObject character)
    {

        if (Vector2.Distance(target.transform.position, character.transform.position) < 0.1f)
        {
                Debug.Log("Watching TV");
        }

        if (Vector2.Distance(target.transform.position, character.transform.position) < 2.0f)
        {
            if (!target.GetComponent<TVScript>().screen.activeSelf)
            {
                Debug.Log("Turning the TV on");
                target.GetComponent<TVScript>().screen.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Going to the TV");
        }
    }

    public WatchTV(GameObject tv)
    {
        target = tv;
        utilityValue = (int)UtilityValues.WATCHING_TV;
    }
}

//public class WastingElectricity : BehaviorSnippet
//{

//    public override void updateBehavior(GameObject character)
//    {
//        if (Vector2.Distance(target.transform.position, character.transform.position) < 2.0f)
//            target.GetComponent<TVScript>().screen.SetActive(true);
//    }

//    public WastingElectricity(GameObject tv)
//    {
//        target = tv;
//        utilityValue = (int)UtilityValues.WASTING_ELECTRICITY;
//    }
//}

public class MakeBread : BehaviorSnippet
{
    GameObject targetOven, targetWheatField;
    public override void updateBehavior(GameObject character)
    {
        Debug.Log("making bread");

        if (targetOven.GetComponent<OvenScript>().isCooking)
        {
            target = targetOven;
            Debug.Log("Waiting for it to finish baking");
        }
        else
        {
            if (character.GetComponent<HeldWheatScript>().heldWheat.activeSelf)
            {
                Debug.Log("have wheat, going to the oven");
                target = targetOven;
                if (Vector2.Distance(target.transform.position, character.transform.position) < 1.0f)
                {
                    Debug.Log("Beginning to bake");
                    character.GetComponent<HeldWheatScript>().heldWheat.SetActive(false);
                    target.GetComponent<OvenScript>().startCooking();
                }
            }
            else
            {
                Debug.Log("looking for wheat");

                target = GameObject.FindGameObjectWithTag("WheatField");
                if (target == null)
                    Debug.LogError("NO WHEATFIELDS FOUND");

                if (Vector2.Distance(target.transform.position, character.transform.position) < 0.5f)
                {
                    Debug.Log("got wheat");
                    character.GetComponent<HeldWheatScript>().heldWheat.SetActive(true);
                }

            }
        }

    }

    public MakeBread(GameObject oven, GameObject wheatField)
    {
        targetOven = oven;
        targetWheatField = wheatField;
        utilityValue = (int)UtilityValues.MAKE_BREAD;
    }
}

public class GetBread : BehaviorSnippet
{

    public override void updateBehavior(GameObject character)
    {
        if (Vector2.Distance(target.transform.position, character.transform.position) < 0.5f)
        {
           Debug.Log("Eating the bread");
            GameObject.Destroy(target);
            character.GetComponent<BehaviourDecisionSystem>().hunger = 1.0f;
        }
        else
        {
            Debug.Log("going to the bread");
        }
    }

    public GetBread(GameObject bread)
    {
        target = bread;
        utilityValue = (int)UtilityValues.GET_BREAD;
    }
}
