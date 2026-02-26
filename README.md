# Offline LEGO-Style Voxel Driving & Mining Sandbox (Unity C# Architecture)

This repository provides a production-oriented architecture scaffold for an **offline-only** 3D voxel sandbox driving/mining game for Windows PC.

## Key Guarantees
- Fully offline execution path (no network services required).
- Event-driven modular architecture.
- Manager separation for scalable expansion.
- Persian (Farsi) UX hooks with RTL alignment.
- Crash-safe mod loading with error logging.

## Manager Topology
- `GameManager`: runtime mode, god mode, offline flags.
- `EconomyManager`: progression economy, inventory, spend controls.
- `VehicleManager`: modular assembly, vehicle spawning, telemetry dispatch.
- `WorldManager`: chunk streaming, biome generation, world events.
- `AIManager`: A* pathfinding entrypoint and dynamic difficulty hooks.
- `MiningManager`: mining tool orchestration and resource event emission.
- `UIManager`: Persian localization bootstrap and engineering HUD.

## Core Systems Implemented
- **Voxel World**: chunk streamer + biome definitions + hazard event controller.
- **Vehicles**: LEGO-like component blocks, center-of-mass and telemetry calculations.
- **Mining**: laser, explosive, drill stubs + scanner signal API.
- **Automation**: robot/conveyor/factory processor hooks.
- **AI**: reusable A* pathfinder interface.
- **Mapping**: satellite map + path suggestion API.
- **Achievements**: stat tracking + deterministic weekly offline challenge generation.
- **Mod Support**: external `Mods/` auto detection and safe reload with persistent log.

## Game Modes
1. **Infinite Sandbox Mode**
   - Unlimited economy gate bypass in `EconomyManager`.
   - Optional God Mode in `GameManager`.
   - Intended for instant mining and unrestricted upgrades.
   - Explicitly surfaced as Sandbox/Modded by UI.

2. **Normal Progression Mode**
   - Full resource spending checks.
   - Economy and upgrade constraints enabled.
   - Supports missions, technology progression, and fuel balancing hooks.

## Farsi / RTL UI
`PersianLocalizationService` includes Farsi mission + tooltip keys and right-aligned rendering setup.

## Mod Security & Stability Notes
- Invalid mod files are isolated and logged in `Application.persistentDataPath/mod_errors.log`.
- Reloads are hot and do not require game restart.
- Event hooks can be extended for Lua/C# sandboxing via future script runtime adapters.

## How to Use in Unity
1. Create a bootstrap `GameObject`.
2. Attach all `Manager` MonoBehaviours to it.
3. Wire dependencies in the inspector (streamer, HUD, localization, etc.).
4. Add world/vehicle prefabs and connect telemetry + tools.
5. Build Windows standalone.
