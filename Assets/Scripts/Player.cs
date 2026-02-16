using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerControls controls;
    public PlayerAim playerAim { get; private set; }
    public PlayerMovement movement { get; private set; }

    private void Awake()
    {
        controls = new PlayerControls();
        playerAim = GetComponent<PlayerAim>(); 
        movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

}
