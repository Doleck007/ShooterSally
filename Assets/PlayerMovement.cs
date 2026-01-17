using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Player playerRef;
    private PlayerControls controls;
    private CharacterController controller;
    private Animator animator;

    private Vector2 moveInput;
    private Vector2 aimInput;
    private Vector3 currentVelocity;
    private float verticalVelocity;
    private bool isRunning;
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float groundedGravity = -2f;

    [Header("Aim")]
    [SerializeField] private LayerMask aimLayerMask = ~0;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimTargetHeight = 1.2f;

    private Vector3 aimWorldPosition;

    private void Awake()
    {
        // cache references only; defer controls setup to OnEnable
        // playerRef = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
        // controller is assigned in Start to match original ordering
    }
    private void Start()
    {
        playerRef = GetComponent<Player>();
        controls = playerRef.controls;

        controller = GetComponent<CharacterController>();
        SnapToGround();
        AssignInputEvents();
    }

    private void AssignInputEvents()
    {
        controls.Character.Movement.performed += OnMovePerformed;
        controls.Character.Movement.canceled += OnMoveCanceled;
        controls.Character.Aim.performed += OnAimPerformed;
        controls.Character.Aim.canceled += OnAimCanceled;
        controls.Character.Run.performed += OnRunPerformed;
        controls.Character.Run.canceled += OnRunCanceled;

    }

    private void OnMovePerformed(InputAction.CallbackContext ctx) => moveInput = ctx.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext ctx) => moveInput = Vector2.zero;
    private void OnAimPerformed(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    private void OnAimCanceled(InputAction.CallbackContext ctx) => aimInput = Vector2.zero;
    private void OnRunPerformed(InputAction.CallbackContext ctx) => isRunning = true;
    private void OnRunCanceled(InputAction.CallbackContext ctx) => isRunning = false;


    private void Update()
    {
        UpdateAimPosition();
        RotateTowardsAim();
        ApplyMovement();
        UpdateAnimator();
    }

    private void SnapToGround()
    {
        if (controller == null) return;

        Vector3 rayOrigin = transform.position + Vector3.up;
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f))
        {
            float bottomOffset = controller.center.y - (controller.height * 0.5f);
            transform.position = new Vector3(transform.position.x, hit.point.y - bottomOffset, transform.position.z);
        }
    }

    private void UpdateAimPosition()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimLayerMask))
        {
            aimWorldPosition = hit.point;
            aimWorldPosition.y = transform.position.y + aimTargetHeight;

            if (aimTarget != null)
            {
                aimTarget.position = aimWorldPosition;
            }
        }
    }

    private void RotateTowardsAim()
    {
        Vector3 aimDirection = aimWorldPosition - transform.position;
        aimDirection.y = 0f;

        if (aimDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection.normalized);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void ApplyMovement()
    {
        if (controller == null) return;

        float inputMagnitude = Mathf.Clamp01(moveInput.magnitude);
        Vector3 inputDirection = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 targetVelocity = inputDirection * targetSpeed * inputMagnitude;

        float smoothRate = inputMagnitude > 0.1f ? acceleration : deceleration;
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, smoothRate * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded)
        {
            verticalVelocity = groundedGravity;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMovement = new Vector3(currentVelocity.x, verticalVelocity, currentVelocity.z);
        controller.Move(finalMovement * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;

        Vector3 localVelocity = transform.InverseTransformDirection(currentVelocity);
        float speed = currentVelocity.magnitude / Mathf.Max(0.0001f, walkSpeed);

        float xVelocity = localVelocity.x / walkSpeed;
        float zVelocity = localVelocity.z / walkSpeed;

        animator.SetFloat("xVelocity", xVelocity, 0.1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, 0.1f, Time.deltaTime);
        animator.SetBool("isRunning", isRunning && speed > 0.1f);
    }

    // Public method for touch input integration
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    // Public method for touch aim integration
    public void SetAimWorldPosition(Vector3 worldPos)
    {
        aimWorldPosition = worldPos;
        aimWorldPosition.y = transform.position.y + aimTargetHeight;
    }
}
