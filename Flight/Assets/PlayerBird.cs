using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerBird : Bird
{
    protected Camera cam;
    Vector3 rotation = Vector3.zero;
    float roll = 0;
    AIBird AIPrefab;
    public List<AIBird> AI = new List<AIBird>();
    protected override void Start()
    {
        Player = this;
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
        base.Start();
        cam = GetComponentInChildren<Camera>();
        soaring = !soaring;
        cam.transform.localPosition = soaring ? new Vector3(0, 0.6f, -1.6f) : new Vector3(0, 1, -2.5f);
        rb.useGravity = true;
        AIPrefab = Resources.Load<AIBird>("AIBirdPrefab");
        tag = "Player";
        rb.angularDrag = 100;

    }

    void SpawnAI()
    {
        while (AI.Count < 30)
        {
            Vector3 direction = new Vector3(Random.value * 360 - 180, 0, Random.value * 360 - 180).normalized * (Random.value * 300);
            Vector3 position = transform.position + direction;
            position.y = AIBird.minAlt + AIBird.maxAlt * Random.value;
            AIBird aIBird = Instantiate(AIPrefab, position, Quaternion.identity);
            aIBird.name = "AI" + AI.Count;
            AI.Add(aIBird);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AI")
        {
            AI.Remove(collision.gameObject.GetComponent<AIBird>());
            Debug.Log("COLLISION : " + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
    private void FixedUpdate()
    {

        velocityMarker.transform.position = transform.position + rb.velocity / 45;
        float vertical = Input.GetAxis("Vertical");//pitch
        float horizontal = Input.GetAxis("Horizontal");//roll
        float yaw = Input.GetAxis("Yaw");
        if(AI.Count < 30)
            SpawnAI();
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            soaring = !soaring;
            yawRot = 0;
            cam.transform.localPosition = soaring ? new Vector3(0, 0.6f, -1.6f) : new Vector3(0, 1, -2.5f);
        }
        if (!soaring)
        {
            if (rb.drag < 0.7f)
            {
                rb.drag = 0.8f;
            }

            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);//snap rotation
            rb.AddForce((transform.forward * vertical).normalized * flapThrust);//forward/backwards movement
            transform.Rotate(new Vector3(0, horizontal, 0));
            if (Input.GetKey(KeyCode.Space))//up
            {
                rb.AddForce((transform.up).normalized * flapLift);

            }
            else if (Input.GetKey(KeyCode.LeftControl))//down
            {
                rb.AddForce(-(transform.up).normalized * flapLift);

            }
            if (rb.velocity.magnitude > maxVelocity / 2)//clip speeds
            {
                rb.velocity = rb.velocity.normalized * maxVelocity / 2;
            }
        }
        else
        {
            if (rb.drag > 0.4f)
            {
                rb.drag = 0.4f;
            }
            if(Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up.normalized * flapLift);
            }
            float angle = Vector3.Angle(Vector3.up, transform.forward);
            roll = -horizontal;
            rotation.y = Input.GetAxis("Mouse X");
            rotation.x = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.forward, 5 * roll);
            transform.Rotate(Vector3.up, 5 * Input.GetAxis("Mouse X"));
            transform.Rotate(Vector3.right, 5 * rotation.x);
            if (rb.velocity.magnitude < flapThrust/2 && vertical < 0)
            {
                rb.velocity = Vector3.zero;
            }
            else
                rb.AddForce((transform.forward * vertical).normalized * flapThrust);
            if (rb.velocity.magnitude > maxVelocity)
                rb.velocity = rb.velocity.normalized * maxVelocity;
            rb.AddForce(transform.up.normalized * lift * (rb.velocity.magnitude/maxVelocity + 1/3));
            //float dist = 400;
            //foreach(AIBird bird in AI)
            //{
            //    if((transform.position - bird.transform.position).magnitude <= dist)
            //    {
            //        dist = (transform.position - bird.transform.position).magnitude;
            //    }
            //}
            //Debug.Log(dist);
        }
    }
}
