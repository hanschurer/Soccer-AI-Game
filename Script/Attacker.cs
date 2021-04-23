using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;




namespace soccerAI {

    public class Attacker : Action

    {

        private SoccerAgent agent;
        private Ball Ball;

        private Vector3 ballLocation;

        private Vector3 agentLocation;

        private Vector3 targetLocation;
        // if is the left team
        private bool isLeft;
        public override void OnStart()
        {
            //Get the agent, ball and team component
            agent = GetComponent<SoccerAgent>();
            Ball = agent.getBall().GetComponent<Ball>();
            isLeft = agent.WhichTeam();
        }

        public override TaskStatus OnUpdate()
        {
            //Get the position of the ball;
            ballLocation = agent.getBallPosition();
            //Get the position of itself;
            agentLocation = agent.transform.position;
            //If the football is within kickable range;
            if (Condition.CanKickBall(agentLocation, ballLocation))
            {
                //Towards the ball;
                agent.transform.LookAt(ballLocation);
                //Depending on the team, give the ball a force;
                if (isLeft)
                {
                    Ball.AddForce(ballLocation, Define.RightDoorPosition);
                }
                else
                {
                    Ball.AddForce(ballLocation, Define.LeftDoorPosition);
                }
                //return success;
                return TaskStatus.Success;
            }
            else
            {
                //Get agent position inside the attacking formation;
                targetLocation = AttackStrategy.Instance.GetAttackGroupLocation(agent, ballLocation, isLeft);
                //Set the target points;
                agent.setDestination(targetLocation);
                //return running;
                return TaskStatus.Running;
            }
        }
}
}
