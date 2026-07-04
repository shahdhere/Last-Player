using UnityEngine;
using TMPro;

public class WinCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] GameObject player;      
    [SerializeField] GameObject rope;        
    [SerializeField] float targetAmount = 1000000f; 
    [SerializeField] float speed = 50000f;           

    private bool isWin = false;
    private bool ropeTouched = false;
    private float currentAmount = 0f;

    void Update()
    {
        if (isWin)
        {
            if (currentAmount < targetAmount)
            {
                currentAmount += speed * Time.deltaTime;
                if (currentAmount > targetAmount)
                    currentAmount = targetAmount;

                winText.text = Mathf.FloorToInt(currentAmount).ToString("N0"); // مثل عداد فلوس
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (!ropeTouched)
            {
                isWin = true; // فاز 
                Debug.Log("Player won!");
            }
            else
            {
                Debug.Log("Player reached the end but touched the rope before!");
            }
        }

        if (other.gameObject == rope)
        {
            ropeTouched = true;
            Debug.Log("Player touched the rope - lose!");
        }
    }
}