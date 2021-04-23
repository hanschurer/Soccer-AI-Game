using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace soccerAI { 

  public class UIManager : MonoBehaviour
    {
        private static UIManager _instrance;
        public Text redText, blueText, winText;
        public CanvasGroup canvasGroup;
        [SerializeField] GameObject winPanel;

        bool flag = true;
        // Start is called before the first frame update
        private void Awake()
        {
            _instrance = this;
            canvasGroup = winPanel.GetComponent<CanvasGroup>();
            setPanelNotvisible();
        }

        public static UIManager getUI
        {
            get
            {
                return _instrance;
            }
        }

        // Update is called once per frame
        void Update()
        {
            redText.text = GameManage.GetGM.getLeftScore.ToString();
            blueText.text = GameManage.GetGM.getRightScore.ToString();
        }

        public void setWIN(int flag)//1为红赢，2为蓝，3为平局
        {
            switch (flag)
            {
                case 1:
                    winText.text = "Red Group Win!";
                    break;
                case 2:
                    winText.text = "Blue Group Win!";
                    break;
            }
            setPanelvisible();
        }

        public void ReStart()
        {
            SceneManager.LoadScene(1);
            winPanel.SetActive(false);
            Time.timeScale = 1;
        }


        public void SetRulebased() {


            SceneManager.LoadScene("MyScene");

        }
        public void setPanelvisible(){
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void setPanelNotvisible(){
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Quit()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

        public void Set()
        {
            if (flag)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                winPanel.SetActive(false);
                Time.timeScale = 1;
            }
            flag = !flag;
        }
    }
}

  

