using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace soccerAI
{
    public class Defender : Action
    {
        private Vector3 agentLocation;
        private SoccerAgent Agent;
        private Ball Ball;
        private bool isLeft;
        private Vector3 ballLocation;

        public override void OnStart()
        {
            Agent = GetComponent<SoccerAgent>();
            Ball = Agent.getBall().GetComponent<Ball>();
            isLeft = Agent.WhichTeam();
        }

        public override TaskStatus OnUpdate()
        {
            //Get football position;
            ballLocation = Agent.getBallPosition();
            //Get agent position;
            agentLocation = Agent.transform.position;
            //To get into this node the agent must have seen the ball
            if (Condition.CanKickDefence(agentLocation, ballLocation))
            {
                //if close to the ball
                if (Condition.CanKickBall(agentLocation, ballLocation))
                {
                    //face to the ball ;
                    Agent.transform.LookAt(ballLocation);
                    //Depending on the direction of opponent's goal, give the ball a force;
                    bool isLeft = Agent.WhichTeam();
                    if (isLeft)
                    {
                        Ball.AddForceBig(ballLocation, Define.RightDoorPosition);
                    }
                    else
                    {
                        Ball.AddForceBig(ballLocation, Define.LeftDoorPosition);
                    }
                    //Return successful;
                    return TaskStatus.Success;
                }
                else
                {
                    if (Condition.CanDefenceGroup(Agent.WhichTeam(), Agent.transform.position, ballLocation)) {

                        var target = DefenceStrategy.Instance.GetDefenceGroupLocation(Agent, ballLocation, isLeft);
                        Agent.setDestination(target);
                        return TaskStatus.Running;
                    }
                    else { 
                        //agent can defence the ball, but the distance is not enought to kick the ball, so move towards the ball;
                        Agent.setDestination(ballLocation);
                    }                      
                        return TaskStatus.Running;
                }
            }
            // return Failure when can't defence the ball;
            return TaskStatus.Failure;

        }


    }
}     

