using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Button gameBtn;

    private void Awake()
    {
        gameBtn.onClick.AddListener(OnClickGameBtn);    
    }

    private void OnClickGameBtn()
    {
        GameManager.Instance.GotoGameScene();
    }
}
