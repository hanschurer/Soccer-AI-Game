
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace soccerAI {
public class Player : MonoBehaviour
{

        private SoccerAgent agent;
        private Ball Ball;
        private Vector3 ballLocation;
        private Vector3 agentLocation;
        private Vector3 targetLocation;
        private bool isLeft;

        void Start()
        {
            agent = GetComponent<SoccerAgent>();
            Ball = agent.getBall().GetComponent<Ball>();
            isLeft = agent.WhichTeam();
        }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Kickball();
    }

    void Move(float h, float v) {

        transform.Translate((Vector3.forward * h + Vector3.right * -v) * 15f * Time.deltaTime);

    }

    void Kickball() {
            ballLocation = agent.getBallPosition();
            agentLocation = agent.transform.position;
            if (Condition.CanKickBall(agentLocation, ballLocation))
            {
                //Depending on the team, give the ball a force;
                if (isLeft)
                {
                    Ball.AddForce(ballLocation, Define.RightDoorPosition);
                }
                else
                {
                    Ball.AddForce(ballLocation, Define.LeftDoorPosition);
                }
            }
        }
}

}

