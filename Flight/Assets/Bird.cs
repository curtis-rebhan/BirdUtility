using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    protected Rigidbody rb;
    protected float lift = 9, flapLift = 15, flapThrust = 15, maxVelocity = 145, LookSpeed = 1.0f;
    protected float yawRot = 0;
    protected GameObject velocityMarker;
    protected bool soaring = false;
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

        #region BirdModule
        GameObject leftWing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject rightWing = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cube);
        body.transform.SetParent(transform);
        leftWing.transform.SetParent(transform);
        rightWing.transform.SetParent(transform);

        leftWing.transform.localScale = new Vector3(1, 0.1f, 0.73f);
        rightWing.transform.localScale = new Vector3(1, 0.1f, 0.73f);
        body.transform.localScale = new Vector3(0.08f, 0.19f, 0.14f);

        leftWing.transform.localPosition = new Vector3(-0.6f, 0);
        rightWing.transform.localPosition = new Vector3(0.6f, 0);
        body.transform.localPosition = Vector3.zero;
        #endregion
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(2.19f, 0.16f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}