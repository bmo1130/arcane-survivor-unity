# Arcane Survivor Unity — Project State

## Current Phase
**U4-A — Magic Missile Basic Auto Combat / Editor Verification Pending**

Unity 2D URP 프로젝트 생성과 기본 Repository 구성이 완료됐다.

U1 Player Movement와 Perspective Camera Follow는 Unity Editor에서 확인됐다.

Camera Relative Movement가 Unity Editor에서 검증됐다. W/S/A/D는 화면 기준 상/하/좌/우로 이동하며 Player는 XZ 평면에서 움직이고 Y는 `0`으로 유지된다.

Perspective Camera는 FOV `50`으로 Player를 부드럽게 추적하며 Unity Console Error가 없다.

Perspective Camera Follow, Player/Slime Billboard, Ground도 정상이며 U1-D와 U1 전체 검증을 완료했다.

Main Scene의 Slime 한 개가 Player를 추적하고 Stop Distance 안에서 Cooldown마다 Damage를 적용하는 동작을 검증했다.

Player HP는 `100`에서 Damage `8`씩 약 `1.2`초 간격으로 감소하며 Unity Console Error가 없다.

Slime Maximum Health `10`, Debug Damage `3`, HP 감소와 네 번째 피격 시 Root/Visual Destroy를 Unity Editor에서 검증했다.

죽은 Slime의 Attack 중단과 기존 Player Movement, Camera Follow, Billboard Regression도 확인했으며 Console Error가 없다. U2 전체는 완료됐다.

Slime 자동 Spawn, `1.5`초 간격, Player 기준 거리 `14`, 여러 방향 Spawn과 Maximum Enemy Count `20`을 Unity Editor에서 검증했다.

Runtime Target/PlayerHealth/Billboard Camera 연결, Slime Chase/Attack, Destroy 이후 Count 회수와 재Spawn도 정상이며 Console Error가 없다. U3-A는 완료됐다.

최대 20 Slime에서 한 점 겹침 완화, 자연스러운 무리 형태, 심각한 떨림/튕김 없음과 기존 Chase/Attack/HP/Death/Re-Spawn을 Unity Editor에서 검증했다.

U3-B와 U3 전체는 완료됐다.

현재 사용자가 Player에 Test Caster를 명시적으로 추가한 경우에만 Magic Missile Lv.1 자동 전투를 검증하는 U4-A를 진행 중이다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U4-A — Magic Missile Basic Auto Combat**이다.

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

### U2-C — Slime HP / Take Damage / Death
- Slime Maximum Health `10`, Debug Damage `3`
- Current Health `10 → 7 → 4 → 1` 감소 확인
- 네 번째 Debug Damage에서 Slime Root와 Visual Destroy
- Death 이후 Player Attack 중단
- Player Movement, Camera Follow, Billboard Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

### U2 — Slime
- U2-A Chase, U2-B Attack + Player HP, U2-C Slime HP + Death 완료
- 수동 배치 Slime 한 마리의 전체 기본 동작 검증 완료

### U3-A — Slime Prefab + Enemy Spawning
- Slime Prefab 자동 Spawn
- Spawn Interval `1.5`, Spawn Distance `14`, Maximum Enemy Count `20`
- 여러 방향의 XZ Spawn과 Slime Y `0`
- Runtime Target, PlayerHealth, Billboard Camera 연결
- Spawn된 Slime Chase/Attack 정상
- Destroy 이후 Count 회수와 재Spawn 정상
- Unity Editor Play Mode 및 Console Error 검증 완료

### U3-B — Simple Enemy Separation
- 최대 20 Slime의 완전한 한 점 겹침 완화
- 자연스러운 Enemy 무리 형태 유지
- 심각한 떨림과 튕김 없음
- Chase, Attack, HP, Death, Destroy 후 Re-Spawn Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

### U3 — Enemy Spawning / Separation
- U3-A Slime Prefab + Enemy Spawning 완료
- U3-B Simple Enemy Separation 완료

## Current Work

### U4-A — Magic Missile Basic Auto Combat
- Player에 수동으로 추가하는 Test-only `MagicMissileCaster`
- 기존 EnemySpawner Spawned Slime 목록에서 XZ 기준 가장 가까운 살아 있는 Slime 선택
- Damage `3`, Cooldown `0.65`, Speed `6`, Lifetime `5`, Collision Radius `0.22`
- Player Y `+1.25` 위치에서 Projectile 생성
- Target을 매 Frame 추적하는 XZ Homing Projectile
- Target 사망 또는 Lifetime 만료 시 Projectile 제거
- Target과 XZ 거리 기반 명중, `SlimeController.TakeDamage(3)` 후 Projectile 제거
- Rigidbody, Collider, Physics, FindObjectsByType, Combat Manager를 사용하지 않음
- Magic Missile은 전투 검증을 위해 수동 활성화하는 Test Caster일 뿐 강제 Starting Skill이 아님

남은 확인:
- Unity Editor Script Import 및 C# Compile
- `Assets/Prefabs/Projectiles/MagicMissile.prefab` Editor 생성
- Player Root에 Test-only `MagicMissileCaster` 수동 추가 및 Reference 연결
- Target 없음/최근접 Target/Cooldown/Homing/명중/Lifetime/Target 선행 사망 확인
- Damage `3`씩 네 번 명중 시 Slime Death 확인
- Spawn/Separation/Chase/Attack/Player/Camera Regression 확인

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

### U2 — Slime
- Chase
- Attack + Player HP
- Slime HP + Death

### U3-A — Slime Prefab + Enemy Spawning
- Slime Prefab
- Spawn Interval `1.5`
- Spawn Distance `14`
- Maximum Enemy Count `20`
- Runtime Player / PlayerHealth / Billboard Camera 연결

### U3-B — Simple Enemy Separation
- Spawned Slime 목록 재사용
- XZ Separation Radius / Strength
- 동일 위치 fallback
- 기존 Move Speed와 Stop Distance 보존

### U4-A — Magic Missile Basic Auto Combat
- Test-only Magic Missile Caster
- Nearest Alive Slime Targeting
- Homing Projectile
- Damage / Lifetime / Collision Radius
- Existing Slime Death 연결

### U4 — Remaining First Combat
- Starting Spell / Loadout 연동은 후속 Phase에서 처리
- Magic Missile Lv.2와 다른 Spell은 후속 Phase에서 처리

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

### Enemy Spawning
- `EnemySpawner`가 첫 Spawn과 이후 Spawn을 기본 `1.5`초 간격으로 처리한다.
- Spawn 위치는 Player의 X/Z와 임의 각도를 사용하며 반지름은 `14`, World Y는 항상 `0`이다.
- Spawner가 생성한 살아 있는 Slime은 최대 `20`마리로 제한한다.
- Destroy된 Slime은 Unity Object의 Null 상태를 목록에서 제거해 Count를 회수한다.
- Spawn 직후 `SlimeController.Setup`으로 Player Transform과 PlayerHealth를 명시적으로 연결한다.
- Spawn 직후 Slime 하위의 `BillboardToCamera`에 Inspector에서 받은 Main Camera Transform을 연결한다.
- Runtime 연결을 위해 Scene 전체 검색, Enemy Manager Framework, Object Pool, Service Locator를 사용하지 않는다.
- Difficulty Scaling, Wave, Spawn Interval 감소는 현재 구현하지 않는다.

### Simple Enemy Separation
- 현재 Maximum Enemy Count `20`에서는 각 Slime이 Spawned Slime 목록을 직접 순회하는 O(n²) 계산을 사용한다.
- 기본 Separation Radius는 `0.75`, Separation Strength는 `0.35`다.
- Separation은 Chase를 대체하지 않고 XZ 평면의 작은 보정 이동으로 추가한다.
- 이웃별 보정은 거리 비율로 약화하고 평균낸 뒤 최대 길이 `1`로 제한한다.
- Chase와 Separation의 최종 Frame 이동량은 기존 Move Speed `2.6`을 넘지 않는다.
- Stop Distance 안쪽을 향하는 이동 성분은 제거해 기존 접근 경계를 보존한다.
- 두 Slime의 X/Z가 완전히 같으면 Instance ID 기반 고정 방향을 사용해 NaN 없이 겹침에서 빠져나온다.
- Rigidbody, Collider, Spatial Hash, Quadtree, Enemy Manager Framework는 사용하지 않는다.

### Magic Missile Test Combat
- **Magic Missile is enabled manually for combat verification only and is NOT a forced starting skill.**
- Magic Missile은 U4-A 전투 검증을 위해 사용자가 Player에 `MagicMissileCaster`를 수동 추가할 때만 활성화된다.
- 이 Test Caster는 Starting Skill System이나 실제 Loadout 획득을 의미하지 않는다.
- 게임 시작 규칙은 Active 1 Empty / Active 2 Empty / Passive Empty이며 Magic Missile을 강제로 지급하지 않는다.
- 기존 EnemySpawner 목록에서 Player와 XZ 기준 가장 가까운 살아 있는 Slime 하나를 선택한다.
- Target이 없으면 발사하지 않고 Cooldown 준비 상태를 유지한다.
- Reference Prototype과 같이 Projectile은 살아 있는 Target을 매 Frame 추적하는 Homing 방식이다.
- Target이 먼저 죽으면 Retarget하지 않고 Projectile을 제거한다.
- 기본값은 Damage `3`, Cooldown `0.65`, Speed `6`, Lifetime `5`, Collision Radius `0.22`다.
- Slime HP `10`에는 네 번 명중해야 Death가 발생한다.
- Projectile은 Player Y `+1.25`에서 생성되고 XZ 평면으로 이동하며 시각 높이를 유지한다.
- Rigidbody, Collider, Physics, FindObjectsByType, 범용 Combat/Projectile Framework를 사용하지 않는다.

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

U4-A의 `MagicMissileCaster`는 Editor 전투 검증을 위한 수동 Test Component이며 이 규칙을 변경하지 않는다.

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
  - `ground`
  - `EnemySoawner`
- `player` Root는 `PlayerMovement`와 `PlayerHealth`를 사용하며 Position `(0, 0, 0)`, Rotation `(0, 0, 0)`이다.
- `player/Visual` Child는 기존 Square SpriteRenderer와 `BillboardToCamera`를 사용하며 Local Position은 `(0, 0.5, 0)`이다.
- `player`의 `Player/Move` Input Action Reference는 연결되어 있다.
- U1-A / U1-A2의 World XZ 이동 동작은 Unity Editor에서 검증됐다.
- Scene에 직렬화된 Player Move Speed는 `7`이다.
- U1-D Camera-relative movement는 Unity Editor에서 검증 완료됐다.
- PlayerMovement의 Movement Camera는 `Main Camera`에 연결되어 있다.
- Main Camera는 Perspective, FOV `50`이다.
- Main Camera에는 `CameraFollow`가 연결되어 있다.
  - Follow Target: `player`
  - Offset `(0, 14, 12)`
  - Follow Sharpness `7`
  - Look At Height `0.5`
- U1 Camera Follow는 Unity Editor에서 검증됐다.
- `player`에는 `PlayerHealth`가 연결되어 있으며 Maximum HP는 `100`이다.
- `player`에는 아직 `MagicMissileCaster`가 없으며 U4-A Editor Test Setup에서 사용자가 명시적으로 추가해야 한다.
- 수동 배치 Slime은 Scene에서 제거됐다.
- `EnemySoawner`에는 `EnemySpawner`가 연결되어 있다.
  - Player: `player`
  - Slime Prefab: 실제 Slime Prefab Asset
  - Billboard Camera: `Main Camera`
  - Spawn Interval `1.5`
  - Spawn Distance `14`
  - Maximum Enemy Count `20`
- U3-A Spawn/Runtime Reference/Count 회수와 U3-B Separation/Gameplay Regression은 Unity Editor에서 검증됐다.
- `ground`는 Unity 기본 Square Sprite를 사용하며 Position `(0, -0.05, 0)`, Rotation `(90, 0, 0)`, Scale `(80, 80, 1)`이다.
- `SampleScene`은 제거되었고 Build Scene List와 마지막 활성 Scene 기록에서 참조하지 않는다.
- `ProjectSettings.asset`의 `templateDefaultScene`에는 프로젝트 템플릿 출처 정보로 기존 `SampleScene` 경로가 남아 있다. Build Scene 항목은 아니며 U1 진행을 막지 않는다.
- `Assets/Settings/Scenes/URP2DSceneTemplate.unity`는 Unity 2D URP Scene Template이다.

### Prefab
- Slime 실제 경로: `Assets/Prefabs/Enemies/Slime.prefab`
- Root `Slime`
  - `SlimeController`
  - `Visual`
    - `SpriteRenderer`
    - `BillboardToCamera`
- Prefab의 Target, PlayerHealth, Billboard Camera는 Runtime에 연결된다.
- U3-B Separation은 Script 기본값 Radius `0.75`, Strength `0.35`로 Editor 검증 완료됐다.
- `Assets/Prefabs/Projectiles/MagicMissile.prefab`은 아직 없으며 U4-A Editor Setup에서 생성해야 한다.

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
  - Runtime `Setup(Transform, PlayerHealth, IReadOnlyList<SlimeController>)`으로 Spawn Reference와 이웃 목록 연결
  - Separation Radius `0.75`, Strength `0.35`
  - XZ 이웃 거리 기반 Separation과 동일 위치 fallback
  - 최종 Move Speed/Stop Distance/Y 보존
  - 외부 Targeting용 `IsAlive` 읽기 전용 상태
  - U2-C Editor 검증 완료
- `Assets/Scripts/Enemies/EnemySpawner.cs`
  - 첫 Spawn과 이후 Spawn Interval 기본값 `1.5`
  - Player 주변 XZ 반지름 `14`, World Y `0` Spawn
  - Maximum Enemy Count 기본값 `20`
  - Destroy된 Slime 목록 정리 및 Count 회수
  - Spawn 시 Player/PlayerHealth/Billboard Camera Runtime 연결
  - 기존 Spawned Slime 목록을 각 Slime의 Separation 이웃 목록으로 전달
  - Test Combat Targeting용 `SpawnedEnemies` 읽기 전용 목록과 Billboard Camera 제공
  - 필수 Reference Null 방어
  - U3-A Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Combat/MagicMissileCaster.cs`
  - Player에 사용자가 수동 추가하는 Test-only Component
  - EnemySpawner 목록에서 XZ 최근접 살아 있는 Slime 선택
  - 기본 Damage `3`, Cooldown `0.65`, Speed `6`, Lifetime `5`, Collision Radius `0.22`
  - Projectile 생성과 Runtime Target/Camera/수치 전달
  - Target 없음과 필수 Reference Null 방어
  - U4-A Editor 연결 및 Play Mode 검증 대기
- `Assets/Scripts/Combat/MagicMissileProjectile.cs`
  - 살아 있는 Target을 향한 XZ Homing 이동
  - Player Y `+1.25` 발사 높이 유지
  - XZ Collision Radius 명중 판정과 `SlimeController.TakeDamage`
  - 명중, Lifetime 만료, Target 선행 사망 시 Root Destroy
  - 하위 Billboard Camera Runtime 연결
  - U4-A Projectile Prefab 생성 및 Play Mode 검증 대기
- `Assets/Scripts/Player/PlayerHealth.cs`
  - Maximum HP 기본값 `100`
  - Current HP Inspector 확인 가능
  - `TakeDamage(float)`
  - 0 이하 Damage 무시 및 HP 0 Clamp
  - Player Death 없음
  - Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Visual/BillboardToCamera.cs`
  - Camera Transform 참조
  - Spawn된 Visual용 Runtime `SetCamera(Transform)` 연결 지점
  - `LateUpdate`에서 Visual Child만 Camera 방향으로 회전
  - Camera Null 방어
  - Player/Slime Visual Child와 Main Camera 연결 완료
  - U1-D 조작 수정 후 최종 Play Mode Regression 확인 완료
- Assembly Definition 없음

## Known Issues
- U4-A Magic Missile Caster/Projectile은 아직 Unity Editor Prefab/Scene 연결 및 Play Mode 검증 전이다.
- `Assets/Prefabs/Projectiles/MagicMissile.prefab`이 아직 없고 Player에도 Test Caster가 연결되지 않았다.
- Scene Spawner 이름이 `EnemySpawner`가 아니라 `EnemySoawner`로 저장되어 있다. 기능에는 영향이 없어 이번 Phase에서는 변경하지 않았다.
- Player Death와 HP UI는 구현되지 않았다.
- `ProjectSettings.asset`의 프로젝트 템플릿 메타데이터에는 `templateDefaultScene: Assets/Scenes/SampleScene.unity`가 남아 있다. 실제 Build Scene과 활성 Scene은 모두 `Main.unity`를 사용한다.
- Git commit / push는 현재 작업 범위에서 의도적으로 수행하지 않았다.

## Deferred Work
- Spawn Difficulty Scaling, Wave, Spawn Interval 감소
- Enemy Manager Framework와 Object Pool
- Physics2D 활용 범위
- ScriptableObject 활용 범위
- Common Upgrade 최대 Level
- Ice Bolt Lv.2 Main Target 중복 Damage 유지 여부
- Lightning Stagger anti-permastun 필요 여부
- 최종 Balance
- U4 이후 Experience, Skill, Synergy 구현

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**Pending U4-A Editor Verification**

먼저 Test Caster와 Projectile Prefab을 Unity Editor에서 명시적으로 연결하고 Targeting/Homing/Damage/Lifetime과 기존 Gameplay Regression을 검증한다.

검증 결과를 확인한 뒤 다음 작은 Phase를 제안한다. 이번 작업에서는 U5 XP, Starting Spell Selection, Loadout을 구현하지 않는다.
