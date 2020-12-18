using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementScript : MonoBehaviour
{
    public GameObject targetObj = null;
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;
    BehaviourDecisionSystem decisionSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        decisionSystem = this.gameObject.GetComponent<BehaviourDecisionSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj)
        {
            Vector2 targetDifference = (targetObj.transform.position - this.transform.position);
            if (targetDifference.magnitude > 0.1)
            {
                targetDifference.Normalize();
                rb.velocity = targetDifference * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
            }
            

        }
    }
}
