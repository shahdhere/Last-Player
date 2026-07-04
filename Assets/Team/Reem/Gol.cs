using UnityEngine;


public class Gol : MonoBehaviour
{
    public GameObject winUI; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            winUI.SetActive(true); 
            Time.timeScale = 0f;   
        }
    }
}
