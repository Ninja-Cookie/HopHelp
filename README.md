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
- ToggleShowDeathWarp
- Poke
- SetSpeed

# Command Details:

### Bind a key to a console command (These only work if console is enabled)
- Bind KEY "COMMAND"

`Example: Bind G "LoadPosition Slot4"`

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

#### Toggle Showing Death Warp Position
- ToggleShowDeathWarp

`Example: ToggleShowDeathWarp`

#### Set Current Speed Foward
- SetSpeed SPEED

`Example: SetSpeed 500`

#### Poke at a game object variable and see its value
- Poke CHAIN

`Example: Poke PlayerItem.Health.Health`
