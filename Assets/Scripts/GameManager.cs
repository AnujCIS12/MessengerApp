using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static readonly string KEY_PLAYER_LAST_QUIZ_LOGIN="Playerlastlogin";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GotoGameScene()
    {
        SwitchSceneByIndex(0);
    }
    public void GotoQuizScene()
    {
        QuizManager.Instance.SaveCurrentLoginTime();
        SwitchSceneByIndex(1);
    }
    private void SwitchSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
        }
    }
}
