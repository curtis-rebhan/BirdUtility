using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBird : Bird
{
    protected Camera cam;
    protected override void Start()
    {
        base.Start();
        cam = GetComponentInChildren<Camera>();
    }
    private void FixedUpdate()
    {

        velocityMarker.transform.position = transform.position + rb.velocity / 45;
        float vertical = Input.GetAxis("Vertical");//pitch
        float horizontal = Input.GetAxis("Horizontal");//roll
        float yaw = Input.GetAxis("Yaw");
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
            if (rb.velocity.magnitude > 0.1f)
                transform.LookAt(transform.position + rb.velocity.normalized);
            yawRot -= horizontal;
            transform.Rotate(new Vector3(0, 0, yawRot));

            if (rb.drag > 0.4f)
            {
                rb.drag = 0.4f;
            }
            Vector3 rotation = Vector3.zero;
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            if (rb.velocity.magnitude < flapThrust && vertical < 0)
            {
                rb.velocity = Vector3.zero;
            }
            else
                rb.AddForce((transform.forward * vertical).normalized * flapThrust);
            rb.AddForce((transform.right * rotation.y).normalized * LookSpeed * rb.velocity.magnitude);
            rb.AddForce((transform.up * rotation.x).normalized * LookSpeed * rb.velocity.magnitude);
            Debug.Log(rb.velocity.magnitude);
        }
    }
}
