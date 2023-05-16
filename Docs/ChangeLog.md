# Change Log

A bi-weekly log of project changes separated by two week sprints.

## Catalogue

| Date | Sprint |
| - | - |
| [4/27/2023 - 5/11/2023](#4272023---5112023) | 8 |
| [4/6/2023 - 4/20/2023](#462023---4202023) | 7 |
| [3/14/2023 - 4/6/2023](#3142023---462023) | 6 |
| [3/3/2023 - 3/14/2023](#332023---3142023) | 5 |
| [2/8/2023 3/2/2023](#2082023---3022023) | 4 |
| [1/17/2023 - 2/7/2023](#1172023---2072023) | 3 |
| [1/4/2023 - 1/16/2023](#1042023---1162023) | 2 |
| [12/6/2022 - 1/3/2023](#12062022---1032023) | 1 |

## 4/27/2023 - 5/11/2023 (Sprint #8)

- New Features:
  - Implemented item system [#171](https://github.com/TigardHighGDC/Augmentation/pull/171).
  - Added full room gameplay [#180](https://github.com/TigardHighGDC/Augmentation/pull/180).
  - Now maintains certain game actions across rooms [#180](https://github.com/TigardHighGDC/Augmentation/pull/180).
  - Created a NPC text box similar to animal crossing [#169](https://github.com/TigardHighGDC/Augmentation/pull/169).
  - Boss fight with bullet patterns implemented [#183](https://github.com/TigardHighGDC/Augmentation/pull/183).
- Enhancements:
  - Fully added our art into the game [#172](https://github.com/TigardHighGDC/Augmentation/pull/172).
  - Updated the level map icons with our own assets [#168](https://github.com/TigardHighGDC/Augmentation/pull/168).
  - Added bullet bounce weapon effect [#178](https://github.com/TigardHighGDC/Augmentation/pull/178).
  - Now loads scenes asynchronously [#181](https://github.com/TigardHighGDC/Augmentation/pull/181).
- Bug Fixes:
  - Fixed a problem with the weapon inventory not tracking the amount of ammo in each weapon after the weapon was dropped [#171](https://github.com/TigardHighGDC/Augmentation/pull/171).
  - Fixed a bug where the scene would not switch back to the main menu after player death [#187](https://github.com/TigardHighGDC/Augmentation/pull/187).

## 4/6/2023 - 4/20/2023 (Sprint #7)

- New Features:
  - Basic UI added [#138](https://github.com/TigardHighGDC/Augmentation/pull/138).
  - Item event manager added [#156](https://github.com/TigardHighGDC/Augmentation/pull/156).
- Enhancements:
  - Added audio to enemies [#145](https://github.com/TigardHighGDC/Augmentation/pull/145).
  - Updated enemy projectile sprites [#145](https://github.com/TigardHighGDC/Augmentation/pull/145).
  - Initial rooms created & added scene switching to the map [#157](https://github.com/TigardHighGDC/Augmentation/pull/157).
  - Moved gun object closer to player to allow for aiming at very close enemies [#167](https://github.com/TigardHighGDC/Augmentation/pull/167).
  - Updated Hackerman enemy volume [#158](https://github.com/TigardHighGDC/Augmentation/pull/158).
  - Added player animations [#165](https://github.com/TigardHighGDC/Augmentation/pull/165).
  - Added gun images [#165](https://github.com/TigardHighGDC/Augmentation/pull/165).
- Bug Fixes:
  - Fixed weapon inventory not properly tracking the amount of ammo in each weapon [#128](https://github.com/TigardHighGDC/Augmentation/pull/128).

## 3/14/2023 - 4/6/2023 (Sprint #6)

- New Features:
  - Added corruption display with new corruption eye UI [#121](https://github.com/TigardHighGDC/Augmentation/pull/121).
  - Implemented corruption stat changes [#125](https://github.com/TigardHighGDC/Augmentation/pull/125).
  - Implemented corruption auto changes [#125](https://github.com/TigardHighGDC/Augmentation/pull/125).
  - Added `Door.cs` for cross level movement [#123](https://github.com/TigardHighGDC/Augmentation/pull/123).
  - Added new gun sounds [#132](https://github.com/TigardHighGDC/Augmentation/pull/132).
  - Added music player that works with scene transitioning [#134](https://github.com/TigardHighGDC/Augmentation/pull/134).
  - Added URP Lighting [#136](https://github.com/TigardHighGDC/Augmentation/pull/136).
  - Added the procedurally generated level map [#141](https://github.com/TigardHighGDC/Augmentation/pull/141).
- Enhancements:
  - Basic variable renaming for improved readability [#120](https://github.com/TigardHighGDC/Augmentation/pull/120).
  - Bullets now have the ability to pierce enemies [#127](https://github.com/TigardHighGDC/Augmentation/pull/127).
  - Bullets now destroy themselves when they hit a wall [#130](https://github.com/TigardHighGDC/Augmentation/pull/130).
- Bug Fixes:
  - Changed `AudioManipulation.cs` to return a new audio file instead of modifying the original [#125](https://github.com/TigardHighGDC/Augmentation/pull/125).
  - Updated `Bug.prefab`, a fix from [#114](https://github.com/TigardHighGDC/Augmentation/pull/114) [#119](https://github.com/TigardHighGDC/Augmentation/pull/119).
  - `LaptopGoon.prefab` merge fix [#140](https://github.com/TigardHighGDC/Augmentation/pull/140).

## 3/3/2023 - 3/14/2023 (Sprint #5)

- New Features:
  - Updated Unity to version `2021.3.20f1` [#96](https://github.com/TigardHighGDC/Augmentation/pull/96).
  - Added health bar for both player health and corruption [#95](https://github.com/TigardHighGDC/Augmentation/pull/95).
  - Added new EY enemy type [#97](https://github.com/TigardHighGDC/Augmentation/pull/97).
  - Added new Hackerman enemy type [#99](https://github.com/TigardHighGDC/Augmentation/pull/99).
  - Added new Bug enemy type [#101](https://github.com/TigardHighGDC/Augmentation/pull/101).
  - Added a way to load scenes asynchronously [#77](https://github.com/TigardHighGDC/Augmentation/pull/77).
  - Added weapon inventory system [#107](https://github.com/TigardHighGDC/Augmentation/pull/107).
- Enhancements:
  - Updated player image [#100](https://github.com/TigardHighGDC/Augmentation/pull/100).
  - General script cleanups [#105](https://github.com/TigardHighGDC/Augmentation/pull/105).
  - Updated weapon sprites, sounds and values [#112](https://github.com/TigardHighGDC/Augmentation/pull/112).
  - Improved bug AI [#114](https://github.com/TigardHighGDC/Augmentation/pull/114).
- Documentation:
  - Added comments that were previously missing in `AIPhysics.cs` [#98](https://github.com/TigardHighGDC/Augmentation/pull/98).
  - Added previously missing copyright notices to top of scripts [#105](https://github.com/TigardHighGDC/Augmentation/pull/105).
- Bug Fixes:
  - Fixed the player prefab... Again... [#103](https://github.com/TigardHighGDC/Augmentation/pull/103).
  - Fixed a compiler error in the async scene loader [#104](https://github.com/TigardHighGDC/Augmentation/pull/104).

## 2/8/2023 - 3/2/2023 (Sprint #4)

- New Features:
  - Updated Unity to version `2021.3.18f1` [#88](https://github.com/TigardHighGDC/Augmentation/pull/88).
  - Added audio crusher to distort sounds [#69](https://github.com/TigardHighGDC/Augmentation/pull/88).
  - Added enemy knockback [#92](https://github.com/TigardHighGDC/Augmentation/pull/92).
- Enhancements:
  - Added reloading sound to guns [#89](https://github.com/TigardHighGDC/Augmentation/pull/88).
  - Upload of sounds & added volume control [#91](https://github.com/TigardHighGDC/Augmentation/pull/91).
- Bug Fixes:
  - Added a file to the `AStartPathfindingProject` submodule that removes a untracked commit message [#78](https://github.com/TigardHighGDC/Augmentation/pull/78).

## 1/17/2023 - 2/7/2023 (Sprint #3)

- New Features:
  - Basic usage of the A* Pathfinding project [#47](https://github.com/TigardHighGDC/Augmentation/pull/47).
  - Added `Circle Collider 2d` to player object [#51](https://github.com/TigardHighGDC/Augmentation/pull/51).
  - New breakable item type with random drops [#53](https://github.com/TigardHighGDC/Augmentation/pull/53).
  - New range based enemy type with dynamic pathing [#58](https://github.com/TigardHighGDC/Augmentation/pull/58).
  - Updated Unity to version `2021.3.17f1` [#62](https://github.com/TigardHighGDC/Augmentation/pull/34).
  - Player stat added that tracks level corruption level [#76](https://github.com/TigardHighGDC/Augmentation/pull/76).
- Enhancements:
  - Moved `Damage` for `Bullet.cs` to `WeaponData.cs` [#58](https://github.com/TigardHighGDC/Augmentation/pull/58).
  - Started Polymorphic structure of `NonPlayerHealth.cs` [#64](https://github.com/TigardHighGDC/Augmentation/pull/64).
- Bug Fixes:
  - Removed `EnemyHealth.cs` as it was renamed to `NonPlayerHealth.cs` [#64](https://github.com/TigardHighGDC/Augmentation/pull/64).
  - Fixed a branch merge issue where prefab data was lost in `LaptopGoon.prefab` [#68](https://github.com/TigardHighGDC/Augmentation/pull/64).

## 1/4/2023 - 1/16/2023 (Sprint #2)

- New Features:
  - Expanded Gun class with auto reload and bullet despawn times [#18](https://github.com/TigardHighGDC/Augmentation/pull/18).
  - Updated Unity to version `2021.3.16f1` [#34](https://github.com/TigardHighGDC/Augmentation/pull/34).
  - Linked the A* Pathfinding project to the repository [#41](https://github.com/TigardHighGDC/Augmentation/pull/41).
  - Created a basic template enemy class [#38](https://github.com/TigardHighGDC/Augmentation/pull/38).
  - Added the ability to play sound effects with guns [#44](https://github.com/TigardHighGDC/Augmentation/pull/44).
  - Added gun damage & tracking of entity health [#32](https://github.com/TigardHighGDC/Augmentation/pull/32).
- Documentation:
  - Fixed typos in `CONTRIBUTING.md` [#36](https://github.com/TigardHighGDC/Augmentation/pull/36).
  - Added game title to the `README.md` [#45](https://github.com/TigardHighGDC/Augmentation/pull/45).

## 12/6/2022 - 1/3/2023 (Sprint #1)

- New Features:
  - Issue Templates for GitHub [#11](https://github.com/TigardHighGDC/Augmentation/pull/11).
  - 2D Player Movement [#13](https://github.com/TigardHighGDC/Augmentation/pull/13).
  - Gun Template Class and Gun Data Container [#18](https://github.com/TigardHighGDC/Augmentation/pull/18).
- Documentation:
  - Started GitHub Pages Website [#20](https://github.com/TigardHighGDC/Augmentation/pull/20) & [#21](https://github.com/TigardHighGDC/Augmentation/pull/21).
- Bug Fixes:
  - `UserSettings/` `.gitignore` fix [#10](https://github.com/TigardHighGDC/Augmentation/pull/10).
