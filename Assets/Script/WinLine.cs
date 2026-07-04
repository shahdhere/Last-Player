using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLine : MonoBehaviour
{
    [SerializeField] GameObject winUI; // شاشة الفوز
    [SerializeField] string mainMenuSceneName = "Game"; 

    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasWon && other.CompareTag("Player"))
        {
            hasWon = true;
            WinGame();
        }
    }

    void WinGame()
    {
        winUI.SetActive(true); // تظهر شاشة الفوز
        Time.timeScale = 0f;   // توقف اللعبة
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // استعادة الوقت
        SceneManager.LoadScene(mainMenuSceneName); // تحميل المشهد الرئيسي
    }
}
