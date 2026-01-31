using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    private Player playerRef;
    private PlayerControls controls;
    private Vector2 aimInput;


    [Header("Aim")]
    [SerializeField] private LayerMask aimLayerMask = ~0;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimTargetHeight = 1.2f;
    private Vector3 lookingDirection;



    private void Start()
    {
        playerRef = GetComponent<Player>();
        AssignInputEvents();
    }


    private void Update()
    {
        GetMousePosition();
        aimTarget.position = GetMousePosition() + Vector3.up * aimTargetHeight;
    }

    public Vector3 GetMousePosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(aimInput);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
        {
            return  hitInfo.point;
           
        }
        return Vector3.zero;
    }

    private void AssignInputEvents()
    {
        controls = playerRef.controls;
        controls.Character.Aim.performed += OnAimPerformed;
        controls.Character.Aim.canceled += OnAimCanceled;
    }


    private void OnAimPerformed(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    private void OnAimCanceled(InputAction.CallbackContext ctx) => aimInput = Vector2.zero;
}
