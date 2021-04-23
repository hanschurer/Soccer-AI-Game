using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soccerAI
{
    public class Goal : MonoBehaviour
    {
        // Determine if it is the left team
        public bool isLeftTeam;

        private void OnTriggerEnter(Collider other)
        {
            //If the ball goes into the left team's goal then the game management script is called to give the right team a point
            if (isLeftTeam && other.gameObject.tag == "ball")
            {
                GameManage.GetGM.addRightScore();
               
            }
            else if (!isLeftTeam && other.gameObject.tag == "ball") //vice versa
            {
                GameManage.GetGM.addLeftScore();
                
            }


        }

    }
}
