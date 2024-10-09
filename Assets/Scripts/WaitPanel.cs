using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitPanel : MonoBehaviour
{
    public Button closedBtn;
    public TextMeshProUGUI waitMessageTxt;
    private string data;

    private void OnEnable()
    {
        closedBtn.onClick.AddListener(OnClickClosedBtn);
        data = "Kindly Wait ";
    }
    public void SetMessage()
    {
        string time = QuizManager.Instance.waitminValue + " : " + QuizManager.Instance.waitsecvalue;
        waitMessageTxt.text = string.Concat(data,time," Min"); 
    }
    private void OnDisable()
    {
        closedBtn.onClick.RemoveAllListeners();
    }
    private void OnClickClosedBtn()
    {
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetMessage();
    }
}
