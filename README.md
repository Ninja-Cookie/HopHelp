# Hop Help

"Hop Help" is a tool that adds useful commands to the Developer console.

(Developer has asked not to reveal how to enable it, and this won't enable it for you)

---


Requires BepInEx (https://github.com/BepInEx/BepInEx/releases)

Place the DLL anywhere inside the "/BepInEx/Plugins/" folder.

Everything Added:
- Bind
- Unbind
- SetPosition
- SavePositionExact
- SavePosition
- LoadPosition
- GetPosition
- ToggleShowTriggers
- ToggleWarpDisplay
- Poke
- SetSpeed
- SetFPS
- GetNamesOf
- ForceActive
- ForceUnactive
- ForceActiveToggle
- ToggleShowWallrun
- SetTimescale

# Command Details:

### Bind a key to a console command (These only work if console is enabled)
- Bind KEY "COMMAND; ANOTHER COMMAND"

`Example: Bind G "LoadPosition Slot4; SetSpeed 200"`

#### Unbind a key
- Unbind KEY

`Example: Unbind G`

#### Set the players position
- SetPosition X Y Z

`Example: SetPosition 55.31 -23 8992.522`

#### Save players position to X Y Z (Default: Slot1)
- SavePositionExact X Y Z (Slot)

`Example: SavePositionExact 55.31 -23 8992.522 Slot4`

#### Save players current position (Default: Slot1)
- SavePosition (Slot)

`Example: SavePosition Slot8`

#### Load players position from Slot (Default: Slot1)
- LoadPosition (Slot)

`Example: LoadPosition Slot8`

#### Get players current position
- GetPosition

`Example: GetPosition`

#### Toggle Showing Triggers (Beta)
- ToggleShowTriggers

`Example: ToggleShowTriggers`

#### Toggle Showing a Warp Position
- ToggleWarpDisplay TYPE

`Example: ToggleWarpDisplay Death`

#### Set Current Speed Foward
- SetSpeed SPEED

`Example: SetSpeed 500`

#### Poke at a game object variable and see its value
- Poke CHAIN

`Example: Poke PlayerItem.Health.Health`

#### Set FPS
- SetFPS FPS

`Example: SetFPS 30`

#### Get internal names of types of objects in the scene
- GetNamesOf CLASS

`Example: GetNamesOf VoidHole`

#### Force an object to be active
- ForceActive OBJECT-NAME

`Example: ForceActive voidtower`

#### Force an object to not be active
- ForceUnactive OBJECT-NAME

`Example: ForceUnactive voidtower`

#### Force an object to change current active state
- ForceActiveToggle OBJECT-NAME

`Example: ForceActiveToggle voidtower`

#### Show wallrun jump angle prediction
- ToggleShowWallrun

`Example: ToggleShowWallrun`

#### Set game speed / timescale
- SetTimescale VALUE

`Example: SetTimescale 0.5`
