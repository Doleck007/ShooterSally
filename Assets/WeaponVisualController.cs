using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem;

public class WeaponVisualController : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransforms;
    [SerializeField] private Transform pistol;
    [SerializeField] private Transform revolver;
    [SerializeField] private Transform autoRifle;
    [SerializeField] private Transform shotgun;
    [SerializeField] private Transform sniperRifle;
    private Animator animator;

    private int currentWeaponIndex;
    private Transform currentLeftHandTarget;

    [Header("Left hand IK")]
    [SerializeField] private Transform leftHandIK;
    private Rig rig;
    private int weaponShouldChange = 0;



    private Player playerRef;
    private PlayerControls controls;
    private bool ownsControls = false;

    private void Awake()
    {
        playerRef = GetComponentInParent<Player>();
        animator = GetComponentInChildren<Animator>();


    }


    private void Start()
    {
        playerRef = GetComponent<Player>();
        controls = playerRef.controls;
        rig = GetComponentInChildren<Rig>();
        controls.Character.GunSelect.performed += OnGunSelectPerformed;
       // controls.Character.GunSelect.performed -= OnGunSelectPerformed;

        if (rig == null)
        {
            Debug.LogWarning("WeaponVisualController: No Rig component found. Make sure RigBuilder is on a parent.");
        }
        SwitchToWeapon(0);
    }

    private void OnGunSelectPerformed(InputAction.CallbackContext ctx)
    {
        Debug.LogWarning("OnGunSelectPerformed: weapon change call back");
        CycleToNextWeapon();
    }
    private void Update()
    {

    }

    private void CycleToNextWeapon()
    {
        if (gunTransforms == null || gunTransforms.Length == 0) return;

        currentWeaponIndex = (currentWeaponIndex + 1) % gunTransforms.Length;
        rig.weight = 0;
        animator.SetTrigger("ChangeWeapon");
        // animator.GetBool
        //SwitchToWeapon(currentWeaponIndex);
    }

    private void SwitchToWeapon(int index)
    {
        SetAllWeaponsActive(false);

        if (index >= 0 && index < gunTransforms.Length)
        {
            gunTransforms[index].gameObject.SetActive(true);
            UpdateLeftHandTarget(gunTransforms[index]);
        }
        SwitchAnimationLayer(index);
    }

    private void UpdateLeftHandTarget(Transform weapon)
    {
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
    private void SwitchAnimationLayer(int layerIndexGunIndex)
    {
        for (int i = 1; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        if (layerIndexGunIndex == 0 || layerIndexGunIndex == 1 || layerIndexGunIndex == 2) animator.SetLayerWeight(1, 1); // default layer
        if (layerIndexGunIndex == 3) animator.SetLayerWeight(2, 1); // shotgun layer
        if (layerIndexGunIndex  == 4) animator.SetLayerWeight(3, 1); // sniper layer

    }

    public void OnWeaponChangeAnimationEvent()
    {
        Debug.LogWarning("OnWeaponChangeAnimationEvent: weapon change animation completed indes is: " + currentWeaponIndex);
        rig.weight = 1;
        SwitchToWeapon(currentWeaponIndex);
        
    }

}
