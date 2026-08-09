# Arcane Survivor Unity — Project State

## Current Phase
**U2-C — Slime HP / Take Damage / Death / Editor Verification Pending**

Unity 2D URP 프로젝트 생성과 기본 Repository 구성이 완료됐다.

U1 Player Movement와 Perspective Camera Follow는 Unity Editor에서 확인됐다.

Camera Relative Movement가 Unity Editor에서 검증됐다. W/S/A/D는 화면 기준 상/하/좌/우로 이동하며 Player는 XZ 평면에서 움직이고 Y는 `0`으로 유지된다.

Perspective Camera는 FOV `50`으로 Player를 부드럽게 추적하며 Unity Console Error가 없다.

Perspective Camera Follow, Player/Slime Billboard, Ground도 정상이며 U1-D와 U1 전체 검증을 완료했다.

Main Scene의 Slime 한 개가 Player를 추적하고 Stop Distance 안에서 Cooldown마다 Damage를 적용하는 동작을 검증했다.

Player HP는 `100`에서 Damage `8`씩 약 `1.2`초 간격으로 감소하며 Unity Console Error가 없다.

U2-B는 완료됐지만 U2 전체는 아직 완료하지 않았다.

현재 Slime에 Maximum/Current Health, `TakeDamage`, Death와 임시 Debug Damage 수단을 추가하는 U2-C를 진행 중이다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U2-C — Slime HP / Take Damage / Death**다.

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

### U1-A / U1-A2 — Player Movement
- 기존 Input System의 `Player/Move` 액션으로 WASD 입력
- Transform 기반 XZ 평면 이동
- Player World Y `0` 유지
- 대각선 이동 속도 증가 방지
- `Time.deltaTime` 기반 이동
- Unity Editor Play Mode 및 Console Error 검증 완료

### U1-B — Perspective Camera + Follow
- Perspective Camera, FOV `50`
- Player 기준 Offset `(0, 14, 12)`
- Follow Sharpness `7`
- Look At Height `0.5`
- `LateUpdate` 기반 framerate-independent exponential smoothing
- Unity Editor Play Mode 및 Console Error 검증 완료

### U1 Core — Player
- U1-A, U1-A2, U1-B, U1-C, U1-D 완료
- Camera-relative WASD XZ 이동, Player Y 유지, Perspective Camera Follow 확인
- Player/Slime Billboard와 Ground 확인
- Unity Editor Play Mode 및 Console Error 검증 완료

### U2-A — Basic Slime Chase
- 수동 배치 Slime 한 개가 Player를 XZ 평면에서 추적
- Slime World Y 유지
- Move Speed `2.6`
- Stop Distance `1.15`에서 정지
- Player가 멀어지면 추적 재개
- Player Movement와 Camera Follow Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

### U2-B — Slime Attack + Player HP
- Player Maximum HP `100`, 시작 Current HP `100`
- Slime Damage `8`, Attack Cooldown `1.2`초
- Stop Distance `1.15`를 Attack Range로 재사용
- 범위 안에서 이동 정지 후 Cooldown마다 Damage 적용
- Current HP `0` Clamp 및 Player Death 미구현
- Player Movement, Camera Follow, Slime Chase Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

## Current Work

### U2-C — Slime HP / Take Damage / Death
- 기존 `SlimeController`에 Maximum/Current Health 추가
- Maximum Health 기본값 `10`, Play 시작 시 Current Health를 Maximum Health로 초기화
- `TakeDamage(float)`에서 NaN, Infinity, 0 이하 Damage 무시
- Current Health를 `0` 이상으로 Clamp
- HP `0` 도달 시 중복 Death를 방지하고 Slime Root GameObject 전체 Destroy
- Debug Damage 기본값 `3`
- Component Context Menu의 `Debug Take Damage`가 실제 `TakeDamage`를 호출
- Enemy Framework, Interface, Event System을 추가하지 않음

남은 확인:
- Unity Editor Script Import 및 C# Compile
- Play 시작 시 Current Health `10` 확인
- Context Menu Debug Damage로 `10 → 7 → 4 → 1 → 0` 확인
- HP `0`에서 Slime Root와 Visual Child 제거 및 Attack 중단 확인
- Chase/Attack, PlayerMovement, CameraFollow, Billboard/Ground Regression 확인

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

### U1-C — Prototype Visual Baseline
- Player / Slime Visual Child
- Camera-facing Billboard
- XZ Ground 기준

### U1-D — Camera Relative Movement
- Camera forward/right 기반 XZ Movement
- 화면 기준 WASD 방향
- Camera Reference Null 방어

### U2-A — Basic Slime Chase
- 수동 배치 Slime
- XZ Chase
- Stop Distance

### U2-B — Slime Attack + Player HP
- Player Maximum / Current HP
- Slime Damage
- Attack Cooldown
- Stop Distance 기반 Attack Range

### U2-C — Slime HP / Take Damage / Death
- Slime Maximum / Current Health
- Invalid Damage 방어와 HP Clamp
- Slime Root Destroy
- Context Menu Debug Damage

### U2 — Remaining Slime Features
- Slime Prefab
- U2 전체 Editor 검증 및 완료 판단

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
- 이동 방향은 Inspector에 연결한 Main Camera Transform을 기준으로 계산한다.
- Camera forward/right의 Y 성분을 제거하고 각각 XZ 평면에서 정규화한다.
- Input X는 Camera Right, Input Y는 Camera Forward에 적용해 W/S/A/D가 화면 기준 상/하/좌/우로 보이게 한다.
- Camera는 Inspector에서 한 번 연결하며 `Camera.main`을 매 Frame 검색하지 않는다.
- World Y는 변경하지 않는다.
- 기본 Move Speed는 Reference Prototype과 같은 `7`이다.
- 입력과 최종 이동 방향을 최대 길이 `1`로 제한하고 이동량에는 `Time.deltaTime`을 적용한다.
- Player Rotation은 현재 변경하지 않는다.
- Perspective Camera와 Follow는 U1-B로 분리한다.

### Camera
- Main Camera는 Perspective Projection과 FOV `50`을 사용한다.
- Player 기준 기본 Offset은 `(0, 14, 12)`다.
- 기본 Follow Sharpness는 `7`이다.
- `LateUpdate`에서 Player 이동 이후 Camera를 갱신한다.
- 위치 보간 계수는 `1 - exp(-sharpness * deltaTime)`을 사용한다.
- 매 Frame Player 위치의 Y `+0.5` 지점을 바라본다.
- Camera Shake, Dead Zone, Zoom, Boundary, Cinemachine은 현재 구현하지 않는다.

### Slime Chase
- Slime은 현재 Rigidbody, Collider, NavMesh 없이 Transform으로 이동한다.
- Target과의 거리는 XZ 평면에서만 계산하고 Slime Y는 변경하지 않는다.
- 기본 Move Speed는 `2.6`, Stop Distance는 `1.15`다.
- 한 Frame에 이동할 수 있는 거리를 `distance - stopDistance` 이하로 제한한다.
- Stop Distance에 도달하면 Player 중심까지 더 파고들지 않고 정지한다.
- Slime 회전과 Visual 방향 보정은 현재 구현하지 않는다.

### Player Health
- Player Maximum HP 기본값은 `100`이다.
- 게임 시작 시 Current HP는 Maximum HP로 초기화한다.
- HP는 향후 Regeneration과 Maximum HP Upgrade를 위해 `float`를 사용한다.
- 0 이하 Damage는 무시하고 Current HP는 `0` 아래로 내려가지 않는다.
- Player Death와 HP UI는 현재 구현하지 않는다.

### Slime Attack
- 기존 Stop Distance `1.15`를 Attack Range로 함께 사용한다.
- Attack Range 안에서는 이동을 멈추고 Cooldown 준비 시 PlayerHealth에 Damage를 적용한다.
- 기본 Damage는 `8`, Attack Cooldown은 `1.2`초다.
- Cooldown은 범위 안팎과 관계없이 `Time.deltaTime`으로 감소한다.
- Animation, Effect, Sound, 범용 Damage Interface는 현재 구현하지 않는다.

### Slime Health / Death
- Slime Health는 별도 Framework 없이 기존 `SlimeController`가 관리한다.
- Maximum Health 기본값은 `10`이며 Play 시작 시 Current Health를 Maximum Health로 초기화한다.
- `TakeDamage(float)`는 NaN, Infinity, 0 이하 값을 무시하고 Current Health를 `0` 이상으로 Clamp한다.
- Current Health가 `0`이면 중복 Death를 방지하고 Component를 비활성화한 뒤 Slime Root GameObject를 Destroy한다.
- Spell Combat 전 검증용 Debug Damage 기본값은 `3`이며 Context Menu도 실제 `TakeDamage`를 호출한다.
- Death Animation, Effect, XP Drop, 범용 Enemy Health 구조는 현재 구현하지 않는다.

### Prototype Visual Baseline
- Player와 Slime Gameplay Root는 Position과 Rotation을 담당하며 Rotation `(0, 0, 0)`을 유지한다.
- SpriteRenderer와 `BillboardToCamera`는 각 Root 아래의 `Visual` Child에 둔다.
- Billboard는 `LateUpdate`에서 Visual Child의 Rotation만 변경한다.
- Player는 기존 Square Sprite, Slime은 기존 Circle Sprite를 유지한다.
- Ground는 Unity 기본 Square Sprite를 XZ 평면에 눕혀 사용한다.
- Ground 크기는 Reference Prototype worldSize `80`을 참고하지만 Gameplay Boundary는 구현하지 않는다.
- 외부 Art, Animation, Custom Shader, 3D Model, Renderer Pipeline 변경은 하지 않는다.

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
  - `slime`
  - `ground`
- `player` Root는 `PlayerMovement`와 `PlayerHealth`를 사용하며 Position `(0, 0, 0)`, Rotation `(0, 0, 0)`이다.
- `player/Visual` Child는 기존 Square SpriteRenderer와 `BillboardToCamera`를 사용하며 Local Position은 `(0, 0.5, 0)`이다.
- `player`의 `Player/Move` Input Action Reference는 연결되어 있다.
- U1-A / U1-A2의 World XZ 이동 동작은 Unity Editor에서 검증됐다.
- Scene에 직렬화된 Player Move Speed는 `7`이다.
- U1-D Camera-relative movement는 Unity Editor에서 검증 완료됐다.
- 현재 디스크의 `Main.unity`에는 PlayerMovement의 `movementCamera` 직렬화 항목이 아직 기록되지 않았다. Editor에서 연결 상태를 확인하고 Scene을 저장해야 영구 보존된다.
- Main Camera는 Perspective, FOV `50`이다.
- Main Camera에는 `CameraFollow`가 연결되어 있다.
  - Follow Target: `player`
  - Offset `(0, 14, 12)`
  - Follow Sharpness `7`
  - Look At Height `0.5`
- U1 Camera Follow는 Unity Editor에서 검증됐다.
- `slime` Root는 `SlimeController`를 사용하며 Position `(4, 0, 4)`, Rotation `(0, 0, 0)`이다.
- `slime/Visual` Child는 기존 Circle SpriteRenderer와 `BillboardToCamera`를 사용하며 Local Position은 `(0, 0, 0)`이다.
- 현재 디스크의 `Main.unity`에는 `slime` Root에도 SpriteRenderer가 남아 있다. Editor에서 검증한 구조와 차이가 있다면 Scene의 미저장 상태를 확인해야 한다.
- `slime`의 Target은 `player`, Move Speed는 `2.6`, Stop Distance는 `1.15`다.
- U2-A Slime Chase는 Unity Editor에서 검증됐다.
- `player`에는 `PlayerHealth`가 연결되어 있으며 Maximum HP는 `100`이다.
- `slime`에는 PlayerHealth Reference, Damage `8`, Attack Cooldown `1.2`가 연결되어 있다.
- U2-B Slime Attack과 Player HP는 Unity Editor에서 검증됐다.
- Player와 Slime의 Billboard Camera Reference는 `Main Camera`에 연결되어 있다.
- `ground`는 Unity 기본 Square Sprite를 사용하며 Position `(0, -0.05, 0)`, Rotation `(90, 0, 0)`, Scale `(80, 80, 1)`이다.
- `SampleScene`은 제거되었고 Build Scene List와 마지막 활성 Scene 기록에서 참조하지 않는다.
- `ProjectSettings.asset`의 `templateDefaultScene`에는 프로젝트 템플릿 출처 정보로 기존 `SampleScene` 경로가 남아 있다. Build Scene 항목은 아니며 U1 진행을 막지 않는다.
- `Assets/Settings/Scenes/URP2DSceneTemplate.unity`는 Unity 2D URP Scene Template이다.

### Prefab
- 없음

### Script
- `Assets/Scripts/Player/PlayerMovement.cs`
  - 기존 `Player/Move` Input Action을 읽어 Camera 기준 XZ Transform 이동
  - Inspector Camera Transform 참조
  - Camera forward/right를 XZ 평면에 투영하고 정규화
  - Input X → Camera Right, Input Y → Camera Forward
  - World Y 좌표 유지
  - 기본 Move Speed `7`
  - 대각선 속도 제한과 Null 방어 유지
  - U1-D Editor Compile, Camera 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Camera/CameraFollow.cs`
  - `LateUpdate` 기반 부드러운 Position Follow
  - 매 Frame Player LookAt
  - 기본 Offset `(0, 14, 12)`
  - 기본 Follow Sharpness `7`
  - 기본 Look At Height `0.5`
  - Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Enemies/SlimeController.cs`
  - XZ 평면 Target 추적
  - 기본 Move Speed `2.6`
  - 기본 Stop Distance `1.15`
  - Stop Distance overshoot 방지
  - Target Null 방어
  - U2-A Chase Editor 검증 완료
  - PlayerHealth 참조와 Cooldown 공격 추가
  - 기본 Damage `8`
  - 기본 Attack Cooldown `1.2`
  - U2-B Editor 연결 및 Play Mode 검증 완료
  - Maximum Health 기본값 `10`, Current Health Play 시작 초기화
  - Invalid Damage 방어, HP 0 Clamp, 중복 Death 방어
  - HP 0에서 Slime Root GameObject Destroy
  - Debug Damage 기본값 `3`과 `Debug Take Damage` Context Menu
  - U2-C Editor 검증 대기
- `Assets/Scripts/Player/PlayerHealth.cs`
  - Maximum HP 기본값 `100`
  - Current HP Inspector 확인 가능
  - `TakeDamage(float)`
  - 0 이하 Damage 무시 및 HP 0 Clamp
  - Player Death 없음
  - Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Visual/BillboardToCamera.cs`
  - Camera Transform 참조
  - `LateUpdate`에서 Visual Child만 Camera 방향으로 회전
  - Camera Null 방어
  - Player/Slime Visual Child와 Main Camera 연결 완료
  - U1-D 조작 수정 후 최종 Play Mode Regression 확인 완료
- Assembly Definition 없음

## Known Issues
- U2-C Slime Health, Debug Damage, Death는 아직 Unity Editor 검증 전이다.
- 현재 디스크의 `Main.unity`에는 PlayerMovement `movementCamera` 참조가 직렬화되지 않았고 `slime` Root SpriteRenderer도 남아 있다. 사용자가 보고한 정상 Editor 상태가 미저장 상태라면 Scene 저장이 필요하다.
- Player Death와 HP UI는 구현되지 않았다.
- `ProjectSettings.asset`의 프로젝트 템플릿 메타데이터에는 `templateDefaultScene: Assets/Scenes/SampleScene.unity`가 남아 있다. 실제 Build Scene과 활성 Scene은 모두 `Main.unity`를 사용한다.
- Git commit / push는 현재 작업 범위에서 의도적으로 수행하지 않았다.

## Deferred Work
- Slime Prefab
- Enemy Spawn 및 Enemy Manager
- Physics2D 활용 범위
- ScriptableObject 활용 범위
- Common Upgrade 최대 Level
- Ice Bolt Lv.2 Main Target 중복 Damage 유지 여부
- Lightning Stagger anti-permastun 필요 여부
- 최종 Balance
- U2 이후 Enemy, Combat, Experience, Skill, Synergy 구현

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**Pending U2-C Editor Verification**

먼저 U2-C의 Current Health, Debug Damage, Root Destroy와 기존 Gameplay Regression을 Unity Editor에서 검증한다.

검증 결과로 U2 전체 완료 여부를 판단한 뒤 다음 작은 Phase를 제안한다. 이번 작업에서는 U3, Spell Combat, XP를 구현하지 않는다.
