using UnityEngine;
using UnityEngine.AI;


namespace soccerAI {

        public class Ball : MonoBehaviour
        {
            [SerializeField] float FORCE = Define.FORCE;
            [SerializeField] float BIG_FORCE = Define.BIG_FORCE;
            [SerializeField] Rigidbody rb;

            private RaycastHit hit = new RaycastHit();

            private Vector3 oriPos;
            public GameObject ball;

            private void Start()
            {
                oriPos = transform.position;
            }
            public void setOri()
            {
                this.gameObject.transform.position = oriPos;
                rb.velocity = Vector3.zero;

        }

            /// <summary>
            /// Add a force;
            /// </summary>
            /// <param name="form"></param>
            /// <param name="to"></param>
            public void AddForce(Vector3 form, Vector3 to)
            {
                Vector3 force = (to - form).normalized * FORCE;
                rb.AddForce(new Vector3(force.x, 0, force.z), ForceMode.Impulse);
            }
            /// <summary>
            /// Add a bigger force;
            /// </summary>
            /// <param name="form"></param>
            /// <param name="to"></param>
            public void AddForceBig(Vector3 form, Vector3 to)
            {
                Vector3 force = (to - form).normalized * BIG_FORCE;
                rb.AddForce(new Vector3(force.x, 0, force.z), ForceMode.Impulse);
            }

            public void BeforeKickOff()
            {
                transform.position = new Vector3(0, 10000, 0);
                rb.velocity = Vector3.zero;
            }

            public void ReStart()
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
            }


            void Update()
            {
                // Set the position of the ball to the point of the mouse;
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit, 100);
                    if (null != hit.transform)
                    {
                        transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
                        body.velocity = Vector3.zero;
                        body.Sleep();
                    }
                }
            }
        }
    


}

