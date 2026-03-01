# Astro

Astro is an action-packed asteroid shooter game built with Unity. Pilot your spaceship through an endless asteroid field, dodging incoming threats while blasting asteroids into fragments. Test your reflexes and survival instincts in this retro-inspired arcade experience.

## Features

- Fast-paced asteroid shooting gameplay with physics-based mechanics
- Multiple asteroid sizes that break into smaller pieces when destroyed
- Responsive spaceship controls with smooth rotation and movement
- Audio mixer with independent volume controls for music and sound effects
- Graphics settings with post-processing effects
- Cross-platform support including mobile (Android)
- Touch and keyboard input support
- Settings panel for audio and graphics customization
- Persistent save system for player preferences

## Technology Stack

- Unity Game Engine
- C# for game logic and mechanics
- ShaderLab for custom visual effects (46.9%)
- HLSL for advanced rendering (9.7%)
- Universal Render Pipeline (URP) for graphics

## Game Mechanics

### Spaceship Controls

Your spaceship is controlled with responsive input handling. Navigate through the asteroid field by rotating your ship and thrusting forward. The game supports both keyboard and touch controls for mobile platforms.

### Combat System

Fire your weapon to destroy incoming asteroids. The gun system features:
- Sound effects for each shot
- Knockback physics on projectiles
- Instantiated bullets with physics-based movement

### Asteroid Physics

Asteroids break apart into smaller pieces when hit, creating increasingly challenging gameplay:
- Large asteroids spawn smaller variants
- Each fragment has independent physics
- Collision detection with spaceship and bullets
- Physics-based movement and rotation

### Audio System

Astro features a comprehensive audio system with three independent volume channels:
- Master Volume: Overall game volume control
- Music Volume: Background music level
- SFX Volume: Sound effects level

All settings are saved and persist between game sessions.

### Graphics Settings

Customize your visual experience with toggleable post-processing effects. The game features:
- High-quality shader effects
- Distance field text rendering with TextMeshPro
- Customizable graphics quality settings
- Post-processing effects toggle


## How to Play

1. Use arrow keys or WASD to rotate and move your spaceship
2. Hold fire button or left mouse click to shoot
3. Destroy asteroids by shooting them before they hit your ship
4. Larger asteroids break into smaller, faster asteroids
5. Survive as long as possible and maximize your score
6. Access settings to adjust audio and graphics to your preference

## Mobile Controls

On Android devices:
- Use on-screen controls for movement
- Touch and drag to aim and shoot
- Responsive touch input for precise control

## Settings

Customize your game experience through the settings panel:
- Toggle background music on/off
- Adjust sound effects volume
- Enable/disable post-processing graphics effects
- All preferences are automatically saved

## Installation

1. Clone this repository
2. Open the project in Unity
3. Open the manager scene
4. Press Play in the Unity Editor or build for your target platform

## Building

To build the game:
1. Go to File > Build Settings
2. Select your target platform
3. Click Build and Run

The game supports PC (Windows, Mac, Linux) and Mobile (Android, iOS) platforms.

## Requirements

- Unity 2021 LTS or newer
- enjoy

## Author

Created by Quantum-Youssef-Amr

## License

This project is provided as-is for educational use.
