using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mlplayer : MonoBehaviour
{
    public float force;
    public float speed;
    public float sprintDuration;
    public float sprintSpeed;
    private bool isSprint;
    private float SprintTime;
    private Vector3 direction;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Sprint(h, v);
    }

    void Sprint(float h, float v)
    {

        transform.Translate((Vector3.forward * -h + Vector3.right * v) * speed * Time.deltaTime);

        if (!isSprint)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSprint = true;
                direction = transform.forward*-h + transform.right * v;
                direction.y = 0f;
            }
        }
        else
        {
            if (SprintTime <= 0)// reset
            {
                isSprint = false;

                SprintTime = sprintDuration;
            }
            else
            {
                SprintTime -= Time.deltaTime;
                rb.velocity = direction * SprintTime * sprintSpeed;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            var direction = collision.contacts[0].point - this.transform.localPosition;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * force);
        }
    }


}
