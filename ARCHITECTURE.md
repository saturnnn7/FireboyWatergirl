# FBW — Game Architecture

## Tile Size
Base tile: **32x32px**

## Tile Types

### Basic Blocks
| Type | Size | Notes |
|---|---|---|
| `SolidBlock` | 32x32 | Standard solid block |
| `HalfBlock` | 32x16 | Half-height block |
| `QuarterBlock` | 16x16 | Quarter-size block |
| `SlopeBlock` | 32x32 | 45° diagonal |
| `SmallSlopeBlock` | 16x16 | 45° diagonal, small |
| `StairBlockLeft` | 32x32 | Left part of stair pair |
| `StairBlockRight` | 32x32 | Right part of stair pair |
| `Ladder` | 32x32 | Climbable tile |
| `Empty` | — | Air, no collision |

All blocks support:
- **Rotation:** 0°, 90°, 180°, 270°
- **Texture template:** swappable visual theme, collision unchanged

### Hazard Blocks
| Type | Kills |
|---|---|
| `FireTile` | Watergirl |
| `WaterTile` | Fireboy |
| `PoisonTile` | Both players |

Hazard blocks share the same collision/rotation system as basic blocks.

---

## Entity Types

### Players
| Type | Notes |
|---|---|
| `SpawnPoint` | Tied to `PlayerType` (Fireboy / Watergirl) |

### Goal
| Type | Notes |
|---|---|
| `Door` | End of level OR transition to another room. Behavior set in editor. |

### Collectibles
| Type | Collected by |
|---|---|
| `GemFire` | Fireboy only |
| `GemWater` | Watergirl only |
| `GemNeutral` | Either player |

### Puddles
| Type | Kills |
|---|---|
| `PuddleFire` | Watergirl |
| `PuddleWater` | Fireboy |
| `PuddlePoison` | Both players |

Puddles are entities (not tiles) because they have arbitrary width/height not tied to the tile grid.

### Activators
| Type | Notes |
|---|---|
| `Lever` | Toggle on/off, linked to target entities |
| `ButtonInstant` | Active while stood on, no delay |
| `ButtonTimer` | Active for `duration` seconds after triggered |

### Activated Objects
| Type | Notes |
|---|---|
| `Piston` | Acts as a door — blocks/unblocks passage |
| `MovingPlatform` | Moves along a defined path, two modes (see below) |

### Physics Objects
| Type | Notes |
|---|---|
| `Box` | Pushable by players, affected by gravity |

### Laser System
| Type | Notes |
|---|---|
| `LightSource` | Emits a laser beam at a set angle |
| `LightSlider` | Player pushes it to rotate `LightSource` angle within 0–90° |
| `Mirror` | Static 45° slope block, reflects laser beam |
| `LightReceiver` | Activates `linkedTo[]` entities when hit by laser |

---

## MovingPlatform Modes

| Mode | Behavior |
|---|---|
| `Auto` | Moves continuously between start and end point, pauses at each end |
| `Triggered` | Stays at start point until activated, then moves to end point |

---

## Linking System

Activators reference target entities by `id` via a `linkedTo` array.

```json
{
  "id": "btn_01",
  "type": "ButtonTimer",
  "duration": 3.0,
  "linkedTo": ["piston_01", "platform_02"]
}
```

Supported activators: `Lever`, `ButtonInstant`, `ButtonTimer`, `LightReceiver`
Supported targets: `Piston`, `MovingPlatform`, `LightSource`, `Door`

---

## Level JSON Structure

```json
{
  "id": "uuid",
  "title": "Level Name",
  "authorId": "uuid",
  "rooms": [
    {
      "id": 1,
      "width": 40,
      "height": 25,
      "assignedPlayer": "both",
      "tileTheme": "default",
      "tiles": [
        {
          "x": 0,
          "y": 24,
          "type": "SolidBlock",
          "rotation": 0,
          "textureTemplate": "default"
        }
      ],
      "entities": [
        {
          "id": "lever_01",
          "type": "Lever",
          "x": 10.0,
          "y": 18.0,
          "linkedTo": ["piston_01"]
        },
        {
          "id": "piston_01",
          "type": "Piston",
          "x": 20.0,
          "y": 10.0
        },
        {
          "id": "platform_01",
          "type": "MovingPlatform",
          "x": 15.0,
          "y": 15.0,
          "mode": "Auto",
          "pathEnd": { "x": 25.0, "y": 15.0 },
          "pauseDuration": 1.0
        }
      ],
      "spawnPoints": {
        "fireboy":   { "x": 2, "y": 22 },
        "watergirl": { "x": 4, "y": 22 }
      },
      "exits": [
        {
          "doorId": "door_01",
          "type": "NextRoom",
          "targetRoomId": 2
        },
        {
          "doorId": "door_02",
          "type": "EndLevel"
        }
      ]
    }
  ]
}
```

---

## Input System

Two input groups (`GroupA`, `GroupB`), each fully remappable.
Default assignment: `GroupA` → Fireboy, `GroupB` → Watergirl.
Groups can be swapped at any time during gameplay.
Saved locally in `settings.json`.

```json
{
  "inputGroups": [
    {
      "id": "A",
      "bindings": {
        "moveLeft":  "A",
        "moveRight": "D",
        "jump":      "W"
      }
    },
    {
      "id": "B",
      "bindings": {
        "moveLeft":  "Left",
        "moveRight": "Right",
        "jump":      "Up"
      }
    }
  ],
  "groupAssignments": {
    "fireboy":   "A",
    "watergirl": "B"
  }
}
```

---

## Render Layers

| Layer | Content |
|---|---|
| 0 | Background |
| 1 | Tiles |
| 2 | Entities (puddles, boxes, platforms) |
| 3 | Players |
| 4 | Laser beams |
| 5 | UI / HUD |

---

## Player Modes

| Mode | Description |
|---|---|
| `Coop` | Both Fireboy and Watergirl, one keyboard (two input groups) |
| `SingleFireboy` | Only Fireboy, room `assignedPlayer: fireboy` |
| `SingleWatergirl` | Only Watergirl, room `assignedPlayer: watergirl` |
