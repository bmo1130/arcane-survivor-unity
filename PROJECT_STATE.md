# Arcane Survivor Unity — Project State

## Current Phase
**U1-A2 — Player Movement Plane Correction (Editor Verification Pending)**

Unity 2D URP 프로젝트 생성과 기본 Repository 구성이 완료됐다.

U1-A의 WASD Input System 연결과 입력 동작은 Unity Editor에서 확인됐다.

기존 Unity 구현이 XY 평면을 사용해 Three.js Reference Prototype의 XZ 이동과 달랐으므로 이동 평면을 수정했다.

Unity Editor에서 XZ 이동과 Y 고정을 확인하기 전까지 U1-A와 U1 전체는 완료로 기록하지 않는다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U1-A2 — Player Movement Plane Correction**이다.

## Completed Features

### U0 — Project Setup
- Unity `6000.3.21f1` 2D URP 프로젝트 생성
- Git Repository 및 `origin/main` 연결
- Unity 생성 파일을 제외하는 `.gitignore` 적용
- 최소 프로젝트 구조 확인
  - `Assets/Scenes`
  - `Assets/Settings`
  - `Packages`
  - `ProjectSettings`
- `Assets/Scenes/Main.unity` 생성 및 저장
- Build Scene List에 `Main.unity` 등록
- 기존 `SampleScene` 제거
- 프로젝트 Root에 개발 문서 배치
  - `AGENTS.md`
  - `PROJECT_STATE.md`
  - `GAME_SPEC.md`
  - `MIGRATION_NOTES.md`
- Codex 프로젝트 접근 및 실제 파일 구조 확인

## Current Work

### U1-A2 — Player Movement Plane Correction
- U1-A에서 기존 `InputSystem_Actions`의 `Player/Move` 액션과 WASD 입력 동작 확인
- `Player/Move`의 `Vector2` 입력을 World XZ 이동으로 변환
  - Input X → World X
  - Input Y → World Z
  - World Y 유지
- `Vector2` 입력을 최대 길이 1로 제한해 대각선 속도 증가 방지 유지
- Inspector에서 조절 가능한 Move Speed 기본값을 Reference Prototype과 같은 `7`로 변경
- `Time.deltaTime` 기반 Transform 이동 유지
- Rigidbody2D와 Collider는 현재 충돌 Gameplay가 없어 사용하지 않음

남은 확인:
- Unity Editor Script Import 및 C# Compile
- 기존 Scene의 `player` Move Speed를 `7`로 변경
- Play Mode에서 W/S가 Z축, A/D가 X축을 변경하는지 확인
- 이동 중 Player Y 좌표가 유지되는지 확인

## Previous Prototype
기존 Three.js Prototype은 다음까지 구현되었다.

- WASD Player Movement
- Slime
- Enemy Spawn / Chase / Attack
- Magic Missile
- XP Orb
- Experience / Level
- Level Up Pause / 3-choice Upgrade
- Active 2 + Passive 1
- Common Upgrade 3종
- Arcane / Fire / Lightning / Frost School 전체
- 네 School의 2/4/6 Synergy
- Starting Spell Selection
- 8 Active 중 하나를 Lv.1로 선택해 시작

Three.js Prototype 마지막 Gameplay Phase: **Phase 7C**

## Unity Migration Goal
Three.js 구현 코드를 그대로 번역하지 않는다.

보존 대상:
- Game Rule
- Skill Rule
- Build Structure
- Combat Behavior
- Player Experience

Unity Engine이 제공하는 기능이 적절하다면 활용한다.

## Planned Unity Phase

### U0 — Project Setup
- Unity Project 생성
- Git Repository 생성
- `.gitignore`
- 기본 Folder 구조
- Main Scene
- 문서 배치
- Codex 프로젝트 접근 확인

### U1-A — Basic Player Movement
- Player GameObject
- SpriteRenderer
- Input System WASD 연결

### U1-A2 — Player Movement Plane Correction
- XZ Movement
- Move Speed 7
- Y Position 유지

### U1-B — Perspective Camera + Follow
- Perspective Camera
- Player Follow

### U2 — Slime
- Slime Prefab
- Spawn
- Chase
- Attack
- HP
- Death

### U3 — Enemy Spawning
- Spawn Timer
- Spawn Distance
- Enemy Count 제한

### U4 — First Combat
- Magic Missile
- Nearest Enemy Targeting
- Projectile
- Damage
- Enemy Death

### U5 — Experience
- XP Orb
- Pickup
- Player Experience
- Level

### U6 — Level Up
- Pause
- Upgrade 3-choice
- Upgrade Apply
- Resume

### U7 — Skill Loadout
- Active Slot 2
- Passive Slot 1
- Skill Lv.1 / Lv.2
- Eligibility

### U8 — Skill Data / School
- Arcane
- Fire
- Lightning
- Frost
- School Point 계산

### U9 — Starting Spell Selection
- Empty Loadout Start
- 8 Active Choice
- Lv.1 Active Slot 1

### U10 — Complete School Skills
- Arcane
- Fire
- Lightning
- Frost

### U11 — Synergy
- 각 School 2/4/6

### U12 — Prototype Demo Pass
- 최소 Visual
- Effect
- UI 정리
- Playtest

## Important Decisions

### Unity Project Setup
- 현재 Unity 버전은 `6000.3.21f1`이다.
- 2D URP 구성을 사용한다.
- `Assets/Scenes/Main.unity`를 Main Scene으로 사용한다.
- Build Scene List에는 `Main.unity` 하나만 활성화되어 있다.
- 현재 Phase에 필요하지 않은 빈 폴더나 시스템은 미리 만들지 않는다.

### Player Movement
- 기존 `Assets/InputSystem_Actions.inputactions`의 `Player/Move` 액션을 사용한다.
- 이동 Script는 `InputActionReference`를 통해 해당 액션을 직접 참조한다.
- 이동은 현재 충돌이 필요하지 않으므로 Rigidbody2D 없이 Transform에 적용한다.
- Three.js Reference Prototype과 같이 이동 평면은 XZ를 사용한다.
- Input X는 World X, Input Y는 World Z로 변환하고 World Y는 변경하지 않는다.
- W는 +Z, S는 -Z, A는 -X, D는 +X 방향이다.
- 기본 Move Speed는 Reference Prototype과 같은 `7`이다.
- 대각선 입력은 최대 길이 1로 제한하고 이동량에는 `Time.deltaTime`을 적용한다.
- Player Rotation은 현재 변경하지 않는다.
- Perspective Camera와 Follow는 U1-B로 분리한다.

### Git Tracking
Repository에 포함할 대상:
- `Assets`와 대응하는 `.meta` 파일
- `Packages/manifest.json`
- `Packages/packages-lock.json`
- `ProjectSettings`
- 프로젝트 문서

`ProjectSettings/SceneTemplateSettings.json`은 Unity 프로젝트의 공유 설정이므로 Git 포함 대상이다.

Repository에서 무시할 대상:
- `Library`
- `Temp`
- `Obj`
- `Logs`
- `UserSettings`
- IDE 생성 Solution / Project 파일

### Starting Spell
Magic Missile 강제 시작은 폐기됐다.

현재 규칙:
Active 1 Empty / Active 2 Empty / Passive Empty
→ 8 Active 중 하나 선택
→ 선택한 Spell Lv.1을 Active 1에 장착

### School Point
별도 누적 상태보다 현재 Skill Loadout의 Level 합으로 계산하는 것을 우선한다.

### Balance
Prototype 구현 중 기존 Balance 수치를 이유 없이 재조정하지 않는다.

본격 Balance Pass는 기능 구현과 재미 검증 이후 진행한다.

### Optimization
실제 Performance 문제가 확인되기 전에는 ECS, Object Pooling, Spatial Partition, 대규모 Custom Framework를 선행 구현하지 않는다.

Unity 자체 기능은 필요에 따라 사용할 수 있으나 미래 문제를 예상해 과도한 구조를 만들지 않는다.

## Current Scene / Prefab / Script Structure

### Scene
- Main Scene: `Assets/Scenes/Main.unity`
- Build Scene List: `Assets/Scenes/Main.unity`, enabled
- Root GameObjects:
  - `Main Camera`
  - `Global Light 2D`
  - `player`
- `player`는 테스트용 Square SpriteRenderer와 `PlayerMovement`를 사용한다.
- `player`의 `Player/Move` Input Action Reference는 연결되어 있다.
- Scene에 직렬화된 Move Speed는 현재 `5`이며 U1-A2 Editor 검증 전에 `7`로 변경해야 한다.
- `SampleScene`은 제거되었고 Build Scene List와 마지막 활성 Scene 기록에서 참조하지 않는다.
- `ProjectSettings.asset`의 `templateDefaultScene`에는 프로젝트 템플릿 출처 정보로 기존 `SampleScene` 경로가 남아 있다. Build Scene 항목은 아니며 U1 진행을 막지 않는다.
- `Assets/Settings/Scenes/URP2DSceneTemplate.unity`는 Unity 2D URP Scene Template이다.

### Prefab
- 없음

### Script
- `Assets/Scripts/Player/PlayerMovement.cs`
  - 기존 `Player/Move` Input Action을 읽어 XZ Transform 이동
  - World Y 좌표 유지
  - 기본 Move Speed `7`
  - 대각선 속도 제한과 Null 방어 유지
  - Editor Compile 및 Play Mode 검증 대기
- Assembly Definition 없음

## Known Issues
- U1-A의 WASD 입력 자체는 Unity Editor에서 확인됐지만 이동 평면이 잘못되어 최종 완료로 처리하지 않았다.
- U1-A2의 XZ 이동과 Y 좌표 유지는 Unity Editor에서 아직 확인되지 않았다.
- 기존 `player` Component의 직렬화된 Move Speed가 `5`이므로 Inspector에서 `7`로 변경해야 한다.
- 현재 2D Square와 기존 Camera는 XZ 이동을 최종 시각 방식으로 보여주기 위한 구성이 아니다. Visual과 Camera는 U1-B에서 처리한다.
- `ProjectSettings.asset`의 프로젝트 템플릿 메타데이터에는 `templateDefaultScene: Assets/Scenes/SampleScene.unity`가 남아 있다. 실제 Build Scene과 활성 Scene은 모두 `Main.unity`를 사용한다.
- Git commit / push는 현재 작업 범위에서 의도적으로 수행하지 않았다.

## Deferred Work
- Perspective Camera와 Player Follow
- Physics2D 활용 범위
- ScriptableObject 활용 범위
- Common Upgrade 최대 Level
- Ice Bolt Lv.2 Main Target 중복 Damage 유지 여부
- Lightning Stagger anti-permastun 필요 여부
- 최종 Balance
- U2 이후 Enemy, Combat, Experience, Skill, Synergy 구현

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**U1-B — Perspective Camera + Follow**

U1-A2의 Unity Editor Compile과 XZ 이동 검증이 완료된 뒤 진행한다.

U1-B에서는 Perspective Camera 구성과 Player Follow만 구현한다.

Enemy, Combat, Spell 등 U2 이후 시스템은 함께 구현하지 않는다.
