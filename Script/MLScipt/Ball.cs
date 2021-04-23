using UnityEngine;



public class Ball : MonoBehaviour
{
    public GameObject Instance;
    public string redTag;
    public string blueTag;
    private GameObject gm;
    [HideInInspector]
    public SoccerEnv env;
    void Start()
    {
        env = Instance.GetComponent<SoccerEnv>();
        

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(redTag)) //ball touched purple goal
            {
                Debug.Log("Blue team scored a goal!");
                env.GoalTouched(Team.Blue);

            }
            if (collision.gameObject.CompareTag(blueTag)) //ball touched blue goal
            {
                Debug.Log("Blue team scored a goal!");
                env.GoalTouched(Team.Red);
            }
        }
    }
}
