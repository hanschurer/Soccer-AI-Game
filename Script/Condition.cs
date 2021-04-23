using UnityEngine;
using System.Collections.Generic;

namespace soccerAI
{
	public class Condition
	{
        /// <summary>
        /// can agent see the ball;
        /// </summary>
        /// <param name="agentLocation"></param>
        /// <param name="ballLocation"></param>
        /// <returns></returns>
        public static bool CanSeeBall(Vector3 agentLocation, Vector3 ballLocation)
        {
            if (Mathf.Abs(agentLocation.x - ballLocation.x) < Define.See_Circle && Mathf.Abs(agentLocation.z - ballLocation.z) < Define.See_Circle)
            {
                if (ballLocation.y <= 0.5f)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Can aent see the goal;
        /// </summary>
        /// <param name="agentLocation"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        public static bool CanSeeDoor(Vector3 agentLocation, bool isLeft)
        {
            if (isLeft)
            {
                if ((Define.RightDoorPosition - agentLocation).sqrMagnitude < 400)
                {
					return true;
                }
				return false;
            }
            else
            {
				if((Define.LeftDoorPosition - agentLocation).sqrMagnitude < 400)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Can agent kick the ball;
        /// </summary>
        /// <param name="agentLocation"></param>
        /// <param name="ballLocation"></param>
        /// <returns></returns>
		public static bool CanKickBall(Vector3 agentLocation,Vector3 ballLocation)
		{
			if(ballLocation.y>=2)
			{
				return false;
			}
			if (Mathf.Abs (agentLocation.x - ballLocation.x) < Define.CanKickBallDistance 
                && Mathf.Abs (agentLocation.z - ballLocation.z) < Define.CanKickBallDistance)
				return true;
			return false;
		}

        /// <summary>
        /// Defencer team Can kick ball strategy;
        /// </summary>
        /// <param name="agentLocation"></param>
        /// <param name="ballLocation"></param>
        /// <returns></returns>
		public static bool CanKickDefence(Vector3 agentLocation,Vector3 ballLocation)
		{ 
			return CanSeeBall(agentLocation,ballLocation);	
		}

        /// <summary>
        /// Defencer team Can kick ball strategy;
        /// </summary>
        /// <param name="agentLocation"></param>
        /// <param name="ballLocation"></param>
        /// <returns></returns>
        public static bool CanDefenceGroup(bool bLeft,Vector3 agnetLocation,Vector3 ballLocation)
        {
            if (bLeft)
            {
                return Vector3.Distance(ballLocation, Define.LeftDoorPosition) > Vector3.Distance(agnetLocation, Define.LeftDoorPosition);
            }
            else
            {
                return Vector3.Distance(ballLocation, Define.RightDoorPosition) > Vector3.Distance(agnetLocation, Define.RightDoorPosition);
            }
        }

        ///<summary>
        ///can Goal keeper kick the ball
        ///<param name="ballLoaction"></param>
        ///<param name="isLeft"></param>
        ///</summary>
        public static bool CanGoalKeeper(Vector3 ballLoaction, bool isLeft)
        {
            if (isLeft)
            {
                if (ballLoaction.x < -30f && Mathf.Abs(ballLoaction.z) < Mathf.Abs(7f))
                {
                    return true;
                }
            }
            else {
                if (ballLoaction.x > 30f && Mathf.Abs(ballLoaction.z) < Mathf.Abs(7f))
                {
                    return true;
                }
            }

            return false;
        }
	}
}

