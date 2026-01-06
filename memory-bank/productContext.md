# Product Context

## Why this project exists
Provide a compact, well-documented Unity top-down shooter prototype for learners and contributors to study input handling, movement, basic combat, and simple AI patterns.

## Problems it solves
- Teaches how to structure a small Unity game project for clarity and extensibility.
- Demonstrates modern Input System usage and modular player controls.
- Shows integration points for AI/navigation and basic combat without overwhelming complexity.

## How it should work
- A single SampleScene demonstrates player movement, aiming, shooting, and one or two enemy behaviors.
- Input is configurable through the Input System asset and mapped to a PlayerControls abstraction.
- Systems are decoupled: input => player controller => movement/aiming => weapon/shooting.
- Minimal, readable code with comments for educational value.

## User experience goals
- Immediate, responsive player controls with clear feedback (movement, shooting).
- Low cognitive load for new contributors: readable code, clear folder structure, and Memory Bank docs.
- Fast iteration: scenes and prefabs enable quick experimentation and learning.

## Target users
- Beginners learning Unity game architecture.
- Instructors creating short tutorials.
- Contributors exploring modular game systems.
