using System.Collections;
using UnityEngine;

public class NPC : Player
{
   
    private void OnGirlSinging(object message)
    {
        StopAllCoroutines();
        StartCoroutine(MoveForSeconds((float)message));

    }
    IEnumerator MoveForSeconds(float secondsToMove)
    {
        float randomStartDelay = Random.Range(0f, 1f);
        float randomStopDelay = Random.Range(0f, 1f);
        yield return new WaitForSeconds(randomStartDelay);
        float moveDuration = secondsToMove - 1.64f - randomStopDelay;

    }
}


