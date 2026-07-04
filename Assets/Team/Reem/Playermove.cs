/*using UnityEngine;

public class Playermove : MonoBehaviour
{
    public Rigidbody PlayerPhyx;
    public float PlayerSpeed = 5f;
    public float JumpForce = 7f;

    [SerializeField] float fallDeathY = -10f;

    void Update()
    {
        float playerVelocity_Y = PlayerPhyx.linearVelocity.y;

        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerPhyx.linearVelocity = new Vector3(PlayerSpeed, playerVelocity_Y, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerPhyx.linearVelocity = new Vector3(-PlayerSpeed, playerVelocity_Y, 0);
        }
        else
        {
            PlayerPhyx.linearVelocity = new Vector3(0, playerVelocity_Y, 0);
        }

       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPhyx.linearVelocity = new Vector3(PlayerPhyx.linearVelocity.x, JumpForce, 0);
        }

       
        if (transform.position.y < fallDeathY)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Rope"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);
        
    }
}
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody PlayerPhyx;
    public float PlayerSpeed = 5f;
    public float JumpForce = 6f;
    public Animator PlayerAnimator;

    void Update()
    {
        float playerVelocity_Y = PlayerPhyx.linearVelocity.y;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerPhyx.linearVelocity = new Vector3(0, playerVelocity_Y, PlayerSpeed);
            PlayerAnimator.SetTrigger("isRun");
        }
   
        else
        {
            PlayerPhyx.linearVelocity = new Vector3(0, playerVelocity_Y, 0);
            PlayerAnimator.SetTrigger("isIdle");
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAnimator.SetTrigger("isJump");
            PlayerPhyx.linearVelocity = new Vector3(PlayerPhyx.linearVelocity.x, JumpForce, PlayerPhyx.linearVelocity.z);
        }
    }



private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        gameObject.SetActive(false);

    }



}
