using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace soccerAI
{
    public class BallinGoalZone : Conditional
    {

        private SoccerAgent Agent;
        private bool isLeft;
        private Vector3 ballLoaction;

 

        public override void OnStart()
        {
            //Get agent Scripts
            Agent = GetComponent<SoccerAgent>();
            isLeft = Agent.WhichTeam();

        }

        public override TaskStatus OnUpdate()
        {   
            ballLoaction = Agent.getBallPosition();
            //Determining whether the ball is in the keeper's zone
            if (Condition.CanGoalKeeper(ballLoaction,isLeft))
            {
                //If it is in goal keeper field, give the goalkeeper a reinforcement on speed and accelaeration
                Agent.gameObject.GetComponent<NavMeshAgent>().speed = 15;
                Agent.gameObject.GetComponent<NavMeshAgent>().acceleration = 12;
                return TaskStatus.Success;
            }
            else
            {
                Agent.GetComponent<NavMeshAgent>().speed = 10;
                Agent.gameObject.GetComponent<NavMeshAgent>().acceleration = 8;
                return TaskStatus.Failure;
            }
        }
    }
}
