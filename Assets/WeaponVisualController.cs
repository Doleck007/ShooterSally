using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransforms;
    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform sniperRifle;

    private int currentWeaponIndex;
    private Transform currentLeftHandTarget;

    [Header("Left hand IK")]
    [SerializeField] private Transform leftHandIK;

    private Player playerRef;
    private PlayerControls controls;
    private bool ownsControls = false;

    private void Awake()
    {
        playerRef = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        if (playerRef != null && playerRef.controls != null)
        {
            controls = playerRef.controls;
            ownsControls = false;
        }
        else
        {
            controls = new PlayerControls();
            ownsControls = true;
        }

        if (ownsControls)
            controls.Enable();

        controls.Character.GunSelect.performed += OnGunSelectPerformed;
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Character.GunSelect.performed -= OnGunSelectPerformed;

            if (ownsControls)
                controls.Disable();
        }
    }

    private void Start()
    {
        SwitchToWeapon(0);
    }

    private void OnGunSelectPerformed(InputAction.CallbackContext ctx)
    {
        CycleToNextWeapon();
    }

    private void CycleToNextWeapon()
    {
        if (gunTransforms == null || gunTransforms.Length == 0) return;

        currentWeaponIndex = (currentWeaponIndex + 1) % gunTransforms.Length;
        SwitchToWeapon(currentWeaponIndex);
    }

    private void SwitchToWeapon(int index)
    {
        SetAllWeaponsActive(false);

        if (index >= 0 && index < gunTransforms.Length)
        {
            gunTransforms[index].gameObject.SetActive(true);
            UpdateLeftHandTarget(gunTransforms[index]);
        }
    }

    private void UpdateLeftHandTarget(Transform weapon)
    {
        //currentLeftHandTarget = weapon.Find("LeftHand_TargetTransform");
        //currentLeftHandTarget = weapon.GetComponentInChildren<"LeftHand_TargetTransform">().transform;
        //weapon.GetComponentInChildren<"LeftHand_TargetTransform">();
        currentLeftHandTarget = weapon.GetComponentInChildren<LeftHandTargetTransform>().transform;
        leftHandIK.localPosition = currentLeftHandTarget.localPosition;
        leftHandIK.localRotation = currentLeftHandTarget.localRotation;
    }

    private void SetAllWeaponsActive(bool active)
    {
        foreach (Transform gun in gunTransforms)
        {
            if (gun != null)
            {
                gun.gameObject.SetActive(active);
            }
        }
    }
}
