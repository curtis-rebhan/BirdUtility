using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBird : Bird
{
    [SerializeField]
    GameObject player;
    float threatRange = 20;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb.drag = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// returns a normalized vector away from the target
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    Vector3 Flee(Transform target)
    {
        return (transform.position - target.position).normalized;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.1f)
            transform.LookAt(transform.position + rb.velocity.normalized);
        if ((transform.position - player.transform.position).magnitude < threatRange)
        {
            rb.AddForce(Flee(player.transform) * flapThrust);
        }
    }
}
