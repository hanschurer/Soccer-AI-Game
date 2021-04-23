using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace soccerAI
{
    public class PassBall : Action
    {
        
        private Ball ball;
        private SoccerAgent mAgent;
        private bool isLeft;
        private Vector3 ballLoaction;
        private SoccerAgent nearPlayer;

        
        public override void OnStart()
        {
            mAgent = GetComponent<SoccerAgent>();
            isLeft = mAgent.WhichTeam();
            ballLoaction = mAgent.getBallPosition();
            ball = mAgent.getBall().GetComponent<Ball>();
        }

        public override TaskStatus OnUpdate()
        {
            nearPlayer = AttackStrategy.Instance.findNear(mAgent, isLeft);
            if(nearPlayer.getNum() == mAgent.getNum())
            {   //if we are the closest player to the goal return failure
                return TaskStatus.Failure;
            }
            else
            {
                // if we are not the clost player to the goal.
                if (Condition.CanKickBall(mAgent.transform.position, ballLoaction))
                {
                    mAgent.transform.LookAt(ballLoaction);
                    //shoot the ball to the nearest attacker
                    ball.AddForce(ballLoaction,nearPlayer.transform.position);
                    return TaskStatus.Success;
                }
                return TaskStatus.Failure;
            }
        }
    }
}
