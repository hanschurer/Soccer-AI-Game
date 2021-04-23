using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soccerAI
{
    public class DefenceStrategy : MonoBehaviour
    {
        public static DefenceStrategy Instance;

        [SerializeField] List<SoccerAgent> TeamsLeft = new List<SoccerAgent>();
        [SerializeField] List<SoccerAgent> TeamsRight = new List<SoccerAgent>();
        List<SoccerAgent> team = new List<SoccerAgent>();

        void Awake()
        {
            Instance = this;
        }

        public List<SoccerAgent> GetAgentTeam(bool isLeft)
        {
            if (isLeft)
            {
                return TeamsLeft;
            }
            return TeamsRight;
        }
        /// <summary>
        /// Get agent position inside the defensive group;
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="targetPosition"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
		public Vector3 GetDefenceGroupLocation(SoccerAgent agent, Vector3 targetPosition, bool isLeft)
        {
            //Get list of agent according the team;
            List<SoccerAgent> team = GetAgentTeam(isLeft);

            //Rank the distance between the player and the target point, the closest to the target point the top of the list; 
            team.Sort((a, b) => {
                return Vector3.Distance(a.transform.position, targetPosition).CompareTo(Vector3.Distance(b.transform.position, targetPosition));
            });

            //Get agent index value after sorting;
            var index = team.FindIndex(a => a.getNum() == agent.getNum());

            // if the index value is 0, the target is the ball.
            if (index <= 0)
            {
                return targetPosition;
            }
            else
            {
                //Align the agent to the nearest player who is closer to the ball
                var nearsMeAgentLocation = team[index-1].transform.position;

                if (transform.position.z > nearsMeAgentLocation.z)
                {
                    return new Vector3(nearsMeAgentLocation.x, 0, nearsMeAgentLocation.z + 3);
                }

                else
                {
                    return new Vector3(nearsMeAgentLocation.x, 0, nearsMeAgentLocation.z - 3);
                }

            }
        }
    }
}
