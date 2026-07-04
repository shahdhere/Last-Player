using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainigTime = 135f; // 3 دقائق
    [SerializeField] GameObject loseUI; 

    private bool hasLost = false; 

    void Update()
    {
        if (remainigTime > 0)
        {
            remainigTime -= Time.deltaTime;
        }
        else
        {
            remainigTime = 0;

            if (!hasLost)
            {
                LoseGame();
            }
        }

        // لما يوصل 30 ثانية يتغير لونه أحمر
        if (remainigTime <= 30 && remainigTime > 0)
        {
            timerText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(remainigTime / 60);
        int seconds = Mathf.FloorToInt(remainigTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoseGame()
    {
        hasLost = true;
        loseUI.SetActive(true); // تظهر شاشة الخسارة
        Time.timeScale = 0f; 
    }
}
