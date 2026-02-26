# Offline LEGO-Style Voxel Driving & Mining Sandbox (Unity C# Architecture)

This repository provides a **playable architecture baseline** for an offline-only 3D voxel sandbox driving/mining game for Windows PC.

## What was upgraded in this revision
- Real game mode rule-book (`GameModeRules`) with explicit Sandbox and Progression switches.
- Runtime mode switching now propagates correctly from `GameManager` through `GameBootstrap` to all listeners.
- Chunk streaming now reports dynamic LOD per chunk.
- Biome generator now evaluates biome types from procedural noise (surface and underground).
- Vehicle simulation now includes fuel, wheel torque distribution, and telemetry including fuel-level.
- Mining tools support on-foot and while-driving operation with cooldown logic.
- World events now include meteor, earthquake, and rare vein spawn events.
- Mod system now validates JSON manifest schema and script extension to avoid invalid mod crashes.

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

## Game Modes
1. **Infinite Sandbox Mode (Offline Only)**
   - Unlimited economy spending and unlock flags.
   - Instant mining support.
   - Infinite fuel protection.
   - Optional god mode.
   - Explicit Sandbox/Modded badge support in HUD.

2. **Normal Progression Mode**
   - Resource costs and inventory limits.
   - Upgrade and fuel constraints.
   - Technology and mission progression hooks.

## Farsi / RTL UI
`PersianLocalizationService` includes Persian mission + tooltip keys and right-aligned rendering setup.

## Mod Security & Stability Notes
- Invalid mod files are isolated and logged in `Application.persistentDataPath/mod_errors.log`.
- Reloads are hot and do not require game restart.
- JSON manifests are validated (`id` required, script extension limited to `.lua`/`.cs`).

## How to Use in Unity
1. Create a bootstrap `GameObject`.
2. Attach all `Manager` MonoBehaviours to it.
3. Wire dependencies in the inspector (streamer, HUD, localization, etc.).
4. Add world/vehicle prefabs and connect telemetry + tools.
5. Build Windows standalone.
