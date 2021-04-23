using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;

public enum mTeam
{
    Red = 1,
    Blue = 0
}

public class MyAI : Agent
{

    [HideInInspector]
    float kickPower;
    public Team team;
    public Vector3 iniPos;
    public Rigidbody rb;
    float HorizontalSpeed;
    float VerticalSpeed;
    float score=1f/5000;
    public float rotSign;
    BehaviorParameters behaviorParameters;
    public override void Initialize()
    {

        behaviorParameters = gameObject.GetComponent<BehaviorParameters>();

        if (behaviorParameters.TeamId == (int)Team.Red)
        {
            team = Team.Red;
            iniPos = new Vector3(transform.position.x - 3f, 1.1f, transform.position.z);
        }
        else {
            team = Team.Blue;
            iniPos = new Vector3(transform.position.x + 3f, 1.1f, transform.position.z);
        }

        HorizontalSpeed = 0.4f;
        VerticalSpeed = 1.1f;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 500;
        
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var moveDirection = Vector3.zero;
        var rotateDirection = Vector3.zero;
        kickPower = 0f;
        var act = actionBuffers.DiscreteActions;
        var forward = act[0];
        var right = act[1];
        var rotate = act[2];

        switch (forward)
        {
            case 1:
                moveDirection = transform.forward * VerticalSpeed;
                kickPower = 1f;
                break;
            case 2:
                moveDirection = transform.forward * -VerticalSpeed;
                break;
        }
        switch (right)
        {
            case 1:
                moveDirection = transform.right * HorizontalSpeed;
                break;
            case 2:
                moveDirection = transform.right * -HorizontalSpeed;
                break;
        }
        switch (rotate)
        {
            case 1:
                rotateDirection = transform.up * -1f;
                break;
            case 2:
                rotateDirection = transform.up * 1f;
                break;
        }
        transform.Rotate(rotateDirection, Time.deltaTime * 100f);
        AddReward(-score);
        //2f is the agent run speed
        rb.AddForce(moveDirection * 2f, ForceMode.VelocityChange);
    }

    // ball kick function
    private void OnCollisionEnter(Collision collision)
    {
        var force = 2000f * kickPower;
        if (collision.gameObject.CompareTag("ball"))
        {
            AddReward(score);
            var direction = collision.contacts[0].point - this.transform.localPosition;
            direction = direction.normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * force);
        }
    }

    // Heuristic function is copy from the ML-agent soccer sample
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteout = actionsOut.DiscreteActions;
        discreteout.Clear();
   
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            discreteout[0] = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            discreteout[0] = 2;
        }
        //rotate
        if (Input.GetKey(KeyCode.A))
        {
            discreteout[2] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            discreteout[2] = 2;
        }
        //right
        if (Input.GetKey(KeyCode.E))
        {
            discreteout[1] = 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            discreteout[1] = 2;
        }
    }

}
