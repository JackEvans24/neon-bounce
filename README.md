# Amuzo Bounce

## How to play

**Step 1 - Place a platform**

You are given a platform (preview in the bottom-right of the screen). Click to place it anywhere on the screen.

Platforms must be a suitable distance from any existing platforms.

**Step 2 - Drop the ball**

Click anywhere on the screen to drop a ball in that position. Your goal is to reach the score target (shown in the top-right of the screen).

Every time the ball bounces on a **PINK** platform the **PINK** value raises by one. Every time the ball bounces on a **GREEN** platform the **GREEN** value raises by one. Your total score is the product of these two values.

When the ball falls off the screen the turn is over, and you can drop another ball. If you haven't reached the score target after three attempts, the game is over.

**Step 3 - Repeat**

Once you pass the score target, you are gifted another platform to place. Play for as long as you like and reach the highest score you can - mine was something like 2500.

## Features

- There is a checkbox on the `GameStateManager` object to turn Assist Mode on/off. When assist mode is on, the game will show where you last placed a ball, and where it fell off the screen.
- There is a `CameraShake.cs` script on the `Main Camera` object - play with the exposed values to mess with camera shake intensity.
- There is a `VolumeIntensifier.cs` script on the `Main Camera/Global Volume` object - play with the exposed values to mess with bloom and chromatic aberration on bounce.

## Next steps

As always, features had to be cut, and I'm aware of places where the game design has some shortcomings. Things I would focus on next would include:

- Code quality
    - Better separation of state and UI - A single game controller script was refactored into a state machine to better handle things like switching between placement/play/gameover states, but in a more complete project I would go further, using the states to handle game state (duh), and separate view classes to handle Game UI. This perhaps would have been excessive for this project, but it creates a good foundation to work from.
    - Command pattern for how beams interact with the rest of the game - having the score struct take a `BeamData` param to work out what it should do is way less extensible than having the beams tell the score what to do. Also opens the door for different effects with lives and the beams themselves.
- QoL
    - Confirm button when placing a platform.
    - Show platform overlap areas - it's annoying to have to guess how close to another beam you can place the next one.
    - Ability to sell existing shapes to make room for new ones.
    - Ability to skip shape placement for a score bonus.
- Audio
    - Use Fmod to pitch bend a collision noise based on how high your score is, so that the noise starts low and rises in pitch and volume as you gain more points.
    - High tempo background music - high pass when placing a new shape
    - SFX
        - Level complete
        - Ball dropped
        - Ball out of bounds
        - Beam placed
        - Beam placement invalid
- Content
    - More shapes - e.g. Chevrons, Diamonds
    - Shape modifiers - e.g.:
        - Friction
        - Increased ball velocity
        - Disappear on hit
        - Moving beams
        - Ball saver which reduces score
- Juice
    - Ball squash and stretch
    - Better text animation