using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace soccerAI
{
    public class BallinSight : Conditional
    {
        /// <summary>
        /// agnet;
        /// </summary>
        private SoccerAgent Agent;
        /// <summary>
        /// ball;
        /// </summary>
        private Ball Ball;

        public override void OnStart()
        {
            Agent = GetComponent<SoccerAgent>();
            Ball = Agent.getBall().GetComponent<Ball>();
        }

        public override TaskStatus OnUpdate()
        {
            //If can see the ball, return success. Otherwise return failure;

            if (Condition.CanSeeBall(Agent.transform.position, Ball.transform.position))
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }


    }
}