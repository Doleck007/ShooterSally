using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Animator animator;
    public Vector2 moveInput;
    public Vector2 aimInput;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    private bool isRunning = false;


    [Header("Gravity")]
    public float gravity = -9.81f;
    public float groundedGravity = -0.5f;

    [Header("Aim")]
    [SerializeField] private LayerMask aimLayerMask = ~0;
    [SerializeField] private Transform aim; 

    private CharacterController controller;
    private float verticalVelocity = 0f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Character.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => moveInput = Vector2.zero;
        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;
        controls.Character.Run.performed += ctx => isRunning = true;
        controls.Character.Run.canceled += ctx => isRunning = false;    
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        // optional: snap to ground at start
        if (controller != null)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 1f;
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f))
            {
                float bottomLocalOffset = controller.center.y - (controller.height * 0.5f);
                float desiredY = hit.point.y - bottomLocalOffset;
                transform.position = new Vector3(transform.position.x, desiredY, transform.position.z);
            }
        }
    }

    void Update()
    {
        // 1) Rotate toward aim point first so forward is up-to-date for movement
       AimTowardsMouse();

        // 2) Move relative to updated forward/right
        ApplyMovement();
        AnimatorControllers();
    }

    private void AnimatorControllers()
    {
        Vector3 movementDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        if (animator != null)
        {
            float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
            float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);  
            float speedPercent = moveInput.magnitude;
            animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
            animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);
            animator.SetBool("isRunning", isRunning);
        }
    }   

    private void AimTowardsMouse()
    {
        if (aimInput != Vector2.zero && Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(aimInput);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
            {
                Vector3 lookPoint = hitInfo.point;
                lookPoint.y = transform.position.y;
                Vector3 lookDir = (lookPoint - transform.position).normalized;
                if (lookDir.sqrMagnitude > 0.0001f)
                {
                    Quaternion target = Quaternion.LookRotation(lookDir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
                }
            }
            aim.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }

    private void ApplyMovement()
    {
        // input vector and magnitude
        Vector2 input = moveInput;
        float inputMag = Mathf.Clamp01(input.magnitude);
        Vector3 inputDir = inputMag > 0f ? new Vector3(input.x, 0f, input.y).normalized : Vector3.zero;

        // horizontal movement relative to player orientation
        Vector3 horizontal = (transform.forward * inputDir.z + transform.right * inputDir.x) * moveSpeed * inputMag;

        // gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = groundedGravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = new Vector3(horizontal.x, verticalVelocity, horizontal.z);
        controller.Move(finalMove * Time.deltaTime);
    }
}
