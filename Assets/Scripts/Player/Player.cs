using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private string horizontalInputName, verticalInputName;
    [SerializeField] private float movementSpeed;

    [Space] [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [Space] [SerializeField] private GameObject trapPrefab;
    [SerializeField] [Tooltip("How many traps does the player have at the start of the level?")] private int numberOfTraps;
#pragma warning restore 0649

    private CharacterController charController;
    private bool isJumping;
    
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();

        if (Input.GetMouseButtonDown(0))
        {
            SetTrap();
        }
    }

    //handle player movement with input from the horizontal and vertical axes
    private void PlayerMove()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        JumpInput();
    }
    
    //create a trap at the player's position, if the player has traps in inventory
    private void SetTrap()
    {
        if (numberOfTraps <= 0)
            return;

        
        GameObject trap = Instantiate(trapPrefab);
        trap.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        numberOfTraps--;
    }

    //handle jump input
    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    //jump movement
    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }
}