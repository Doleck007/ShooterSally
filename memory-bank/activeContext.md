# Active Context

## Current Work Focus
- Reviewing Assets/PlayerMovement.cs and Assets/PlayerControls.cs to verify Input System integration and movement responsiveness.
- Ensure player movement uses physics-friendly patterns (FixedUpdate for movement, normalized input vectors) and that PlayerControls generated class is wired correctly.

## Recent Changes
- Created memory-bank core files: projectbrief.md, productContext.md, systemPatterns.md, techContext.md.
- Refactored Assets/PlayerMovement.cs to:
  - Read input via Input System callbacks (existing).
  - Handle aiming/rotation in Update() for responsiveness.
  - Apply movement and gravity in FixedUpdate() using normalized input and input magnitude to avoid diagonal speed boost.
  - Use fixedDeltaTime for physics and gravity integration.

## Next Steps
- Verify changes in Unity Play mode (movement, aiming, gravity behavior).
- Confirm PlayerControls input map works as expected (Movement, Aim, Fire).
- Update progress.md with verification results and any follow-up fixes.
- Add brief unit/manual test steps for movement responsiveness and aiming accuracy.
- Consider pooling for projectiles when implementing shooting.

## Active Decisions & Considerations
- Movement implementation will favor FixedUpdate for physics.
- Use Input System generated PlayerControls wrapper rather than polling Input.GetAxis.
- Keep public API small and testable; prefer serialized fields over hard-coded values.

## Important Patterns & Preferences
- Single Responsibility per component.
- Explicit event callbacks for shooting/health changes.
- ScriptableObjects for tunables where appropriate.

## Learnings / Notes
- Memory Bank created and seeded; must be read at start of every session.
- PlayerMovement.cs was refactored to improve responsiveness and correct physics timing.
- PlayerControls.cs matches expected "Character" action map with Movement, Aim, Fire actions.
