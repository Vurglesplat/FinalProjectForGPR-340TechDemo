using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldWheatScript : MonoBehaviour
{
    public GameObject heldWheat;
    // Start is called before the first frame update
    void Start()
    {
        heldWheat.SetActive(false);
    }
}
