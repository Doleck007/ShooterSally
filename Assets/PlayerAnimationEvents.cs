using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerWeaponVisualController playerWeaponVisualController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerWeaponVisualController = GetComponentInParent<PlayerWeaponVisualController>();
    }

    public void OnWeaponChangeAnimationEvent()
    {
        Debug.Log("OnWeaponChangeAnimationEvent: weapon change animation completed indes is");
        if (playerWeaponVisualController != null)
        {
            playerWeaponVisualController.OnWeaponChangeAnimationEvent();
        }
    }
    public void OnReloadAnimationEvent()
    {
        Debug.Log("OnReloadAnimationEvent: reload animation event called");
        if (playerWeaponVisualController != null)
        {
            playerWeaponVisualController.ReturnRigWeightToOne();
        }
    }
  
}
