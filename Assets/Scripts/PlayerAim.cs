using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    private Player playerRef;
    private PlayerControls controls;
    private Vector2 mouseInput;


    [Header("Aim Control")]
    [SerializeField] private Transform aim;

    [SerializeField] private bool isAimingPricision;
    
    [Header("Camera control")]
    [SerializeField] private Transform cameraTarget;
    [Range(1f, 1.5f)]
    [SerializeField] private float minCameraDistance = 1.5f;
    [Range(1f, 5f)]
    [SerializeField] private float maxCameraDistance = 4f;

    [Range(2f, 8f)]
    [SerializeField] private float cameraSensitivity = 5f;
    
    [Space]
    [SerializeField] private LayerMask aimLayerMask;
   // [SerializeField] private float aimTargetHeight = 1f;
    private Vector3 lookingDirection;

    private RaycastHit lastKnownMouseHit;



    private void Start()
    {
        playerRef = GetComponent<Player>();
        controls = playerRef.controls;
        AssignInputEvents();
    }


    private void Update()
    {
        aim.position = GetMousePosition().point;

        if (!isAimingPricision)
           aim.position = new Vector3(aim.position.x, transform.position.y + 1, aim.position.z);
     
        cameraTarget.position = Vector3.Lerp(cameraTarget.position, DesiredCameraPosition(), cameraSensitivity * Time.deltaTime);   

    }

    public bool CanAimPrecisly()
    {
        return isAimingPricision;
    }

    public RaycastHit GetMousePosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(mouseInput);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            lastKnownMouseHit = hitInfo;
            return  hitInfo;
           
        }
        return lastKnownMouseHit;
    }

    private void AssignInputEvents()
    {
        controls = playerRef.controls;
        controls.Character.Aim.performed += OnAimPerformed;
        controls.Character.Aim.canceled += OnAimCanceled;
        controls.Character.AimPrecisly.performed += OnAimTogglePerformed;
        controls.Character.AimPrecisly.canceled += OnAimToggleCanceled;
    }
    private Vector3 DesiredCameraPosition()
    {
   
       float actualMaxCameraDistance = playerRef.movement.moveInput.y < - .5f ? minCameraDistance : maxCameraDistance;
        
        Vector3 desiredCameraPosition = GetMousePosition().point;
        Vector3 aimDirection = (desiredCameraPosition - transform.position).normalized;

        float distanceToDesiredPosition = Vector3.Distance(transform.position, desiredCameraPosition);
        float clampedDistance = Mathf.Clamp(distanceToDesiredPosition, minCameraDistance, actualMaxCameraDistance);

        desiredCameraPosition = transform.position + aimDirection * clampedDistance;
        desiredCameraPosition.y = transform.position.y + 1;

        return desiredCameraPosition;
    }

    private void OnAimPerformed(InputAction.CallbackContext ctx) => mouseInput = ctx.ReadValue<Vector2>();
    private void OnAimCanceled(InputAction.CallbackContext ctx) => mouseInput = Vector2.zero;
    private void OnAimTogglePerformed(InputAction.CallbackContext ctx) => isAimingPricision = true;
    private void OnAimToggleCanceled(InputAction.CallbackContext ctx) => isAimingPricision = false;
}
