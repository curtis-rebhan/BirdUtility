using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    protected Rigidbody rb;
    protected float lift = 9f, flapLift = 15, flapThrust = 15, maxVelocity = 33, LookSpeed = 1.0f;
    protected float yawRot = 0;
    protected GameObject velocityMarker;
    protected bool soaring = false;
    protected static PlayerBird Player;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        velocityMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        velocityMarker.transform.parent = transform;
        velocityMarker.transform.localScale = Vector3.one * 0.1f;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.drag = 0.8f;
        rb.angularDrag = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}