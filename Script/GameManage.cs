using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace soccerAI { 

    public class GameManage : MonoBehaviour
    {
        private static GameManage gm;
        private int leftScore = 0;
        private int rightScore = 0;


        [SerializeField] List<SoccerAgent> players = new List<SoccerAgent>();
        public Ball ball;
        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                SceneManager.LoadScene(0);
            }
        }
        private void Awake()
        {
            gm = this;
        }
        public static GameManage GetGM
        {
            get
            {
                return gm;
            }
        }
        public void addLeftScore()
        {
            leftScore++;
            setOriginal();
            if (leftScore == 5)
            {
                Time.timeScale = 0;
                UIManager.getUI.setWIN(0);
            }
        }

        public void addRightScore()
        {
            rightScore++;
            setOriginal();
            if (rightScore == 5)
            {
                Time.timeScale = 0;
                UIManager.getUI.setWIN(1);
            }
        }

        public int getLeftScore
        {
            get{
                return leftScore;
            }
        }
        public int getRightScore
        {
            get
            {
                return rightScore;
            }
        }
        public void setOriginal()
        {
            foreach (var player in players)
            {
                player.setInitPos();
            }
            ball.setOri();
        }


        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
        }
    }

}
