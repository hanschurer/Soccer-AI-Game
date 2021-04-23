using UnityEngine;
using System.Collections.Generic;

namespace soccerAI
{
	public class Define
	{
        /// <summary>
        /// Football playground Length
        /// </summary>
        public static int Length = 80;
        /// <summary>
        /// The force of the kick
        /// </summary>
        public static int FORCE = 8;
        /// <summary>
        /// The big force of kick
        /// </summary>
        public static int BIG_FORCE = 13;
        /// <summary>
        /// Radius of patrol
        /// </summary>
        public static float Patrol_Circle = 1.2f;
        /// <summary>
        ///  see the radius of the ball
        /// </summary>
        public static float See_Circle = 10f;
        /// <summary>
        /// Location of the left-hand goal of the pitch
        /// </summary>
        public static Vector3 LeftDoorPosition = new Vector3(-Length/2f,0,0);
        /// <summary>
        /// Position of the right-hand goal of the pitch
        /// </summary>
        public static Vector3 RightDoorPosition = new Vector3(Length/2f,0,0);
        /// <summary>
        /// Distance to be able to kick the ball
        /// </summary>
        public static float CanKickBallDistance = 1f;


	}
}

