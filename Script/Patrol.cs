using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Patrol : Action
{

    private NavMeshAgent agent;

    // A list of all the patrolling waypoints
    private List<Vector3> waypoints = new List<Vector3>();
    // patrolling position
    private Vector3 PatrolPos;

    private Vector3 agentPos;

    private int index;


    //Follow the radius of the circle to patrol
    [SerializeField] float Patrol_Circle = 4f;


    // Start is called before the first frame update

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        var InitPos = agent.transform.position;
        var PatrolPos1 = new Vector3(InitPos.x + Patrol_Circle, InitPos.y, InitPos.z + Patrol_Circle);
        var PatrolPos2 = new Vector3(InitPos.x + Patrol_Circle, InitPos.y, InitPos.z - Patrol_Circle);
        var PatrolPos3 = new Vector3(InitPos.x - Patrol_Circle, InitPos.y, InitPos.z - Patrol_Circle);
        var PatrolPos4 = new Vector3(InitPos.x - Patrol_Circle, InitPos.y, InitPos.z + Patrol_Circle);
        waypoints.Add(PatrolPos1);
        waypoints.Add(PatrolPos2);
        waypoints.Add(PatrolPos3);
        waypoints.Add(PatrolPos4);
    }



    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
            agentPos = agent.transform.position;
        // When reach the  patrol point, re-randomized to the next patrol point
            if (Mathf.Abs(agentPos.x - PatrolPos.x) < 1 && Mathf.Abs(agentPos.z - PatrolPos.z) < 1)
                {
                index = (index + 1) % waypoints.Count;
                    PatrolPos = waypoints[index];
                    //Debug.Log("PatrolPos: " + PatrolPos);
                }
        // Set the patrol point as the target point of Agent;
            agent.SetDestination(PatrolPos);
            return TaskStatus.Running;
        
    }

    public override void OnStart()
    {
        // Find the nearest patrol point;
        float distance = Mathf.Infinity;
        float localDistance;
        for (int i = 0; i < waypoints.Count; ++i)
        {
            if ((localDistance = Vector3.Magnitude(agent.transform.position - waypoints[i])) < distance)
            {
                distance = localDistance;
                index = i;
            }
        }

        PatrolPos = waypoints[index];
        agent.enabled = true;
        agent.SetDestination(PatrolPos);

    }
}

