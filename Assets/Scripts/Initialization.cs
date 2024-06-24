using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Initialization : MonoBehaviour
{
    [NonSerialized] public GameObject Player;
    [NonSerialized] public Rigidbody PlayerRb;
    [NonSerialized] public GroundedController PlayerGroundedController;

    private void Init() { Player = FindObjectOfType<VerticalPlayerMovement>().gameObject;
                          PlayerRb = Player.GetComponent<Rigidbody>();
                          PlayerGroundedController = Player.transform.GetChild(0).GetComponent<GroundedController>(); }

    private void Awake() { Init(); }
}
