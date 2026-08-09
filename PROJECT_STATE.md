# Arcane Survivor Unity — Project State

## Current Phase
**U8-B — School Point Calculation / Editor Verification Pending**

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

Test-only Magic Missile의 자동 Target 선택, Homing, Damage, Target 선행 사망 처리와 Projectile Lifetime을 Unity Editor에서 검증했다. 기존 Spawn/Separation과 U1~U3 기능도 정상이며 Console Error가 없다.

Slime Death 시 Experience Orb 생성, Reward `4`, Pickup Radius `1.1`, 직접 Pickup과 `0 → 4 → 8` 누적을 Unity Editor에서 검증했다. Magnet과 중복 획득은 없으며 기존 Combat/Spawn/Movement도 정상이고 Console Error가 없다.

Level `1`, XP `0/8`, 두 Orb 후 Level `2`, XP `0/12`, Pending `1`, `Time.timeScale = 0`과 전체 Gameplay Pause를 Unity Editor에서 검증했다. Debug Pending 완료 후 `Time.timeScale = 1` Resume와 다음 Requirement `12`도 정상이며 Console Error가 없다.

Level Up 시 정확히 3개의 Button 표시, Pause 상태의 클릭, Pending 순차 감소, Multiple Pending 동안 Pause 유지와 마지막 Pending 이후 Resume를 Unity Editor에서 검증했다. Console Error가 없으며 U6-B는 완료됐다.

Maximum Health, Magic Power, Magic Missile Base Damage + Bonus, Regeneration, Pause 중 Regeneration 정지, Common Upgrade Level/UI Label 갱신과 Pending 순차 처리를 Unity Editor에서 검증했다. Console Error가 없으며 U6-C는 완료됐다.

Active 1/2와 Passive 1의 Empty 시작, 동일 Skill Lv.2 Cap, 새 Active/Passive Slot 제한, Debug Reset과 기존 Gameplay Regression을 Unity Editor에서 검증했다. Console Error가 없으며 U7-A는 완료됐다.

SkillCatalog의 실제 School Skill 12개, Active 8/Passive 4, School별 3개, Max Lv.2, Definition 기반 Loadout 획득과 기존 Slot 제한을 Unity Editor에서 검증했다. 시작 Loadout은 Empty이고 Common Upgrade와 Test Caster 분리도 유지되며 Console Error가 없다. U8-A는 완료됐다.

현재 School Point를 별도 상태 없이 장착된 Catalog Skill의 Current Level 합으로 실시간 계산하는 U8-B를 진행 중이다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U8-B — School Point Calculation**이다.

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

### U4-A — Magic Missile Basic Auto Combat
- Slime이 없을 때 발사하지 않음
- Spawn된 Slime 중 XZ 기준 최근접 Target 자동 선택
- Magic Missile 자동 발사와 Homing 정상
- Damage `3`, Slime HP `10 → 7 → 4 → 1 → Death` 확인
- Target 선행 사망과 Projectile Lifetime 처리 정상
- Enemy Spawn/Separation, Player Movement/Camera/PlayerHealth Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료
- Player의 `MagicMissileCaster`는 U4 전투 검증용 Test Caster이며 강제 Starting Skill이 아님

### U5-A — Experience Orb Drop + Direct Pickup
- Slime Death 시 Experience Orb 한 개 생성
- Experience Reward `4`, Pickup Radius `1.1`
- 자동 Magnet 없이 Player의 직접 Pickup 정상
- Current Experience `0 → 4 → 8` 누적 정상
- Orb 중복 획득 없음
- Combat, Spawn, Movement Regression 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

### U6-A — Level Progression + Level-Up Pause
- Level `1`, XP `0/8` 시작
- 첫 Orb 후 XP `4/8`, 두 번째 Orb 후 Level `2`, XP `0/12`
- Pending Level Ups `1`과 `Time.timeScale = 0` 확인
- Player, Enemy, Spawn, Projectile 등 Gameplay 전체 Pause 정상
- Debug Pending 완료 후 Pending `0`, `Time.timeScale = 1`, Gameplay Resume 정상
- 다음 Level Requirement `12` 확인
- Unity Editor Play Mode 및 Console Error 검증 완료

### U6-B — Level-Up 3-Choice UI Skeleton
- Level Up 시 `Time.timeScale = 0`과 LevelUpPanel 표시 정상
- 정확히 3개의 Button 표시 및 Pause 상태 클릭 정상
- Choice 선택 시 Pending Level Ups 하나 감소
- Multiple Pending에서 중간 Resume 없이 다음 Choice 표시
- 마지막 Pending 처리 후 Panel 숨김과 Gameplay Resume
- Unity Editor Play Mode 및 Console Error 검증 완료

### U6-C — Common Upgrade Choices + Application
- Maximum Health 선택 시 Maximum/Current HP 각각 `+10` 정상
- Magic Power 선택 시 Magic Damage Bonus `+1` 정상
- Test-only Magic Missile Base Damage `3` + Bonus 연동 정상
- Regeneration 선택당 `+0.5 HP/sec` 적용 정상
- Level-Up Pause 중 Regeneration 정지 정상
- Common Upgrade Level과 Level-Up UI Label 갱신 정상
- Pending Level Ups 순차 처리 정상
- Unity Editor Play Mode 및 Console Error 검증 완료

### U7-A — Skill Loadout Core
- 시작 시 Active Slot 1/2와 Passive Slot 1 모두 Empty, Level `0`
- 같은 Active와 Passive 재획득 시 Level `1 → 2`
- School Skill Level `2` Cap 정상
- Active 두 개 장착 후 세 번째 다른 Active 거부 정상
- Passive 한 개 장착 후 두 번째 다른 Passive 거부 정상
- Debug Reset으로 세 Slot Empty 복구 정상
- 기존 Gameplay Regression과 Console Error 없음
- Magic Missile Test Caster와 Loadout은 의도적으로 분리

### U8-A — Skill Definition + School Metadata
- SkillCatalog에 실제 School Skill `12`개 등록
- Active `8`, Passive Mastery `4`
- Arcane/Fire/Lightning/Frost 각각 Active 2 + Passive 1
- 모든 School Skill Max Level `2`
- Definition 기반 SkillLoadout 획득과 Active 2/Passive 1 제한 정상
- 시작 Loadout Empty와 Common Upgrade Catalog 외부 유지
- Magic Missile Test Caster와 Loadout 분리 유지
- Unity Editor Play Mode 및 Console Error 검증 완료

## Current Work

### U8-B — School Point Calculation
- `SkillLoadout.GetSchoolPoints(SkillSchool)` API 추가
- Active Slot 1/2와 Passive Slot 1을 호출 시점마다 직접 조회
- Slot Skill ID를 SkillCatalog Definition으로 변환하고 해당 School이면 Current Level 합산
- Arcane/Fire/Lightning/Frost 네 School 독립 계산
- Skill Level 1당 School Point 1
- Empty ID, Level 0, Catalog에 없는 Debug Placeholder는 Point 0
- 별도 mutable School Point Field/Counter/Manager 없음
- Loadout Reset만으로 모든 School Point가 자동으로 0
- 현재 Slot/Max Lv.2 규칙으로 School별 자연스러운 최대 `6`
- `Debug Log School Points` Context Menu 추가
- 2/4/6 Synergy, Point HUD, Spell Runtime 연동은 구현하지 않음

남은 확인:
- Unity Editor Script Import 및 C# Compile
- Empty Loadout에서 모든 School `0` 확인
- Magic Missile Lv.1/Lv.2에서 Arcane `1/2` 확인
- Magic Missile/Magic Bolt/Arcane Mastery 합산 Arcane `4 → 6` 확인
- Mixed School에서 Arcane/Fire 독립 계산 확인
- Placeholder Debug Skill Point 제외 확인
- Reset 직후 모든 School `0` 확인
- U1~U8-A Gameplay Regression 확인

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

### U5-A — Experience Orb Drop + Direct Pickup
- Slime Death XP Orb 한 개 생성
- XZ 거리 기반 직접 Pickup
- Player Experience 누적
- Level / Threshold 미구현

### U6-A — Level Progression + Level-Up Pause
- Level / XP Threshold
- Multiple Level Up 처리
- Pending Level Ups
- Level-Up Pause / Debug Resume

### U6-B — Level-Up 3-Choice UI Skeleton
- Placeholder Choice UI
- 정확히 3개 Choice 표시
- Pending Level Up 순차 완료
- Resume

### U6-C — Common Upgrade Choices + Application
- Common Upgrade Choice 데이터
- Maximum Health / Magic Power / Regeneration
- 실제 Upgrade Apply

### U7-A — Skill Loadout Core
- Active Slot 2
- Passive Slot 1
- Skill Lv.1 / Lv.2
- Eligibility

### U8-A — Skill Definition + School Metadata
- Arcane
- Fire
- Lightning
- Frost
- 8 Active + 4 Passive Metadata

### U8-B — School Point Calculation
- 현재 Loadout Skill의 School + Current Level 기반 계산

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
- Current Health가 `0`이면 중복 Death를 방지하고 Component를 비활성화한 뒤 Experience Orb를 한 번 생성하고 Slime Root GameObject를 Destroy한다.
- Editor 검증용 Debug Damage 기본값은 `3`이며 Context Menu도 실제 `TakeDamage`를 호출한다.
- Debug Damage와 Magic Missile Damage는 모두 같은 `TakeDamage → Die` 경로를 사용한다.
- Death Animation, Effect, 범용 Enemy Health 구조는 현재 구현하지 않는다.

### Enemy Spawning
- `EnemySpawner`가 첫 Spawn과 이후 Spawn을 기본 `1.5`초 간격으로 처리한다.
- Spawn 위치는 Player의 X/Z와 임의 각도를 사용하며 반지름은 `14`, World Y는 항상 `0`이다.
- Spawner가 생성한 살아 있는 Slime은 최대 `20`마리로 제한한다.
- Destroy된 Slime은 Unity Object의 Null 상태를 목록에서 제거해 Count를 회수한다.
- Spawn 직후 `SlimeController.Setup`으로 Player Transform, PlayerHealth, PlayerExperience, Experience Orb Prefab과 Billboard Camera를 명시적으로 연결한다.
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

### Player Experience / Experience Orb
- `PlayerExperience`는 게임 시작 시 Level `1`, Current Experience `0`으로 초기화한다.
- `AddExperience(float)`는 NaN, Infinity, 0 이하 값을 무시하고 유효한 XP만 누적한다.
- Base Experience To Level 기본값은 `8`, Experience Growth Per Level 기본값은 `4`다.
- 현재 Requirement는 `8 + (Level - 1) * 4`이며 Inspector의 Experience To Next Level에서 확인한다.
- Threshold 이상이면 현재 Requirement를 차감하고 Level을 올린 뒤 다음 Requirement를 계산한다.
- 남는 XP는 보존하며 한 번의 AddExperience에서 모든 Level Up을 처리한다.
- 한 번의 AddExperience에서 획득한 Level 수는 `LevelsGained` 알림으로 전달한다.
- Slime Experience Reward 기본값은 `4`이며 기존 단일 Death 경로에서 Orb 하나만 생성한다.
- Experience Orb의 기본 Value는 `4`, Pickup Radius는 `1.1`이다.
- Orb Pickup은 Collider나 Rigidbody 없이 Player와 Orb의 XZ 거리로만 판정한다.
- Orb는 Player가 직접 범위 안에 들어왔을 때 XP를 정확히 한 번 지급한 뒤 Root GameObject를 Destroy한다.
- `Time.timeScale`이 `0`인 동안에는 추가 Orb Pickup을 처리하지 않는다.
- Magnet, Attraction, Loot Manager, Event Bus, Service Locator를 사용하지 않는다.
- Orb Root는 Slime Death World Position에 생성하고 Visual Child를 Local Y `0.4`에 두어 Gameplay XZ 위치와 시각 높이를 분리한다.
- Orb의 `BillboardToCamera`는 Prefab에서 Scene Camera를 저장하지 않고 Runtime Setup으로 Main Camera Transform을 받는다.

### Level-Up Pause
- `LevelUpController`는 PlayerExperience의 `LevelsGained` 알림을 구독한다.
- 획득한 Level 수를 Pending Level Ups에 누적하고 하나 이상이면 `Time.timeScale = 0`으로 Gameplay를 Pause한다.
- `Debug Complete Pending Level Up` Context Menu는 Upgrade를 적용하지 않는 Pause/Pending 검사용 fallback으로 유지한다.
- Pending이 남아 있으면 Pause를 유지하고 `0`이 되면 `Time.timeScale = 1`로 Resume한다.
- Controller Disable/Destroy 시 이 Controller가 소유한 Pause를 복구한다.
- Player Movement, Slime AI/Attack, Enemy Spawn, Test Caster, Projectile, Orb Pickup은 `Time.timeScale = 0`일 때 Update Gameplay를 처리하지 않는다.
- Starting Spell Selection Pause는 아직 구현하지 않는다.

### Level-Up Choice UI
- 기존 uGUI `2.0.0`의 Canvas, Button, Legacy Text를 사용하고 새 Package를 설치하지 않는다.
- `LevelUpChoiceUI`는 Panel 하나, 서로 다른 Button 세 개와 Label 세 개만 관리한다.
- Label은 Maximum Health, Magic Power, Regeneration의 현재 Level, 다음 Level과 고정 효과를 표시한다.
- Level Up 발생 시 Controller가 Pause와 Pending 누적 후 UI를 표시한다.
- Button 클릭 시 해당 화면의 세 Button을 즉시 잠그고 선택 효과를 적용한 뒤 Pending을 정확히 하나만 감소시킨다.
- Pending이 남으면 `Time.timeScale = 0`을 유지하고 같은 Panel에 다음 세 Choice를 즉시 표시한다.
- Pending이 `0`일 때만 Panel을 숨기고 Gameplay를 Resume한다.
- UI가 숨겨진 상태의 Choice, 범위 밖 Choice Index, 중복 Button Reference는 안전하게 거부한다.
- 이번 Phase에서는 세 Common Upgrade가 항상 표시되며 Random Pool을 사용하지 않는다.

### Common Upgrade
- Common Upgrade는 Slot과 School Point를 사용하지 않으며 반복 선택할 수 있다.
- Maximum Health, Magic Power, Regeneration Level은 각각 `0`으로 시작하고 선택 시 `+1`된다.
- Maximum Health는 Maximum HP와 Current HP를 각각 `+10`하며 Current HP는 Maximum HP를 넘지 않는다.
- Magic Power는 별도 `PlayerMagicPower`의 Magic Damage Bonus를 `+1`한다.
- Test-only Magic Missile은 Inspector Damage `3`을 Base Damage로 유지하고 발사 시 Bonus를 더한 최종 Damage를 Projectile에 전달한다.
- Regeneration은 PlayerHealth가 보유하며 선택당 `+0.5 HP/sec`다. Player HP가 `0`보다 크고 Maximum 미만일 때만 `Time.deltaTime`으로 회복한다.
- Maximum Level, Random Upgrade Pool, ScriptableObject Upgrade Database, 범용 Damage/Ability Framework는 구현하지 않는다.

### Skill Loadout Core
- School Skill Slot은 Active Slot 1, Active Slot 2, Passive Slot 1로 고정한다.
- 게임 시작 시 세 Slot은 빈 Skill ID와 Level `0`으로 초기화한다.
- 새 Active는 Active 1부터 채우고 다음 Active를 Active 2에 넣으며, 두 Slot이 차면 새로운 Active를 거부한다.
- Passive는 하나만 장착하며 다른 Passive가 있으면 새로운 Passive를 거부한다.
- 이미 장착된 같은 Skill ID는 같은 SkillType일 때만 Level을 올리고 Maximum Level `2`에서 거부한다.
- 같은 Skill ID를 두 Slot에 중복 장착하지 않는다.
- Common Upgrade는 Slot을 사용하지 않으며 SkillLoadout에 넣지 않는다.
- U7-A Debug Placeholder ID는 규칙 검증용 상태일 뿐 실제 Skill Content가 아니다.
- Magic Missile Test Caster는 SkillLoadout과 연결하지 않으며 자동 장착하지 않는다.
- School Point와 Spell 효과 연동은 후속 Phase로 미룬다.

### Skill Definition / Catalog
- Definition은 ID, Display Name, School, SkillType, MaxLevel만 가진 불변 C# 데이터다.
- Current Level은 Definition이 아니라 Runtime `SkillLoadout` 상태에만 저장한다.
- School은 Arcane, Fire, Lightning, Frost 네 개만 정의한다.
- Catalog는 각 School의 Active 2개와 Passive Mastery 1개, 총 12개만 포함한다.
- 전체 구성은 Active 8, Passive 4이며 모든 School Skill MaxLevel은 `2`다.
- Catalog는 ID의 Ordinal Dictionary를 만들며 중복 ID를 허용하지 않고 null/공백 조회는 안전하게 실패한다.
- Common Upgrade 3종은 School Skill이 아니므로 Catalog에 포함하지 않는다.
- School Point는 별도 상태로 저장하지 않고 현재 Loadout에서 계산한다.
- ScriptableObject Database, Reflection Registry, Generic Ability Framework를 사용하지 않는다.

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
- School Point의 Source of Truth는 현재 `SkillLoadout`의 세 Slot뿐이다.
- `GetSchoolPoints(SkillSchool)` 호출 시 Active 1/2와 Passive 1을 다시 조회한다.
- 각 Slot의 Catalog Definition School이 요청 School과 같으면 Current Level을 합산한다.
- Skill Level 1당 Point 1이며 현재 Active 2 + Passive 1, 각 Max Lv.2 규칙으로 School별 최대는 자연스럽게 `6`이다.
- Empty, Level `0`, Catalog에 없는 Debug Placeholder Skill은 Point를 제공하지 않는다.
- Loadout Reset 뒤 별도 Point Reset 없이 계산 결과가 자동으로 모두 `0`이 된다.
- 별도 Point Field, 누적 Counter, Manager를 만들지 않는다.
- 2/4/6 Synergy와 활성 여부는 아직 구현하지 않는다.

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
  - `EnemySpawner`
  - `GameSystems`
  - `EventSystem`
  - `Canvas`
- 저장된 `player` Root는 `PlayerMovement`, `PlayerHealth`, Test-only `MagicMissileCaster`, `PlayerExperience`, `PlayerMagicPower`를 사용하며 Position `(0, 0, 0)`, Rotation `(0, 0, 0)`이다.
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
- `player`의 Test-only `MagicMissileCaster`에는 실제 Magic Missile Prefab, `EnemySpawner`, `PlayerMagicPower`가 연결되어 있으며 Base Damage + Bonus 적용을 검증했다.
- `player` Root의 `PlayerExperience`는 U5-A Play Mode 검증에 사용됐다.
- `player/Visual`의 중복 PlayerExperience는 제거됐으며 Root에 하나만 남아 있다.
- 수동 배치 Slime은 Scene에서 제거됐다.
- `EnemySpawner` Root에는 `EnemySpawner` Component가 연결되어 있다.
  - Player: `player`
  - Slime Prefab: 실제 Slime Prefab Asset
  - Billboard Camera: `Main Camera`
  - Spawn Interval `1.5`
  - Spawn Distance `14`
  - Maximum Enemy Count `20`
- Experience Orb Prefab: `Assets/Prefabs/Pickups/ExperienceOrb.prefab`
- `GameSystems`에는 `LevelUpController`가 연결되어 있다.
  - Player Experience: `player` Root의 `PlayerExperience`
  - Level Up Choice UI: `Canvas`의 `LevelUpChoiceUI`
  - Common Upgrade Controller: 같은 `GameSystems`의 `CommonUpgradeController`
  - Pending Level Ups `0`
- `Canvas`, `EventSystem`, 비활성 `LevelUpPanel`, Title과 세 Choice Button이 Main Scene에 존재한다.
- 저장된 Main Scene의 `LevelUpChoiceUI`에는 LevelUpPanel과 Button/Label 세 쌍이 모두 연결되어 있다.
- `GameSystems`에는 PlayerHealth/PlayerMagicPower가 연결된 `CommonUpgradeController`가 있으며 U6-C Play Mode 검증이 완료됐다.
- `GameSystems`에는 `SkillLoadout`이 저장되어 있으며 Active Slot 1/2와 Passive Slot 1은 빈 Skill ID와 Level `0`으로 시작한다. U7-A Slot Rule은 Editor 검증 완료됐고 U8-A에도 새 Scene Reference는 필요하지 않다.
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
- Prefab의 Target, PlayerHealth, PlayerExperience, Experience Orb Prefab과 Billboard Camera는 Runtime에 연결된다.
- U3-B Separation은 Script 기본값 Radius `0.75`, Strength `0.35`로 Editor 검증 완료됐다.
- Magic Missile 실제 경로: `Assets/Prefabs/Projectiles/MagicMissile.prefab`
- Magic Missile Root에는 `MagicMissileProjectile`, Visual Child에는 Circle `SpriteRenderer`와 `BillboardToCamera`가 있다.
- Experience Orb 실제 경로: `Assets/Prefabs/Pickups/ExperienceOrb.prefab`
- Experience Orb Root에는 `ExperienceOrb`, Visual Child에는 Cyan Circle `SpriteRenderer`와 `BillboardToCamera`가 있다.
- Experience Orb Value `4`, Pickup Radius `1.1`, Visual Local Position `(0, 0.4, 0)`, Scale `(0.35, 0.35, 0.35)`다.

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
  - HP 0에서 Experience Orb 한 개 생성 후 Slime Root GameObject Destroy
  - Debug Damage 기본값 `3`과 `Debug Take Damage` Context Menu
  - Experience Reward 기본값 `4`
  - Runtime `Setup`으로 Player/Health/Experience/Orb Prefab/Camera와 이웃 목록 연결
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
  - Spawn 시 Player/PlayerHealth/PlayerExperience/Experience Orb Prefab/Billboard Camera Runtime 연결
  - 기존 Spawned Slime 목록을 각 Slime의 Separation 이웃 목록으로 전달
  - Test Combat Targeting용 `SpawnedEnemies` 읽기 전용 목록과 Billboard Camera 제공
  - 필수 Reference Null 방어
  - U3-A Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Combat/MagicMissileCaster.cs`
  - Player에 사용자가 수동 추가하는 Test-only Component
  - EnemySpawner 목록에서 XZ 최근접 살아 있는 Slime 선택
  - 기본 Damage `3`을 Base Damage로 유지하며 `PlayerMagicPower` Bonus를 발사 시 합산
  - Cooldown `0.65`, Speed `6`, Lifetime `5`, Collision Radius `0.22`
  - Projectile 생성과 Runtime Target/Camera/수치 전달
  - Target 없음과 필수 Reference Null 방어
  - U4-A Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Combat/MagicMissileProjectile.cs`
  - 살아 있는 Target을 향한 XZ Homing 이동
  - Player Y `+1.25` 발사 높이 유지
  - XZ Collision Radius 명중 판정과 `SlimeController.TakeDamage`
  - 명중, Lifetime 만료, Target 선행 사망 시 Root Destroy
  - 하위 Billboard Camera Runtime 연결
  - U4-A Projectile Prefab 생성 및 Play Mode 검증 완료
- `Assets/Scripts/Player/PlayerExperience.cs`
  - Level `1`, Current Experience `0`, 첫 Requirement `8` 초기화
  - Base Requirement `8`, Level당 Growth `4`
  - Invalid/0 이하 XP 방어와 유효 XP 누적
  - 초과 XP 보존과 다중 Level Up 계산
  - 획득 Level 수 `LevelsGained` 알림
  - Inspector에서 Level, Current Experience, Experience To Next Level 확인 가능
- `Assets/Scripts/Experience/ExperienceOrb.cs`
  - Value 기본값 `4`, Pickup Radius 기본값 `1.1`
  - Runtime Player/PlayerExperience/Billboard Camera/Reward 연결
  - XZ 거리 기반 직접 Pickup과 중복 Collect 방어
  - Pickup 후 Orb Root Destroy
  - Pause 중 추가 Pickup 차단
  - Collider/Rigidbody/Magnet 없음
- `Assets/Scripts/Progression/LevelUpController.cs`
  - PlayerExperience `LevelsGained` 구독
  - Pending Level Ups 누적과 `Time.timeScale = 0` Pause
  - Level Up 시 `LevelUpChoiceUI` 표시
  - UI Choice Index 검증 후 `CommonUpgradeController` 적용과 Pending 하나 완료
  - Pending이 남으면 다음 Choice 표시, 마지막 Pending에서 UI 숨김과 Resume
  - Debug Context Menu로 Pending 하나씩 완료
  - Pending `0`에서 `Time.timeScale = 1` Resume
  - Disable/Destroy UI 숨김/Pause 복구와 필수 Reference Null 방어
- `Assets/Scripts/UI/LevelUpChoiceUI.cs`
  - Panel 한 개와 서로 다른 uGUI Button/Text Reference 세 쌍 관리
  - 시작 시 Panel 숨김
  - Common Upgrade 이름, 현재/다음 Level, 효과 Label 표시
  - Button 클릭 잠금과 UI 미표시 상태 중복 처리 방어
  - Controller에 Choice Index 전달
- `Assets/Scripts/Progression/CommonUpgradeController.cs`
  - PlayerHealth와 PlayerMagicPower Reference 보유
  - Maximum Health, Magic Power, Regeneration 선택 Level을 각각 Inspector에 표시
  - 세 고정 Choice 효과 적용과 현재 Level 기반 Label 생성
- `Assets/Scripts/Player/PlayerMagicPower.cs`
  - Magic Damage Bonus `0` 초기화
  - 유효한 증가량 누적과 Base Damage + Bonus 계산
  - NaN, Infinity, 0 이하 입력 방어
- `Assets/Scripts/Player/PlayerHealth.cs`
  - Maximum HP 기본값 `100`
  - Current HP Inspector 확인 가능
  - `TakeDamage(float)`
  - Invalid/0 이하 Damage 무시 및 HP 0 Clamp
  - `IncreaseMaximumHealth(float)`로 Maximum/Current HP 동시 증가
  - Health Regeneration `0` 초기값과 `IncreaseHealthRegeneration(float)`
  - 살아 있고 Maximum 미만일 때 `Time.deltaTime` 기반 회복, Pause 중 정지
  - Player Death 없음
  - Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Skills/SkillLoadout.cs`
  - `SkillType.Active/Passive` 최소 enum
  - Active Slot 1/2와 Passive Slot 1의 private 직렬화 Skill ID/Level 상태
  - 시작 Empty/Level `0`, School Skill Maximum Level `2`
  - `CanAcquireOrUpgrade`와 `AcquireOrUpgrade`의 슬롯/Type/Level 규칙
  - Null/공백 ID, Type 불일치, 중복 Slot, Level Cap 방어
  - 실제 콘텐츠가 아닌 Debug Placeholder Context Menu와 Reset
  - `SkillDefinition` 기반 Can/Acquire 오버로드와 Definition MaxLevel 적용
  - Magic Missile/Magic Bolt/Fireball/Arcane Mastery/Fire Mastery Definition Debug Context Menu
  - 현재 세 Slot을 조회하는 `GetSchoolPoints(SkillSchool)` 실시간 계산 API
  - Empty/Level 0/Unknown Debug ID 제외와 Invalid School 안전한 `0`
  - `Debug Log School Points` Context Menu
  - 외부 System Reference 및 Spell Runtime/Common Upgrade 연동 없음
- `Assets/Scripts/Skills/SkillDefinition.cs`
  - `SkillSchool.Arcane/Fire/Lightning/Frost`
  - ID, Display Name, School, 기존 SkillType, MaxLevel read-only Property
  - Invalid text, School, Type, MaxLevel 생성 방어
  - School Skill Maximum Level 상수 `2`
- `Assets/Scripts/Skills/SkillCatalog.cs`
  - 4 School의 Active 8 + Passive 4, 총 12 Definition
  - Read-only 전체 목록과 ID 기반 안전한 `TryGet`
  - Ordinal Dictionary 구성으로 중복 ID 방어
  - Common Upgrade 미포함
- `Assets/Scripts/Visual/BillboardToCamera.cs`
  - Camera Transform 참조
  - Spawn된 Visual용 Runtime `SetCamera(Transform)` 연결 지점
  - `LateUpdate`에서 Visual Child만 Camera 방향으로 회전
  - Camera Null 방어
  - Player/Slime Visual Child와 Main Camera 연결 완료
  - U1-D 조작 수정 후 최종 Play Mode Regression 확인 완료
- Assembly Definition 없음

## Known Issues
- U8-B School Point 계산과 Debug Log는 아직 Unity Editor Compile 및 Play Mode 검증 전이다.
- Skill Definition은 Metadata뿐이며 실제 Spell/Passive 효과를 실행하지 않는다.
- U7-A Debug Placeholder Skill ID는 슬롯 규칙 검증용이며 Catalog의 실제 Skill이 아니다.
- 2/4/6 Synergy 효과와 UI는 아직 구현되지 않았다.
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
- U9 Starting Spell Selection, 실제 Skill 효과와 2/4/6 Synergy 구현
- XP Magnet, Pickup Attraction, XP Merge, Loot System, Object Pool

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**Pending U8-B Editor Verification**

기존 `GameSystems/SkillLoadout`의 Definition Debug와 `Debug Log School Points`로 Empty, 단일 Skill, Same School `6`, Mixed School, Placeholder 제외와 Reset 자동 `0`을 검증한다.

검증 성공 후 다음 Phase로 `U9-A — Starting Spell Selection Data + Flow`를 제안한다. 이번 작업에서는 Starting Spell, Spell Runtime 연동, Synergy를 구현하지 않는다.
