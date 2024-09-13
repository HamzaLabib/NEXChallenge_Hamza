# NEXChallenge_Hamza

1. Game Overview: Hello, as requested, I developed this prototype to serve as a simple proof of concept. While I have many ideas and techniques I'd like to work on for expanding this game, I focused on meeting the initial requirements to establish the test. Please take a look, and I look forward to discussing the next steps.



2. Scene Organization
A- Main Menu Scene (Scene 0): The entry point of the game, where players can start the game, access how-to-play instructions, or exit the game.

B- Gameplay Scene (Scene 1): This is the primary scene where the game takes place.
Contents: Includes the player character, enemy characters, and obstacles.

C- How to Play Scene (Scene 2): Provides a guide documentation to let players know how to interact with the game.



3. Game Mechanics
A- Player Mechanics:
Movement: Described using WASD or arrow keys.
Aiming: Handled through mouse movement.
Shooting: Activated with the left mouse button.
Death: Triggers when health reaches zero or falling down.

B- Enemy Mechanics:
AI Movement: Enemies chase the player once within a certain range.
AI Attack: Enemies shoot when close enough to the player and not in cooldown.
Health and Damage: How enemies take damage when got shot and what happens upon their death after health is over or falling down.



4. Script Descriptions
A- PlayerControl
Purpose: Manages all player movement and rotation based on keyboard input and mouse movement.

Major Methods:
Update(): Checks for player input to perform movement and rotation actions.
KeyboardMovement(): Handles horizontal and vertical movement based on player input using the WASD or arrow keys.
Jump(): Manages the player's ability to jump when the jump key is pressed and the player is grounded.
RotateToMouseCursor(): Rotates the player to face the direction of the mouse cursor.

Interactions:
Uses Unity's Input class to listen for key and mouse inputs.
Interacts with Rigidbody for physics-based movement.

B- PlayerAttack
Purpose: Handles the player's shooting mechanics, including bullet instantiation and direction based on mouse input.

Major Methods:
Update(): Listens for the shoot input (left mouse button) to trigger the shooting mechanism.
Shoot(): Instantiates bullets and applies force based on the direction calculated from the mouse cursor's position.

Interactions:
Uses Camera to calculate the shooting direction.
Interacts with the Rigidbody component of the bullet prefab to apply physics-based force.

C- PlayerHealth
Purpose: Manages the player's health, processes damage taken, and handles death.

Inherits from "HealthControl"

D- EnemyControl
Purpose: Controls enemy behavior, including movement towards the player, rotation to face the player, and handling attack sequences.

Major Methods:
Update(): Calls the FindTarget() method to update enemy movement and orientation each frame.
FindTarget(): Calculates the direction to the player and rotates towards the player.
Move(): Moves the enemy towards the player if outside a predefined range.
Attack(): Handles the attack logic when the enemy is close enough to the player.

Interactions:
Directly interacts with the Transform component of the player to orient and move towards the player.
Uses serialized fields to customize behavior such as speed and attack range.

E- EnemyHealth
Purpose: Manages the enemy's health, processes damage taken, and handles enemy death.

Inherits from "HealthControl"

F- HealthControl
Purpose: Manages the health logic, processes damage taken, and handles death logic.

Major Methods:
TakeDamage(int damage): Reduces the health of the child by a specified amount and calls Die() if health falls below zero.
Die(): Disables the child game object, controls and possibly triggers other game effects like spawning a death effect then triggers the end-game UI.

Interactions:
Interacts with other objects (like bullets) through collision detection.
Could interact with GameManager or other scripts to report enemy/player death for scorekeeping or game state changes.

E- GameManager
Purpose:Manages the overall game state, including scene transitions, and handles player win/lose conditions.

Major Methods:
Update(): Checks conditions to determine if the player has won or lost.
PlayerWin(), PlayerLose(): Handle the win and lose conditions by setting the appropriate messages and triggering UI changes.
MainMenuScene(), GameplayScene(), HowToPlay(), ExitGame(): Manage scene loading and exiting the game.

Instructions:
Directly interacts with the Unity Scene Management to load and unload scenes.
Uses serialized TextMeshProUGUI to display game results.
Manages player and enemy scripts indirectly by enabling/disabling them during end-game conditions.



5. Asset Directory
Assets/Prefabs: List and describe all prefabs used.
Assets/Scenes: Include all scenes by name and their purpose.
Assets/Materials: Outline any materials used for visual effects.
Assets/Audio: Detail background music and any sound effects.
Assets/Scripts: Contains sub folders for Managers, Player and Enemy scripts



6. Future Features
The game can be enhanced by incorporating more gameplay mechanics, optimization, and additional content. Below are proposed features and improvements:

A- Top-Down Based Architecture: Implement a clear hierarchical structure that separates concerns and organizes game components effectively. This will facilitate easier management of game states and interactions.

----Design Patterns: States, Manager, Singleton, Object Pool.----

B- States: Introduce a finite state machine for both the player and enemies to manage different states like attacking, idle and wandering.

C- Manager: Utilize manager classes for handling different aspects of game logic like spawning different types of enemies and tracking player progress.

D- Singleton: Apply this pattern for critical game components that should only have one instance, like GameManager, PlayerManager, and EnemyManager.

E- Object Pool: Implement object pooling for created and destroyed objects like bullets and effects, to minimize instantiating and destroying objects, which can lead to performance hitches.

F- NavMesh: Use NavMesh for AI pathfinding to allow enemies to intelligently navigate complex obstacles while pursuing the player.

G- Scene Baking: Bake the scene to optimize pathfinding performance and the game performance in general.

H- Animation for Player, Enemy and UI.

I- Design multiple levels with increasing difficulty and complexity. Each level could introduce different enemies, obstacles, and timer.

J- Asset Bundle.


Yours,
Hamza Labib