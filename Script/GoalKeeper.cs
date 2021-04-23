using System.Collections;
/// <summary>
/// 守门员的踢球策略
/// </summary>
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace soccerAI
{
    public class GoalKeeper : Action
    {
        private Vector3 ballLoaction;
        private Vector3 agentLoaction;
        private Vector3 originPos;
        private SoccerAgent Agent;
        private Ball Ball;
        private bool isLeft;

        public override void OnStart()
        {
            Ball = Agent.getBall().GetComponent<Ball>();
            isLeft = Agent.WhichTeam();
            Agent = GetComponent<SoccerAgent>();
            originPos = Agent.transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            //Get the position of the football
            ballLoaction = Agent.getBallPosition();
            //Get the agent's position
            agentLoaction = Agent.transform.position;
            //Determines if the football has entered the goalkeeper's area, if it has, returns Success
            if (Condition.CanGoalKeeper(ballLoaction,isLeft))
            {   //Can goalkeeper kick the ball
                if (Condition.CanKickBall(agentLoaction, ballLoaction))
                {
                    Agent.transform.LookAt(ballLoaction);
                    //Give a force with direction according to the team
                    bool bLeft = Agent.WhichTeam();
                    if (bLeft)
                    {
                        Ball.AddForceBig(ballLoaction, Define.RightDoorPosition);
                        Agent.setDestination(originPos);
                    }
                    else
                    {
                        Ball.AddForceBig(ballLoaction, Define.LeftDoorPosition);
                        Agent.setDestination(originPos);
                    }
                    return TaskStatus.Success;
                }
                else
                {
                    Agent.setDestination(ballLoaction);
                    return TaskStatus.Running;
                }
            }

            return TaskStatus.Failure;
        }
    }
}
