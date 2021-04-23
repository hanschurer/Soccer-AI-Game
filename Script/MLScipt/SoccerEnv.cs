using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class SoccerEnv : MonoBehaviour
{
    [System.Serializable]
    public class AgentInfo
    {
        public MyAI Agent;
        [HideInInspector]
        public Vector3 originPos;
        [HideInInspector]
        public Quaternion originRot;
        [HideInInspector]
        public Rigidbody Rb;
    }

    public GameObject ball;
    [HideInInspector]
    public Rigidbody ballRb;
    Vector3 oriPosofBall;
   
    //List of Agents On Platform
    public List<AgentInfo> AgentsList = new List<AgentInfo>();
    private SimpleMultiAgentGroup TeamBlue;
    private SimpleMultiAgentGroup TeamRed;
    private int timer;

    void Start()
    {

        TeamRed = new SimpleMultiAgentGroup();
        TeamBlue = new SimpleMultiAgentGroup();

        ballRb = ball.GetComponent<Rigidbody>();
        oriPosofBall = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);


        foreach (var item in AgentsList)
        {
            item.originPos = item.Agent.transform.position;
            item.originRot = item.Agent.transform.rotation;
            item.Rb = item.Agent.GetComponent<Rigidbody>();

            if (item.Agent.team == Team.Red)
            {
                TeamRed.registerAgent(item.Agent);
            }
            else
            {
                TeamBlue.RegisterAgent(item.Agent);
            }
        }
        ResetScene();
    }

    void FixedUpdate()
    {
        timer += 1;

        if (timer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            TeamBlue.GroupEpisodeInterrupted();
            TeamRed.GroupEpisodeInterrupted();
            ResetScene();
        }
    }


    public void ResetBall()
    {
        ball.transform.position = oriPosofBall + new Vector3(Random.Range(-2.5f, 2.5f), 0f, Random.Range(-2.5f, 2.5f)); ;
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }

    public void GoalTouched(Team scoredTeam)
    {
        if (scoredTeam == Team.Blue)
        {
            TeamBlue.AddGroupReward(1 - timer / MaxEnvironmentSteps);
            TeamRed.AddGroupReward(-1);
        }
        else
        {
            m_TeamRed.AddGroupReward(1 - timer / MaxEnvironmentSteps);
            TeamRed.AddGroupReward(-1);
        }
        TeamBlue.EndGroupEpisode();
        TeamRed.EndGroupEpisode();
        ResetScene();
    }


    public void ResetScene()
    {
        timer = 0;
        ResetBall();
    }
}
