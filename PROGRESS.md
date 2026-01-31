# Top-Down Shooter Progress

## Project Overview
iOS top-down shooter with oil industry theme (Clash of Clans meets Landman). Players raid other players' bases to steal oil and equipment.

## Completed

### Player Movement (PlayerMovement.cs)
- WASD movement relative to player facing direction
- Mouse aiming (player rotates to face cursor)
- Walk/run toggle (Left Shift)
- Smooth acceleration/deceleration
- CharacterController-based movement with gravity
- Animator integration (xVelocity, zVelocity, isRunning parameters)
- Touch-ready with `SetMoveInput()` and `SetAimWorldPosition()` methods

### Input System (PlayerControls.inputactions)
- Movement: WASD
- Aim: Mouse position
- Fire: Left mouse button
- Run: Left Shift

### Camera
- Cinemachine 3.x setup
- CinemachineCamera with Position Composer tracking player
- CinemachineBrain on Main Camera

### Shooting (PlayerShooting.cs)
- Script created with raycast shooting
- Fire input connected
- Fire rate control
- IDamageable interface for enemies
- Muzzle flash/hit effect/trail support (prefabs not assigned)

### Animation Layer Blending
- Avatar Mask created for upper body
- Common Weapon Layer configured with mask
- Shooting animations blend with movement ✅

## In Progress

### Rig Aim Constraints
Setting up Animation Rigging for procedural aiming:
- Multi-Aim Constraint for spine/chest to look at target
- Two Bone IK for arms (if needed)
- Aim target follows mouse/crosshair position

## Next Steps
1. Complete rig aim constraint setup
2. Add muzzle flash / hit effects
3. Create enemy with IDamageable
4. Basic enemy AI (patrol + shoot)
5. Health/damage system
6. iOS touch controls (virtual joysticks)

## File Locations
- `Assets/PlayerMovement.cs` - Movement controller
- `Assets/PlayerShooting.cs` - Shooting mechanics
- `Assets/PlayerControls.cs` - Auto-generated input actions
- `Assets/PlayerControls.inputactions` - Input action asset

## Animator Parameters
- xVelocity (float)
- zVelocity (float)
- isRunning (bool)
- Fire (trigger)
