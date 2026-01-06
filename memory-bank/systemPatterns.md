# System Patterns

## Architecture Overview
Small, modular, and educational architecture focused on clarity:
- Input layer (Input System + PlayerControls) → Player controller → Movement / Aiming → Weapon / Shooting → Feedback (VFX/SFX/UI)
- Systems communicate via explicit method calls and lightweight events; avoid hidden global state.

## Key Components
- PlayerControls (InputActions asset + generated C# wrapper)
- PlayerMovement / PlayerController — single responsibility: translate input to motion
- Weapon / Shooter — handles firing logic, cooldowns, projectile spawning
- Enemy AI — small behavior tree or state machine for patrol, chase, attack
- Navigation — use Unity NavMesh for pathing or simple steering for prototype

## Design Patterns
- Single Responsibility Principle: each MonoBehaviour handles one concern.
- Dependency Injection (constructor-like assignment or serialized fields) for testability.
- Event-driven callbacks for decoupling (C# events or UnityEvent for editor wiring).
- ScriptableObjects for shared configuration (tuning values, weapon data).

## Component Relationships
- Player GameObject:
  - PlayerInput (Input System) referencing PlayerControls
  - PlayerController component (reads input, moves Rigidbody/CharacterController)
  - Weapon component(s) as children or attached modules
- Enemy prefabs:
  - AI controller
  - NavMeshAgent (optional)
  - Health / Damage handlers

## Implementation Notes
- Keep Update usage minimal; prefer FixedUpdate for physics-related movement.
- Normalize input vectors before applying speed to avoid diagonal speed boosts.
- Pool projectiles for performance when firing rapidly.
- Keep public methods small and test-friendly.

## Critical Paths
- Movement responsiveness: input → processed direction → physics movement (FixedUpdate)
- Shooting: input → weapon.Fire() → spawn projectile → apply velocity/damage
- Enemy pathing: target acquisition → path calculation → movement along path

## Patterns to Avoid
- Heavy logic inside Update for many objects.
- Tight coupling between UI and game logic; use events to inform UI.
- Using Find* calls at runtime for frequent access—cache references.
