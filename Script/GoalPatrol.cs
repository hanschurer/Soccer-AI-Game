using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;

namespace soccerAI
{
    public class GoalPatrol : Action
    {
        private SoccerAgent agent;
        private Vector3 PatrolPos;
        private Vector3 agentPosition;
        private Transform ballLoaction;
        private List<Vector3> PatrolPositions = new List<Vector3>();
        private Vector3 InitPos;
        private int index;

        public override void OnStart()
        {
            agent = GetComponent<SoccerAgent>();
            ballLoaction = agent.getBall().transform;
            //Get the location of the Agent itself
             InitPos = agent.transform.position;
            //Set up patrol points 
            PatrolPositions.Add(new Vector3(InitPos.x, InitPos.y, InitPos.z + Define.Patrol_Circle));
            PatrolPositions.Add(new Vector3(InitPos.x, InitPos.y, InitPos.z - Define.Patrol_Circle));
            //Choose a patrol point close to
            float distance = Mathf.Infinity;
            //Distance difference between agent and the patrol point
            float localDistance;
            for(int i = 0; i < PatrolPositions.Count; ++i)
            {
                if((localDistance = Vector3.Magnitude(agent.transform.position - PatrolPositions[i])) < distance)
                {
                    distance = localDistance;
                    index = i;
                }
            }
            PatrolPos = PatrolPositions[index];
            agent.setDestination(PatrolPos);
        }

        public override TaskStatus OnUpdate()
        {
            //If a player moves to a patrol point, set the next patrol point
            agentPosition = agent.transform.position;
            if(Mathf.Abs(agentPosition.x - PatrolPos.x) < 1 && Mathf.Abs(agentPosition.z - PatrolPos.z) < 1)
            {
                index = (index + 1) % PatrolPositions.Count;
                PatrolPos = PatrolPositions[index];
            }
            //Moving players to patrol points
            agent.setDestination(PatrolPos);
            agent.transform.LookAt(ballLoaction);
            return TaskStatus.Running;
        }

    }
}
