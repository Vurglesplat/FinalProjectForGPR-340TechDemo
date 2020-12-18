using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenScript : MonoBehaviour
{
    [SerializeField] GameObject breadPrefab;
    [SerializeField] GameObject grate;


    [Space]
    [Space]
    [Space]

    public bool isCooking = false;


    public float fullCookTime = 10.0f;
    [Range(0.0f, 10.0f)]public float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooking)
        {
            timeLeft -= 0.01f;
            grate.SetActive(true);
        }
        else
        {
            grate.SetActive(false);
        }

        if (timeLeft < 0.0f && isCooking == true)
        {
            finishCooking();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            startCooking();
        }
    }

    public void startCooking()
    {
        timeLeft = fullCookTime;
        isCooking = true;
    }
    public void finishCooking()
    {
        isCooking = false;
        Instantiate(breadPrefab);
    }
}
