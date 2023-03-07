# Change Log

A bi-weekly log of project changes separated by two week sprints.

## Catalogue

| Date | Sprint |
| - | - |
| [3/3/2023 - 3/16/2023](#332023---3162023) | 5 |
| [2/8/2023 3/2/2023](#2082023---3022023) | 4 |
| [1/17/2023 - 2/7/2023](#1172023---2072023) | 3 |
| [1/4/2023 - 1/16/2023](#1042023---1162023) | 2 |
| [12/6/2022 - 1/3/2023](#12062022---1032023) | 1 |

## 3/3/2023 - 3/16/2023 (Sprint #5)

- New Features:
  - Updated Unity to version `2021.3.20f1` [#96](https://github.com/TigardHighGDC/Augmentation/pull/96).
  - Added health bar for both player health and corruption [#95](https://github.com/TigardHighGDC/Augmentation/pull/95).

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
