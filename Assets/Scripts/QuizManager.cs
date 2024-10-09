using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    public float waitminValue, waitsecvalue;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void SaveCurrentLoginTime()
    {
        DateTime currentTime = DateTime.Now; // Get the current date and time
        PlayerPrefs.SetString(GameManager.KEY_PLAYER_LAST_QUIZ_LOGIN, currentTime.ToString());
        PlayerPrefs.Save(); // Save PlayerPrefs
    }
    public TimeSpan? GetTimeSinceLastLogin()
    {
        // Check if the key exists in PlayerPrefs
        if (PlayerPrefs.HasKey(GameManager.KEY_PLAYER_LAST_QUIZ_LOGIN))
        {
            string lastLoginString = PlayerPrefs.GetString(GameManager.KEY_PLAYER_LAST_QUIZ_LOGIN);
            DateTime lastLoginTime;
            if (DateTime.TryParse(lastLoginString, out lastLoginTime))
            {
                // Calculate the time difference
                TimeSpan timeDifference = DateTime.Now - lastLoginTime;
                return timeDifference; // Return the time difference
            }
        }

        // Return null if there's no login time saved
        return null;
    }
    public void CalculateandSetWaitingTime()
    {
        float totalmin = 0;
        if(PlayerPrefs.HasKey(GameManager.KEY_PLAYER_LAST_QUIZ_LOGIN))
        {
            TimeSpan? timeDifference = GetTimeSinceLastLogin();
            if (timeDifference.HasValue)
            {
                if (timeDifference.Value.TotalHours < 1)
                {
                    double remainingMinutes = 60 - timeDifference.Value.TotalMinutes;
                    totalmin = (float)Math.Ceiling(remainingMinutes);
                    UiManager.instance.UipanelController.waitingTime = totalmin;
                    UiManager.instance.UipanelController.quizStatus = false;
                }
                else
                {
                    UiManager.instance.UipanelController.waitingTime = 0;
                    UiManager.instance.UipanelController.quizStatus = true;
                }
            }
        }
        else
        {
            UiManager.instance.UipanelController.waitingTime = 0;
            UiManager.instance.UipanelController.quizStatus = true;
        }
        
    }
    public void StartTimmer(float min)
    {
        StartCoroutine(WaitAndUpdateTimer(min));
    }
    public IEnumerator WaitAndUpdateTimer(float waitingTimeInMinutes)
    {
        float waitingTimeInSeconds = waitingTimeInMinutes * 60; // Convert minutes to seconds
        float elapsedTime = 0;

        while (elapsedTime < waitingTimeInSeconds)
        {
            elapsedTime++; // Increment the elapsed time
            float remainingTime = waitingTimeInSeconds - elapsedTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            this.waitminValue = minutes;
            this.waitsecvalue = seconds;    
            // Update your timer display here (you can replace this with your actual UI update)
            Debug.Log($"Time remaining: {minutes}m {seconds}s");

            // Wait for 1 second before updating again
            yield return new WaitForSecondsRealtime(1f); // Pause for 1 second
        }

        UiManager.instance.UipanelController.quizStatus = true;
        Debug.Log("Wait time is over!"); // Notify when the wait time is complete
    }
}
