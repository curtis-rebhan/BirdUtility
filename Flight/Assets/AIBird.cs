using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBird : Bird
{
    [SerializeField]
    PlayerBird player;
    float threatRange = 20;
    public static float minAlt = 50, maxAlt = 100;
    int state = 0;
    float time = 0;
    public Vector3 wanderVector;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb.drag = 0.5f;
        player = Player;
        maxVelocity = 20;
        //BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        //boxCollider.size = new Vector3(2.19f * 2, 0.16f * 2, 0.75f * 2);
        //boxCollider.isTrigger = true;
        tag = "AI";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Wander(float t)
    {
        time = t;
        wanderVector = new Vector3(0, Random.value * 360 - 180, 0);
        Wander();
    }
    void Wander()
    {
        if(time <= 0)
        {
            return;
        }
        time -= Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(wanderVector), 5);
        rb.AddForce(transform.forward.normalized * flapThrust);
    }
    void PickNewAction()
    {
        Wander(Random.value * 10);
    }
    void Flee()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized;
        if (transform.position.y < minAlt)
        {
            dir = new Vector3(-45, dir.y, dir.z);
        }
        else if (transform.position.y < maxAlt)
        {
            dir = new Vector3(0, dir.y, dir.z);

        }
        else if (transform.position.y > maxAlt)
        {
            dir = new Vector3(45, dir.y, dir.z);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 2);
        rb.AddForce(dir * flapThrust/2);
    }
    private void OnDestroy()
    {
        player.AI.Remove(this);
    }
    private void FixedUpdate()
    {
        if (player != null && (transform.position - player.transform.position).magnitude < threatRange)
        {
            Flee();
        }
        else if (time > 0)
        {
            Wander();
        }
        else
        {
            PickNewAction();
        }
        if ((transform.position - Player.transform.position).magnitude > 600)
            Destroy(gameObject);
        if(transform.position.y < minAlt)
        {
            wanderVector = new Vector3(-45, wanderVector.y, wanderVector.z);
        }
        else if(transform.position.y < maxAlt)
        {
            wanderVector = new Vector3(0, wanderVector.y, wanderVector.z);

        }
        else if(transform.position.y > maxAlt)
        {
            wanderVector = new Vector3(45, wanderVector.y, wanderVector.z);
        }
        if((transform.position - Player.transform.position).magnitude <= 8)
        {
            Destroy(gameObject);
            Debug.Log("Got 'Em");
        }
        //if (rb.velocity.magnitude > 0.1f)
        //    transform.LookAt(transform.position + rb.velocity.normalized);
        //if (rb.velocity.magnitude > maxVelocity)
        //    rb.velocity = rb.velocity.normalized * maxVelocity;
    }
}
