using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace mlsoccer
{
    public class MLUIManager : MonoBehaviour
    {
        private static MLUIManager _instrance;
        public Text redText, blueText, winText;
        public CanvasGroup canvasGroup;
        [SerializeField] GameObject winPanel;

        bool flag = true;
        // Start is called before the first frame update
        private void Awake()
        {
            _instrance = this;
            canvasGroup = winPanel.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static MLUIManager getUI
        {
            get
            {
                return _instrance;
            }
        }

        
        void Update()
        {
            redText.text = MLGameManager.GetGM.getRedScore.ToString();
            blueText.text = MLGameManager.GetGM.getBlueScore.ToString();
        }

        public void setWIN(int flag)
        {
            switch (flag) {
                case 1:
                    winText.text = "Red Group Win!";
                    break;
                case 2:
                    winText.text = "Blue Group Win!";
                    break;
            }
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void ReStart()
        {
            SceneManager.LoadScene(2);
            winPanel.SetActive(false);
            Time.timeScale = 1;
        }


        public void SetRulebased() {
            SceneManager.LoadScene("MLScene1");
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
