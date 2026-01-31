using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private WeaponVisualController weaponVisualController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponVisualController = GetComponentInParent<WeaponVisualController>();
    }

    public void OnWeaponChangeAnimationEvent()
    {
        Debug.Log("OnWeaponChangeAnimationEvent: weapon change animation completed indes is");
        if (weaponVisualController != null)
        {
            weaponVisualController.OnWeaponChangeAnimationEvent();
        }
    }
    public void OnReloadAnimationEvent()
    {
        Debug.Log("OnReloadAnimationEvent: reload animation event called");
        if (weaponVisualController != null)
        {
            weaponVisualController.ReturnRigWeightToOne();
        }
    }
  
}
