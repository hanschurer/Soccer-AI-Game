using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soccerAI
{
    public class gizimo : MonoBehaviour
    {
        // Start is called before the first frame update
        public float maxAngle;
        public float maxRadius;
        public SoccerAgent agent;
        public GameObject ball;
        public TextMesh text;
        public bool isLeft;

        public List<SoccerAgent> team = new List<SoccerAgent>();

        private void OnDrawGizmos()
        {
            agent = GetComponent<SoccerAgent>();
            ball = GameObject.FindGameObjectWithTag("ball");
            isLeft = agent.WhichTeam();

            Gizmos.color = Color.red;
            if (Condition.CanGoalKeeper(ball.transform.position, isLeft)) {

                Gizmos.color = Color.green;
            
            }
            Gizmos.DrawLine(transform.position, ball.transform.position);


            // Gizmos.DrawWireSphere(transform.position, maxRadius);

            /*
                        team.Sort((a, b) => {
                            return Vector3.Distance(a.transform.position, ball.transform.position).CompareTo(Vector3.Distance(b.transform.position, ball.transform.position));
                        });

                        Debug.Log(team[0]);

                        Gizmos.color = Color.red;
                        if (team[0].transform.position == agent.transform.position) {
                            Gizmos.color = Color.green;
                        }

                        Gizmos.DrawLine(transform.position, ball.transform.position);

                    }*/
        }
    }
}