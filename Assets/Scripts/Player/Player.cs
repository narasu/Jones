using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Player>();

            return instance;
        }
    }

#pragma warning disable 0649
    [Header("Movement")]
    [SerializeField] private string horizontalInputName, verticalInputName;
    [SerializeField] private float movementSpeed;

    [Header("Jumping")]
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [Header("Traps")]
    [SerializeField] private GameObject trapPrefab;

    public Vector3 NextPos { get; private set; }

#pragma warning restore 0649

    [SerializeField] [Tooltip("How many traps does the player have at the start of the level?")] private int numberOfTraps = 5;
    [HideInInspector] public int CurrentTraps  { get; private set; } // how many traps the player is currently holding

    private CharacterController charController;
    private bool isJumping;
    
    private void Awake()
    {
        charController = GetComponent<CharacterController>();

        AudioListenerFollow.Instance.playerCamera = gameObject.GetComponentInChildren<Camera>().transform;

        Debug.Log("Traps:" + CurrentTraps);
        NextPos = transform.position;
    }

    private void Start()
    {
        RefillTraps();
    }

    private void Update()
    {
        PlayerMove();
    }

    //handle player movement with input from the horizontal and vertical axes
    private void PlayerMove()
    {
        float horizInput = Input.GetAxisRaw(horizontalInputName);
        float vertInput = Input.GetAxisRaw(verticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        //normalize the movement vector to prevent super fast diagonal movement ( and people cheesing my whole game >:C )
        Vector3 movement = Vector3.Normalize(forwardMovement + rightMovement) * movementSpeed;

        charController.SimpleMove(movement);

        //store next position for the enemy to target
        NextPos = transform.position + movement;

        JumpInput();
    }
    
    //create a trap at the player's position, if the player has traps in inventory
    public void SetTrap()
    {
        if (CurrentTraps <= 0)
            return;

        GameObject trap = Instantiate(trapPrefab);
        GameManager.Instance.AddTrapToList(trap);
        trap.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        CurrentTraps--;
    }

    public void RefillTraps()
    {
        CurrentTraps = numberOfTraps;
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