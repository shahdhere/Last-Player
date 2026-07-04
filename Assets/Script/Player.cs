using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed = 5f;

    [Header("UI Settings")]
    public GameObject loseCanvas; 

    public Vector2 currentMovementInput;
    Rigidbody characterController;
    Animator playerAnimator;
    public bool isDead = false;

    public bool isMoving => characterController.linearVelocity.magnitude > 0.1f;

    void Awake()
    {
        characterController = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        if (!isDead)
            PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector3 move = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        move *= speed * Time.fixedDeltaTime;

        characterController.MovePosition(transform.position + move);

        if (move.magnitude > 0f)
        {
            playerAnimator.SetBool("running", true);
            playerAnimator.SetBool("stopping", false);
        }
        else
        {
            playerAnimator.SetBool("running", false);
            playerAnimator.SetBool("stopping", true);
        }
    }

    public void OnMove(InputValue value)
    {
        currentMovementInput = value.Get<Vector2>();
    }

    public void Die()
    {
        if (!isDead)
        {
            Debug.Log("Player has died.");
            isDead = true;
            playerAnimator.SetTrigger("die");
            characterController.linearVelocity = Vector3.zero;

            Invoke(nameof(ShowLoseScreen), 1.5f);
        }
    }

    void ShowLoseScreen()
    {
        if (loseCanvas != null)
            loseCanvas.SetActive(true);
        Time.timeScale = 0f; 
    }

}