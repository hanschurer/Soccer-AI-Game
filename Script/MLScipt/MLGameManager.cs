using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace mlsoccer
{

    public class MLGameManager : MonoBehaviour
    {
        
        private static MLGameManager gm;
        private int RedScore = 0;
        private int BlueScore = 0;
        
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
        public static MLGameManager GetGM
        {
            get
            {
                return gm;
            }
        }

        public void addRedScore()
        {
            RedScore++;
            
            if (RedScore == 5)
            {
                Time.timeScale = 0;
                MLUIManager.getUI.setWIN(1);
            }
        }

        public void addBlueScore()
        {
            BlueScore++;
            
            if(BlueScore == 5)
            {
                Time.timeScale = 0;
                MLUIManager.getUI.setWIN(0);
            }
        }

        public int getRedScore
        {
            get{
                return RedScore;
            }
        }
        public int getBlueScore
        {
            get
            {
                return BlueScore;
            }
        }
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
        }
    }
}
