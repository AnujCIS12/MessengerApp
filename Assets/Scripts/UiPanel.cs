using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour
{
    public Button quizBtn;
    public float waitingTime;
    public bool quizStatus;
    private void Awake()
    {

    }
    private void OnEnable()
    {
        quizBtn.onClick.AddListener(OnClickQuizBtn);
        SetQuizStatus();
    }
    private void OnDisable()
    {
        quizBtn.onClick.RemoveAllListeners();
    }
    public void OpenWaitPopUp()
    {
        UiManager.instance.quizWaitController.gameObject.SetActive(true);
    }
    private void OnClickQuizBtn()
    {
        if (quizStatus) 
        {
            GameManager.Instance.GotoQuizScene();
        }
        else
        {
            OpenWaitPopUp();
        }
    }
    public void SetQuizStatus()
    {
        QuizManager.Instance.CalculateandSetWaitingTime();
        StartTimmer();
    }
    private void StartTimmer()
    {
        if (waitingTime != 0)
        {
            QuizManager.Instance.StartTimmer(waitingTime);
        }
    }

}
