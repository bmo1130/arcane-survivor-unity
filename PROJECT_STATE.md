# Arcane Survivor Unity — Project State

## Current Phase
**U1-B — Perspective Camera + Follow (Editor Verification Pending)**

Unity 2D URP 프로젝트 생성과 기본 Repository 구성이 완료됐다.

U1-A와 U1-A2의 Player 이동은 Unity Editor에서 확인됐다.

W/S는 Z축, A/D는 X축으로 이동하며 Player Y는 `0`으로 유지된다.

Unity Console Error 없이 WASD 이동 검증을 완료했다.

현재 Perspective Camera와 부드러운 Player Follow를 구현 중이며, Unity Editor 검증 전까지 U1-B와 U1 전체는 완료로 기록하지 않는다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U1-B — Perspective Camera + Follow**다.

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
- W/S → World Z, A/D → World X 이동
- Player World Y `0` 유지
- 대각선 이동 속도 증가 방지
- `Time.deltaTime` 기반 이동
- Unity Editor Play Mode 및 Console Error 검증 완료

## Current Work

### U1-B — Perspective Camera + Follow
- `Assets/Scripts/Camera/CameraFollow.cs` 작성
- `LateUpdate`에서 Player 이동 후 Camera 갱신
- Player 위치와 Offset으로 목표 Camera 위치 계산
- `1 - exp(-sharpness * deltaTime)` 지수 보간으로 framerate-independent Follow
- Player 바닥 기준 높이 `0.5` 지점을 매 Frame 바라봄
- Follow Target 미연결 시 NullReference 없이 갱신 중단
- Reference Prototype 기본값 사용
  - Offset `(0, 14, 12)`
  - Follow Sharpness `7`
  - Look At Height `0.5`

남은 확인:
- Unity Editor Script Import 및 C# Compile
- Main Camera를 Perspective, FOV `50`으로 설정
- Main Camera에 `CameraFollow` 연결
- Play Mode Follow 감각과 Console Error 확인

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

### Camera
- Main Camera는 Perspective Projection과 FOV `50`을 사용한다.
- Player 기준 기본 Offset은 `(0, 14, 12)`다.
- 기본 Follow Sharpness는 `7`이다.
- `LateUpdate`에서 Player 이동 이후 Camera를 갱신한다.
- 위치 보간 계수는 `1 - exp(-sharpness * deltaTime)`을 사용한다.
- 매 Frame Player 위치의 Y `+0.5` 지점을 바라본다.
- Camera Shake, Dead Zone, Zoom, Boundary, Cinemachine은 현재 구현하지 않는다.

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
- U1-A / U1-A2 이동 동작은 Unity Editor에서 검증됐다.
- Scene에 직렬화된 Move Speed는 현재 `5`이며 Script 기본값 `7`과 다르다.
- Main Camera는 현재 Orthographic이며 Transform은 Position `(0, 0, -10)`, Rotation `(0, 0, 0)`이다.
- Main Camera의 Perspective/FOV/CameraFollow Editor 설정은 아직 적용되지 않았다.
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
  - Editor Compile 및 Play Mode 검증 완료
- `Assets/Scripts/Camera/CameraFollow.cs`
  - `LateUpdate` 기반 부드러운 Position Follow
  - 매 Frame Player LookAt
  - 기본 Offset `(0, 14, 12)`
  - 기본 Follow Sharpness `7`
  - 기본 Look At Height `0.5`
  - Editor 연결 및 Play Mode 검증 대기
- Assembly Definition 없음

## Known Issues
- U1-B는 Unity Editor에서 아직 컴파일 및 Play Mode 검증되지 않았다.
- Main Camera는 아직 Orthographic이며 `CameraFollow`가 연결되지 않았다.
- 현재 Scene의 `player` Move Speed는 직렬화된 값 `5`로, Reference Prototype 기준 `7`과 다르다.
- Perspective Camera에서 현재 Square Sprite가 어떻게 보이는지는 U1-B Editor 검증 후 판단한다.
- `ProjectSettings.asset`의 프로젝트 템플릿 메타데이터에는 `templateDefaultScene: Assets/Scenes/SampleScene.unity`가 남아 있다. 실제 Build Scene과 활성 Scene은 모두 `Main.unity`를 사용한다.
- Git commit / push는 현재 작업 범위에서 의도적으로 수행하지 않았다.

## Deferred Work
- Physics2D 활용 범위
- ScriptableObject 활용 범위
- Common Upgrade 최대 Level
- Ice Bolt Lv.2 Main Target 중복 Damage 유지 여부
- Lightning Stagger anti-permastun 필요 여부
- 최종 Balance
- U2 이후 Enemy, Combat, Experience, Skill, Synergy 구현

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**Pending U1-B Editor Verification**

먼저 U1-B의 Unity Editor 설정과 Play Mode 검증을 완료한다.

검증 결과를 확인한 뒤 다음 작은 Phase를 결정한다. U2는 아직 시작하지 않는다.
