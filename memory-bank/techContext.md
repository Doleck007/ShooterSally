# Tech Context

## Technologies
- Unity 2021+ (Editor + Runtime)
- C# (Mono/.NET used by Unity)
- Unity Input System package (PlayerControls/InputActions)
- Universal Render Pipeline (optional, project uses prototype materials)
- Unity NavMesh (optional for enemy navigation)
- Visual Studio Code + Unity integration

## Development setup
- Open the Unity project folder in Unity Editor; open scripts in VS Code.
- Ensure Input System package installed and Input Actions asset set to "Generate C# Class".
- Use Unity package manager for installing/removing packages; prefer package manifest for reproducibility.
- Use Git for source control; keep Library/ and Temp/ out of VCS.

## Dependencies & files of interest
- Assets/PlayerControls.inputactions (generated PlayerControls.cs)
- Assets/PlayerMovement.cs, other gameplay scripts under Assets/
- Packages/manifest.json defines project packages

## Constraints & patterns
- Target platform: desktop (PC) for tutorials; mobile not prioritized.
- Prefer FixedUpdate for physics movement, Update for input processing.
- Normalize input vectors to avoid diagonal speed issues.
- Pool frequently spawned objects (projectiles) to reduce GC and instantiation cost.

## Tooling & workflows
- Code edits made in VS Code; test in Unity Play mode.
- Use Prefabs for modularity; ScriptableObjects for tunables.
- Keep editor-only utilities separate under Editor/ when needed.
