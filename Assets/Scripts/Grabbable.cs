﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public enum Handedness {Left, Right, None}

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {

    private Rigidbody rb;
    private Grabber myHand;
    private SnapOnGrab snap;

    public Handedness handedness;
    public UnityEvent onSpecialGrab;
    public UnityEvent onSpecialDrop;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        snap = GetComponent<SnapOnGrab>();
    }
    
    public void OnGrab(Grabber hand) {
        
        if (myHand && hand != myHand)
            OnDrop(myHand);
        
        transform.parent = hand.transform;
        handedness = hand.handedness;
        myHand = hand;
        rb.isKinematic = true;

        onSpecialGrab?.Invoke();
        
        if (snap) //returns false if snap == null
            snap.Snap();

    }

    public void OnDrop(Grabber hand) {

        handedness = Handedness.None;
        myHand = null;
        hand.grabbedTransform = null;
        rb.isKinematic = false;
        transform.parent = null;
        
        onSpecialDrop?.Invoke();
        
    }


    
}