using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

    Vector2 playerInput;
    float moveSpeed;
    public float walkSpeed, runSpeed;
    GameObject playerParent;
    bool isRunningKey, isRunningStick, isRunning;
    Animator anim;
    bool isGrounded;
    public float groundCheckDistance = 0.01f;
    public LayerMask groundLayer;

    bool jump, jumped;
    public float gravity = 9.0f;
    public float gravityMultiplier = 1f;
    public float jumpForce = 50f;

    Rigidbody rb;

    [Header("Camera Parameters")]

    Transform mainCamera;
    Transform camTransform;
    public Vector3 distance;
    [SerializeField] Transform lookAt;
    [SerializeField] Transform playerOrientation;
    public float turnSpeed = 2.0f;

    bool canSwipe;
    Vector2 touchScreenInitialPos, touchScreenTemporaryPos, touchScreenCurrentPos;
    Vector2 direction;
    float dir;
    Vector3 offset;
    public float cameraTurnSpeed = 4.0f;

    Vector3 lookAtInitialPosition;
    public float minHeight, maxHeight;
    Vector3 offsetY;

    Vector2 mousePos, mouseTemporaryPos;

    public bool isPc, isHandheld;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerParent = GameObject.FindWithTag("PlayerParent");
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
        camTransform = GameObject.FindWithTag("camTransform").transform;
        rb = GetComponent<Rigidbody>();

        lookAtInitialPosition = lookAt.localPosition;

        offset = distance;
    }

    public void Move(InputAction.CallbackContext _context)
    {
        playerInput = _context.ReadValue<Vector2>();
    }

    public void Running(InputAction.CallbackContext _context)
    {
        if (_context.started)
        {
            isRunningKey = true;
        }

        if (_context.performed)
        {
            isRunningKey = true;
        }

        if (_context.canceled)
        {
            isRunningKey = false;
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);

        MovePlayer();

        if(playerInput != Vector2.zero)
        {
            moveSpeed = walkSpeed;
        }

        if(isRunningKey || isRunningStick)
        {
            isRunning = true;
        } else if(!isRunningKey && !isRunningStick)
        {
            isRunning = false;
        }

        if (isHandheld)
        {
            TouchScreenRotation();
        }

        if (isPc)
        {
            MouseRotation();
        }

        if (!isGrounded)
        {
            rb.AddForce(-gravity * gravityMultiplier * Vector3.up, ForceMode.Acceleration);
        }

        if (!isGrounded && jumped)
        {
            jump = false;
            jumped = false;
        }
    }

    private void LateUpdate()
    {
        offset = Quaternion.AngleAxis(dir * cameraTurnSpeed, Vector3.up) * offset;

        mainCamera.position = transform.position + offset;
        camTransform.position = transform.position + offset;

        mainCamera.LookAt(lookAt.position);
        camTransform.LookAt(new Vector3(lookAt.position.x, camTransform.position.y, lookAt.position.z));

        lookAt.localPosition = new Vector3(lookAt.localPosition.x, Mathf.Clamp(lookAt.localPosition.y, lookAtInitialPosition.y - minHeight, lookAtInitialPosition.y + maxHeight), lookAt.localPosition.z);

        lookAt.localPosition += offsetY * Time.deltaTime;
    }

    void MovePlayer()
    {
        Vector3 forwardDirection = camTransform.forward;
        playerOrientation.forward = forwardDirection.normalized;

        Vector3 moveVector = playerInput.x * playerOrientation.right + playerInput.y * playerOrientation.forward;
        moveVector.Normalize();

        if(isGrounded && moveVector != Vector3.zero)
        {
            if (isRunning)
            {

                if (!jump)
                {
                    anim.SetBool("run", true);
                    anim.SetBool("walk", false);
                    anim.SetBool("idle", false);
                }

                moveSpeed = runSpeed;
            }

            if (!isRunning)
            {
                if (!jump)
                {
                    anim.SetBool("run", false);
                    anim.SetBool("walk", true);
                    anim.SetBool("idle", false);
                }

                moveSpeed = walkSpeed;
            }
        }

        

        if(moveVector == Vector3.zero && !jump)
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetBool("idle", true);
        }

        if(moveVector != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveVector, Time.deltaTime * turnSpeed);
        }

        playerParent.transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetIsRunning(bool _isRunning)
    {
        isRunningStick = _isRunning;
    }

    public void OnScreenTouch(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            canSwipe = true;
        }

        if (_context.canceled)
        {
            canSwipe = false;
        }
    }

    public void TouchScreenInitialPosition(InputAction.CallbackContext _context)
    {
        touchScreenInitialPos = _context.ReadValue<Vector2>();
        touchScreenCurrentPos = touchScreenInitialPos;
    }

    public void TouchScreenCurrentPostion(InputAction.CallbackContext _context)
    {
        touchScreenCurrentPos = _context.ReadValue<Vector2>();
    }

    public void MousePosRotation(InputAction.CallbackContext _context)
    {
        mousePos = _context.ReadValue<Vector2>();
    }

    public void JumpButton(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            jump = true;
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded && jump)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);

            anim.SetBool("idle", false);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetTrigger("jump");
        }
    }

    void TouchScreenRotation()
    {
        if (touchScreenInitialPos.x >= Screen.width / 2 && canSwipe)
        {
            if (touchScreenCurrentPos.x >= Screen.width / 2)
            {
                direction = touchScreenCurrentPos - touchScreenTemporaryPos;
            } else
            {
                direction = Vector2.zero;
            }

            if (direction.x > 0)
            {
                dir = 1f;
            }

            if (direction.x < 0)
            {
                dir = -1f;
            }

            if (direction.x == 0)
            {
                dir = 0f;
            }

            if (direction.y > 0)
            {
                offsetY = new Vector3(0, 1, 0);
            }

            if (direction.y < 0)
            {
                offsetY = new Vector3(0, -1, 0);
            }

            if (direction.y == 0)
            {
                offsetY = new Vector3(0, 0, 0);
            }

            touchScreenTemporaryPos = touchScreenCurrentPos;
        }

        if (!canSwipe)
        {
            dir = 0f;
            offsetY = new Vector3(0, 0, 0);
        }
    }

    void MouseRotation()
    {
        direction = mousePos - mouseTemporaryPos;

        if (direction.x > 0)
        {
            dir = 1f;
        }

        if (direction.x < 0)
        {
            dir = -1f;
        }

        if (direction.x == 0)
        {
            dir = 0f;
        }

        if (direction.y > 0)
        {
            offsetY = new Vector3(0, 1, 0);
        }

        if (direction.y < 0)
        {
            offsetY = new Vector3(0, -1, 0);
        }

        if (direction.y == 0)
        {
            offsetY = new Vector3(0, 0, 0);
        }

        mouseTemporaryPos = mousePos;
    }

    public void Jumped()
    {
        jumped = true;
    }
}
