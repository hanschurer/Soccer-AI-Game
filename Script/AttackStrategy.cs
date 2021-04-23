using UnityEngine;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using UnityEngine.UI;

namespace soccerAI
{ 
	public class AttackStrategy : MonoBehaviour
	{ 
		public static AttackStrategy Instance;

		[SerializeField] List<SoccerAgent> TeamsLeft = new List<SoccerAgent>();

		[SerializeField] List<SoccerAgent> TeamsRight = new List<SoccerAgent>();

		void Awake()
		{
			Instance = this;
		}

		public List<SoccerAgent> GetAgentTeam(bool isLeft)
		{
			if(isLeft)
			{
				return TeamsLeft;
			}
			return TeamsRight;
		}
		/// <summary>
		/// Get the position in the attacker team;;
		/// </summary>
		/// <param name="agent"></param>
		/// <param name="targetPosition"></param>
		/// <param name="isLeft"></param>
		/// <returns></returns>
		public Vector3 GetAttackGroupLocation(SoccerAgent agent,Vector3 targetPosition,bool isLeft)
		{
			//Get a list of players according to the team
			List<SoccerAgent> team = GetAgentTeam (isLeft);

			//Rank the distance between the player and the target point, with the closest to the target point will be set at the top of the list; 
			team.Sort((a, b) => {
                return Vector3.Distance(a.transform.position, targetPosition).CompareTo(Vector3.Distance(b.transform.position, targetPosition));
            });

			//Get the index value after sorting;
			var index = team.FindIndex (a=>a.getNum()==agent.getNum());

			// Index value of 0 is the closest player to the target point;
			if (index == 0)
            {
                return targetPosition; 
			} 
			else 
			{
				// Get the position of the closest player to the target point;
				var nearstBallAgentLocation = team[0].transform.position;
                int position = (index + 1) / 2;
               
				if((index%2)==0)
				{
					//If the index is even, place it on the side of the player closest to the target point;
					return new Vector3(nearstBallAgentLocation.x,0,nearstBallAgentLocation.z - position * 6); 
				}
				//If the index is an odd number, place the side of the player nearest to the target point, above the pitch;
				return new Vector3(nearstBallAgentLocation.x,0,nearstBallAgentLocation.z + position * 6);
			}
		}

		//Find the player closest to the goal to the pass ball
		public SoccerAgent findNear(SoccerAgent agent, bool isLeft)
        {
			List < SoccerAgent >  team = GetAgentTeam(isLeft);

			if (isLeft)
			{
				team.Sort((a, b) =>
				{
					return Vector3.Distance(a.transform.position, Define.RightDoorPosition).CompareTo(Vector3.Distance(b.transform.position, Define.RightDoorPosition));
				});
			}
            else
            {
				team.Sort((a, b) =>
				{
					return Vector3.Distance(a.transform.position, Define.LeftDoorPosition).CompareTo(Vector3.Distance(b.transform.position, Define.LeftDoorPosition));
				});
            }

			return team[0];
        }
	}
}

