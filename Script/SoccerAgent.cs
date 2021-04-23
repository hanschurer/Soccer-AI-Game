using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace soccerAI {
    public class SoccerAgent : MonoBehaviour
    {

        [SerializeField] Transform Ball;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] int number;
        //Player team true is left(blue) team, false(red) is right team;
        [SerializeField] bool Team;



        //record the init position
        private Vector3 initialPos;


        public void Start()
        {
            ////record the init position
            initialPos = gameObject.transform.position;
        }

        // function to define which team is the agent in
        public bool WhichTeam()
        {
            return this.Team;
        }

        //function to chase the ball
        public void setDestination(Vector3 target)
        {

            agent.enabled = true;
            agent.SetDestination(target);

        }

        //function to get ball position
        public Vector3 getBallPosition()
        {

            return Ball.position;
        }

        // function to get the ball 
        public GameObject getBall()
        {
            return Ball.gameObject;
        }

        public NavMeshAgent GetNav()
        {
            return this.agent;
        }

        // function to get the palyer number
        public int getNum()
        {
            return this.number;
        }

        //function to reset the position
        public void setInitPos()
        {

            gameObject.transform.position = initialPos;
        }
    }
}

