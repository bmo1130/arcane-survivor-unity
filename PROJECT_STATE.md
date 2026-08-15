# Arcane Survivor Unity — Project State

## Current Phase
**U12 — Complete School Synergy 2/4/6 / Editor Verification Pending**

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

U4-A 당시 Test-only Magic Missile의 자동 Target 선택, Homing, Damage, Target 선행 사망 처리와 Projectile Lifetime을 Unity Editor에서 검증했다. 기존 Spawn/Separation과 U1~U3 기능도 정상이며 Console Error가 없다.

Slime Death 시 Experience Orb 생성, Reward `4`, Pickup Radius `1.1`, 직접 Pickup과 `0 → 4 → 8` 누적을 Unity Editor에서 검증했다. Magnet과 중복 획득은 없으며 기존 Combat/Spawn/Movement도 정상이고 Console Error가 없다.

Level `1`, XP `0/8`, 두 Orb 후 Level `2`, XP `0/12`, Pending `1`, `Time.timeScale = 0`과 전체 Gameplay Pause를 Unity Editor에서 검증했다. Debug Pending 완료 후 `Time.timeScale = 1` Resume와 다음 Requirement `12`도 정상이며 Console Error가 없다.

Level Up 시 정확히 3개의 Button 표시, Pause 상태의 클릭, Pending 순차 감소, Multiple Pending 동안 Pause 유지와 마지막 Pending 이후 Resume를 Unity Editor에서 검증했다. Console Error가 없으며 U6-B는 완료됐다.

Maximum Health, Magic Power, Magic Missile Base Damage + Bonus, Regeneration, Pause 중 Regeneration 정지, Common Upgrade Level/UI Label 갱신과 Pending 순차 처리를 Unity Editor에서 검증했다. Console Error가 없으며 U6-C는 완료됐다.

Active 1/2와 Passive 1의 Empty 시작, 동일 Skill Lv.2 Cap, 새 Active/Passive Slot 제한, Debug Reset과 기존 Gameplay Regression을 Unity Editor에서 검증했다. Console Error가 없으며 U7-A는 완료됐다.

SkillCatalog의 실제 School Skill 12개, Active 8/Passive 4, School별 3개, Max Lv.2, Definition 기반 Loadout 획득과 기존 Slot 제한을 Unity Editor에서 검증했다. 시작 Loadout은 Empty이고 Common Upgrade와 Test Caster 분리도 유지되며 Console Error가 없다. U8-A는 완료됐다.

Empty/단일/동일 School/Mixed School Loadout, Arcane 최대 `6`, Debug Placeholder 제외와 Reset 자동 `0`을 Unity Editor에서 검증했다. 별도 mutable Point State는 없고 기존 Gameplay와 Console도 정상이며 U8-B는 완료됐다.

게임 시작 Empty Loadout/Pause, Active 8개 중 한 개 선택, Active Slot 1 Lv.1, Resume, 중복 선택 거부와 School Point 반영을 Unity Editor에서 검증했다. Magic Missile 선택 시에만 기존 Missile Runtime이 작동하고 다른 Starting Spell에서는 발사하지 않으며 Console Error가 없다. U9-A는 완료됐다.

Play 시작 Starting UI 표시, Active 8 Button, Pause, UI 선택 후 Active Slot 1 Lv.1/Close/Resume, Loadout별 Magic Missile gating과 이후 Level-Up UI 분리를 Unity Editor에서 검증했다. Starting Spell은 한 번만 선택되며 Console Error가 없다. U9-B는 완료됐다.

Magic Bolt Starting 선택, `magic-bolt` Loadout gating, non-homing 직선 발사, Magic Missile과 독립 동작, Damage/Collision/Lifetime을 Unity Editor에서 검증했다. Console Error가 없으며 U10-A는 완료됐다.

Fireball/Lv.2 Radius, Burning과 Tick Progress 유지, Fire Zone/Lv.2 Radius, Fire Mastery Lv.1/Lv.2, Magic Power와 Pause를 Unity Editor에서 검증했다. 기존 Gameplay Regression과 Console Error가 없으며 U10-Fire는 완료됐다.

Chain Lightning/Lv.2 Damage, Lightning Orb/Lv.2 Bounce, Lightning Mastery Lv.1/Lv.2, Stagger, Pause와 기존 Gameplay Regression을 Unity Editor에서 검증했다. Console Error가 없으며 U10-Lightning은 완료됐다.

Ice Bolt/Lv.2 AoE, Blizzard/Lv.2 Radius, Slow, Frost Mastery Lv.1/Lv.2, Burn/Stagger/Slow 공존과 Pause를 Unity Editor에서 검증했다. 복원한 Lightning Caster도 Main Scene에 저장됐으며 기존 Gameplay Regression과 Console Error가 없다. U10-Frost는 완료됐다.

Magic Missile/Magic Bolt Lv.2, Arcane Mastery Lv.1/Lv.2와 8 Active Spell 전체 Cooldown 적용을 Unity Editor에서 검증했다. 기존 Gameplay Regression과 Console Error가 없으며 U10-Arcane은 완료됐다.

Common Upgrade와 현재 SkillLoadout 규칙상 eligible한 School Skill을 통합한 무작위 3-choice, Slot/Max Lv.2 필터, Runtime 즉시 반영과 Multi-Level Queue를 Unity Editor에서 검증했다. Console Error가 없으며 U11은 완료됐다.

현재 네 School의 2/4/6 Point Synergy를 별도 Point State 없이 현재 Loadout에서 실시간 계산해 전투 Runtime에 적용하는 U12의 Editor 검증을 기다리고 있다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

현재 구현 범위는 **U12 — Complete School Synergy 2/4/6**이다.

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
- U4-A 검증 당시 `MagicMissileCaster`는 Test Caster였으며 강제 Starting Skill은 아니었음. U9-A부터 실제 Loadout 장착 여부로 동작

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
- U4-A Magic Missile Base Damage `3` + Bonus 연동 정상
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
- U7-A 검증 당시 Magic Missile Test Caster와 Loadout은 의도적으로 분리했으며 U9-A에서 실제 장착 조건으로 전환

### U8-A — Skill Definition + School Metadata
- SkillCatalog에 실제 School Skill `12`개 등록
- Active `8`, Passive Mastery `4`
- Arcane/Fire/Lightning/Frost 각각 Active 2 + Passive 1
- 모든 School Skill Max Level `2`
- Definition 기반 SkillLoadout 획득과 Active 2/Passive 1 제한 정상
- 시작 Loadout Empty와 Common Upgrade Catalog 외부 유지
- U8-A 검증 당시 Magic Missile Test Caster와 Loadout 분리 유지
- Unity Editor Play Mode 및 Console Error 검증 완료

### U8-B — School Point Calculation
- Empty Loadout에서 Arcane/Fire/Lightning/Frost 모두 `0`
- Magic Missile Lv.1/Lv.2에서 Arcane `1/2`
- 동일 School Skill Level 합산과 Arcane 최대 `6` 정상
- Mixed School별 독립 계산 정상
- Debug Placeholder Skill은 Point 계산에서 제외
- Loadout Reset만으로 모든 School Point 자동 `0`
- 별도 mutable School Point State 없음
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

### U9-A — Starting Spell Selection Core
- Play 시작 직후 Loadout Empty와 `Awaiting Selection = true`
- Gameplay Pause, Enemy Spawn 정지, Magic Missile 발사 정지 정상
- Magic Missile 선택 시 Active Slot 1 Lv.1, Resume와 자동 공격 시작
- 다른 Starting Active 선택 시 Active Slot 1 Lv.1과 Resume, Magic Missile 미발사
- Starting Spell 중복 선택 거부 정상
- 선택한 Skill School Point `1` 자연 반영
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

### U9-B — Starting Spell Selection UI
- Play 시작 시 Starting Spell UI와 Active 8 Button 표시 정상
- Starting Selection 중 Gameplay Pause 정상
- Magic Missile UI 선택 후 Active Slot 1 Lv.1, UI Close, Resume와 공격 시작
- 다른 Starting Spell 선택 후 Active Slot 1 Lv.1과 Resume, Magic Missile 미발사
- Starting Spell 1회 제한 정상
- 이후 Level-Up UI와 Starting UI 충돌 없음
- Unity Editor Play Mode 및 Console Error 검증 완료

### U10-A — Magic Bolt Runtime
- Magic Bolt Starting Spell 선택과 `magic-bolt` Loadout gating 정상
- Cast 시점 방향으로만 이동하는 non-homing 직선 발사 정상
- Magic Missile과 Loadout에 따라 독립적으로 동작
- Damage, 첫 Collision, Lifetime 처리 정상
- Unity Editor Play Mode 및 Console Error 검증 완료

### U10-Fire — Complete Fire School Runtime
- Fireball과 Fireball Lv.2 Explosion Radius 정상
- Burning과 Burn Refresh 중 Tick Progress 유지 정상
- Fire Zone과 Fire Zone Lv.2 Radius 정상
- Fire Mastery Lv.1/Lv.2 및 PlayerMagicPower 적용 정상
- Pause와 기존 Gameplay Regression 정상
- Unity Editor Play Mode 및 Console Error 검증 완료

### U10-Lightning — Complete Lightning School Runtime
- Chain Lightning과 Lv.2 Damage 정상
- Lightning Orb가 Enemy Collision로 사라지지 않고 Lifetime 동안 Pulse
- Lightning Orb Lv.2 Bounce 정상
- Lightning Mastery Lv.1/Lv.2 Bounce Bonus 정상
- Stagger와 Pause 정상
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

### U10-Frost — Complete Frost School Runtime
- Ice Bolt Lv.1/Lv.2 Damage, Slow와 AoE 정상
- Blizzard Lv.1/Lv.2 Tick, 고정 Area와 Radius 정상
- Frost Mastery Lv.1/Lv.2의 Attack/Movement Slow 정상
- Burn, Stagger, Slow 공존과 Pause 정상
- Lightning Caster 복원 후 Main Scene 저장 완료
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

### U10-Arcane — Complete Arcane School Runtime
- Magic Missile Lv.2 Damage/Projectile Count 정상
- Magic Bolt Lv.2 Damage/Projectile Count와 non-homing 정상
- Arcane Mastery Lv.1/Lv.2 Cooldown Multiplier 정상
- Arcane Mastery의 8 Active Spell 전체 Cooldown 적용 정상
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

### U11 — Level-Up School Skill Pool
- Common Upgrade와 eligible School Skill이 섞인 중복 없는 Random 3-choice 정상
- Starting Skill Lv.1 → Lv.2 후보와 실제 Upgrade 정상
- Active 2/Passive 1 Slot 제한과 Max Lv.2 후보 제거 정상
- Common Upgrade는 Slot 상태와 관계없이 계속 후보가 될 수 있음
- Level-Up으로 획득한 Active Runtime 즉시 동작 정상
- Multiple Pending마다 현재 Loadout 기준 새 Candidate Pool 생성과 Queue 처리 정상
- 기존 Gameplay Regression과 Console Error 없음
- Unity Editor Play Mode 검증 완료

## Current Work

### U12 — Complete School Synergy 2/4/6
- 모든 Synergy는 `SkillLoadout.GetSchoolPoints`에서 현재 Loadout Level 합을 실시간 조회하며 별도 Point Counter를 저장하지 않음
- Arcane 2/4/6: 지정된 다섯 Spell Projectile 총 `+1/+1/+2`, 모든 Magic Damage 총 `+0/+1/+2`
- Fire 2: Burn Duration `3 → 5`초
- Fire 4: Burn Tick Interval `1 → 0.65`초
- Fire 6: Burning Enemy가 반경 `3.2`의 살아 있고 Burning이 아닌 Enemy에 `1`초 간격으로 Burn 전염
- Lightning 2: Lightning Stagger Duration 최소 `0.15`초
- Lightning 4: Chain Hit Index마다 Damage `+0/+1/+2...`
- Lightning 6: Chain/Orb Bounce Count `+2`
- Frost 2/4: Slow Duration `2.5 → 4 → 5.5`초
- Frost 6: Slow 경과 시간당 Move Multiplier `0.06` 감소, 최소 `0.25`
- Breakpoint 효과는 누적되며 기존 Mastery/Common Magic Power/Spell Lv.2 효과와 함께 적용
- 기존 Scene/Prefab Reference와 Component 추가 없음
- Synergy UI, VFX와 별도 Manager/Counter 없음

남은 확인:
- Arcane 2/4/6의 Projectile 수와 모든 Direct/Status Magic Damage 확인
- Fire 2/4의 Duration/Tick, Fire 6의 비-Burning 이웃 전염과 Pause 확인
- Lightning 2/4/6의 Stagger/Hit Scaling/Bounce 확인
- Frost 2/4 Duration과 Frost 6 Progressive Slow/Refresh/Expiry 확인
- 기존 U1~U11 Gameplay Regression과 Console Error 확인

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

### U9-A — Starting Spell Selection Core
- Empty Loadout Start
- 8 Active Choice
- Lv.1 Active Slot 1

### U9-B — Starting Spell Selection UI
- 8 Active Button Choice

### U10-A — Magic Bolt Runtime
- Magic Bolt 자동 공격 Runtime

### U10-Fire — Complete Fire School Runtime
- Fireball, Fire Zone, Burning, Fire Mastery와 Active Lv.2 반경 효과

### U10-Lightning — Complete Lightning School Runtime
- Chain Lightning, Lightning Orb, Stagger, Lightning Mastery와 Active Lv.2 효과

### U10-Frost — Complete Frost School Runtime
- Ice Bolt, Blizzard, Slow, Frost Mastery와 Active Lv.2 효과

### U10-Arcane — Complete Arcane School Runtime
- Magic Missile/Magic Bolt Lv.2와 모든 Active Cooldown에 적용되는 Arcane Mastery

### U10 — Complete School Skills
- Arcane
- Fire
- Lightning
- Frost

### U11 — Level-Up School Skill Pool
- 실제 Level-Up 3-choice에 School Skill과 Common Upgrade 후보 연결

### U12 — Synergy
- 각 School 2/4/6

### U13 — Prototype Demo Pass
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

### Magic Missile Runtime
- Magic Missile은 강제 Starting Skill이 아니며 8개 Active Starting Choice 중 하나다.
- `MagicMissileCaster`는 Player에 유지하되 현재 `SkillLoadout`에 `magic-missile`이 Lv.1 이상 장착된 경우에만 동작한다.
- Empty Loadout 또는 다른 Starting Spell 선택 시 Target을 찾거나 Projectile을 발사하지 않는다.
- Magic Missile 선택 시 기존 U4-A 전투 동작을 그대로 사용한다.
- 기존 EnemySpawner 목록에서 현재 Projectile 수만큼 Player와 XZ 기준 가까운 살아 있는 Slime을 선택한다.
- Lv.1은 Base Damage `3`, Projectile `1`이고 Lv.2는 Base Damage `4`, Projectile `2`다.
- Lv.2는 가장 가까운 두 Enemy를 우선 각각 Target으로 사용하며 Enemy가 하나면 두 Projectile 모두 같은 Enemy를 Target으로 사용할 수 있다.
- Lv.2 두 Projectile은 Player Y `+1.25` Launch Center에서 XZ lateral 방향으로 `0.32` 간격을 두고 생성한다.
- Target이 없으면 발사하지 않고 Cooldown 준비 상태를 유지한다.
- Reference Prototype과 같이 Projectile은 살아 있는 Target을 매 Frame 추적하는 Homing 방식이다.
- Target이 먼저 죽으면 Retarget하지 않고 Projectile을 제거한다.
- Inspector 기본값은 Damage `3`, Cooldown `0.65`, Speed `6`, Lifetime `5`, Collision Radius `0.22`로 유지한다.
- Slime HP `10`에는 네 번 명중해야 Death가 발생한다.
- Projectile은 Player Y `+1.25`에서 생성되고 XZ 평면으로 이동하며 시각 높이를 유지한다.
- Rigidbody, Collider, Physics, FindObjectsByType, 범용 Combat/Projectile Framework를 사용하지 않는다.

### Magic Bolt Runtime
- Magic Bolt는 `magic-bolt`가 현재 SkillLoadout에 Lv.1 이상 장착된 경우에만 자동 시전한다.
- Cast 시 현재 Projectile 수만큼 Player와 XZ 기준 가까운 살아 있는 Slime을 선택하고 각 Projectile의 고정 방향을 계산한 뒤 Target을 보관하지 않는다.
- Lv.1은 Base Damage `4`, Projectile `1`이고 Lv.2는 Base Damage `5`, Projectile `2`다.
- Lv.2는 가장 가까운 두 Enemy를 우선 각각 방향 Target으로 사용하며 Enemy가 하나면 두 Projectile 모두 같은 Enemy 방향으로 발사한다.
- Lv.2 두 Projectile은 Player Y `+1.25` Launch Center에서 XZ lateral 방향으로 `0.22` 간격을 두고 생성한다.
- Projectile은 초기 Direction으로만 직진하며 Enemy 이동이나 원래 Target Death에 반응해 방향을 바꾸지 않는다.
- 빠른 이동의 Frame 통과를 줄이기 위해 이전→다음 XZ Segment와 각 살아 있는 Slime 중심의 Collision Radius 교차를 계산한다.
- 하나의 Segment에서 가장 이른 충돌 한 개만 처리하며 Piercing과 다중 Hit는 없다.
- Inspector 기본값은 Base Damage `4`, Cooldown `0.9`, Speed `9`, Lifetime `4`, Collision Radius `0.2`로 유지한다.
- PlayerMagicPower Bonus는 Caster에서 Base Damage에 더해 최종 Damage로 Projectile에 전달한다.
- Projectile은 Player Y `+1.25`에서 생성되고 그 높이를 유지한다.
- Pause 중 Cooldown, Projectile 이동과 Lifetime은 진행하지 않는다.
- Magic Missile과 별도 작은 구현을 유지하며 Generic Projectile Base, Rigidbody/Collider와 Object Pool을 사용하지 않는다.

### Arcane Mastery Runtime
- 별도 MonoBehaviour 없이 `SkillLoadout.GetSkillLevel("arcane-mastery")`를 조회한다.
- Lv.0/Lv.1/Lv.2의 Spell Cooldown Multiplier는 각각 `1/0.9/0.85`다.
- Lv.2는 Lv.1과 합산한 25% 감소가 아니라 총 15% 감소다.
- `GetModifiedSpellCooldown(float)`은 Invalid/0 이하 Base Cooldown을 안전한 `0`으로 처리한다.
- Magic Missile, Magic Bolt, Fireball, Fire Zone, Chain Lightning, Lightning Orb, Ice Bolt, Blizzard가 Cast에 성공한 뒤 설정하는 다음 Cooldown에 적용한다.
- 진행 중인 Cooldown은 Mastery 획득 시 소급 재계산하지 않고 다음 Cast부터 새 배율을 사용한다.
- Inspector의 각 Caster Base Cooldown 값은 변경하지 않는다.
- Arcane Mastery는 Damage, Projectile Count, Radius, Status 강도에 영향을 주지 않는다.
- Arcane Point `2/4/6`은 누적으로 지정된 다섯 Spell Projectile `+1/+1/+2`, 모든 Magic Damage `+0/+1/+2`를 적용한다.
- 추가 Projectile은 가까운 서로 다른 Enemy를 우선하고 Target이 부족하면 기존 Target을 재사용한다. Magic Missile만 Homing을 유지한다.

### Burning Runtime
- `BurnStatus`는 Slime Root에 하나만 부착하는 Fire 전용 상태 Component다.
- Burn 재적용은 중첩 Stack을 추가하지 않고 남은 Duration, Tick Damage와 Tick Interval을 현재 Loadout 기준 최신 값으로 갱신한다.
- 재적용 시 기존 Tick Progress는 유지하므로 Fire Zone의 `0.5`초 재적용이 `1`초 Burn Tick을 막지 않는다.
- 기본 Tick Damage `1`, Duration `3`, Tick Interval `1`을 사용한다.
- Fire 2/4에서 Duration `5`, Tick Interval `0.65`를 사용하며 Fire 6은 반경 `3.2`의 살아 있고 Burning이 아닌 Enemy에게 `1`초마다 Burn을 전염한다.
- 전염 Burn도 현재 Fire Mastery, PlayerMagicPower, Arcane Damage와 Fire 2/4를 다시 계산하며 이미 Burning인 Enemy를 전염으로 Refresh하지 않는다.
- Tick은 기존 `SlimeController.TakeDamage` 단일 Damage/Death 경로를 사용한다.
- Pause 중 Duration과 Tick Progress는 진행하지 않는다.
- Generic Status Framework와 Status Manager는 사용하지 않는다.

### Fireball Runtime
- `fireball`이 SkillLoadout에 Lv.1 이상 장착된 경우에만 자동 시전한다.
- Cast 시 XZ 최근접 살아 있는 Slime 방향을 한 번 정하고 이후 Target을 추적하지 않는다.
- XZ Segment Collision의 첫 Enemy 한 개에 Direct Damage 후 Impact 반경의 모든 살아 있는 Slime에 Burn을 적용한다.
- Base Direct Damage `1`, Cooldown `1.35`, Speed `7.5`, Lifetime `4`, Collision Radius `0.22`를 사용한다.
- Explosion Radius는 Lv.1 `2.2`, Lv.2 `3.4`이며 Lv.2의 다른 전용 효과는 없다.
- Direct Damage와 Burn Damage 모두 PlayerMagicPower Bonus를 사용한다.
- 아무 Enemy도 맞히지 못하고 Lifetime이 끝나면 Explosion 없이 Destroy한다.

### Fire Zone Runtime
- `fire-zone`이 SkillLoadout에 Lv.1 이상 장착된 경우에만 가장 가까운 살아 있는 Slime의 Cast 시점 위치에 생성한다.
- 생성된 Zone은 Target을 따라가지 않고 고정된 XZ World Position을 유지한다.
- Cooldown `4`, Duration `4`, Burn Apply Interval `0.5`를 사용하며 Zone 자체 Direct Damage는 없다.
- Radius는 Lv.1 `2.2`, Lv.2 `3.5`다.
- 범위 내 Slime의 기존 Burn을 반복 갱신하며 Tick Progress는 `BurnStatus`에서 유지한다.
- Runtime이 Circle Visual Scale을 Diameter에 맞게 설정한다.

### Fire Mastery Runtime
- 별도 MonoBehaviour 없이 `SkillLoadout.GetSkillLevel("fire-mastery")`를 조회한다.
- Lv.0/Lv.1/Lv.2 Burn Bonus는 각각 `0/+1/+3`이다.
- Fireball Direct Damage에는 적용하지 않고 Fireball/Fire Zone이 적용하는 Burn Tick Damage에만 적용한다.
- Burn 최종 Damage는 Base Burn + Fire Mastery Bonus + Arcane Synergy Damage + PlayerMagicPower Bonus다.

### Stagger Runtime
- `StaggerStatus`는 Slime Root에 하나만 부착하는 Lightning 전용 상태 Component다.
- 기본 Duration은 Lightning 공격이 전달하는 `0.1`이며 Lightning 2에서는 최소 `0.15`다. 재적용은 현재 Remaining Duration과 새 Duration 중 더 긴 값을 사용한다.
- `SlimeController`는 Stagger 중 Update의 이동, Separation, 공격과 Attack Cooldown 진행을 모두 중단한다.
- Pause 중 Stagger Timer도 진행하지 않으며 종료 후 기존 Slime 행동을 재개한다.
- BurnStatus와 독립 Component로 공존하며 Generic Status Framework는 사용하지 않는다.

### Lightning Chain Runtime
- `LightningChainUtility`는 Chain Lightning과 Lightning Orb만 공유하는 작은 Lightning 전용 helper다.
- 최초 Target을 Hit한 뒤 현재 Target 위치에서 Bounce Range 안의 아직 맞지 않은 가장 가까운 살아 있는 Slime을 고른다.
- 하나의 Chain에서 같은 Slime을 중복 Hit하지 않으며 최대 Enemy `20`에서 단순 O(n²) 탐색을 사용한다.
- 각 Hit는 `SlimeController.TakeDamage`와 `StaggerStatus.ApplyStagger`를 사용한다.
- Lightning 4는 최초 Hit Index `0`부터 `+0/+1/+2...` Damage를 적용하고 Lightning 6은 Chain과 Orb Pulse에 Bounce `+2`를 적용한다.
- Lightning Arc VFX, Generic Ability/Damage Framework와 Spatial Partition은 사용하지 않는다.

### Chain Lightning Runtime
- `chain-lightning`이 SkillLoadout에 Lv.1 이상 장착된 경우 Player 기준 최근접 Slime부터 즉시 자동 공격한다.
- Base Damage는 Lv.1 `1`, Lv.2 `2`이며 PlayerMagicPower Bonus를 모든 Hit에 적용한다.
- Cooldown `1.1`, Base Bounce Count `2`, Bounce Range `5.5`, Stagger Duration `0.1`을 사용한다.
- Base Bounce Count `2`는 최초 Target 외 추가 두 명, 즉 Mastery가 없을 때 최대 세 Target Hit를 의미한다.
- Lightning Mastery는 Damage를 바꾸지 않고 Bounce Count만 증가시킨다.
- Projectile과 Collision Object는 생성하지 않는다.

### Lightning Orb Runtime
- `lightning-orb`가 SkillLoadout에 Lv.1 이상 장착된 경우 Cast 순간 최근접 Slime 방향으로 non-homing 직선 Orb를 발사한다.
- Orb는 Speed `2.2`, Lifetime `6`으로 이동하며 Enemy Collision로 Destroy되지 않는다.
- `0.75`초마다 Orb 기준 Radius `4.5` 안의 최근접 살아 있는 Slime을 최초 Target으로 Lightning Chain을 실행한다.
- Damage `1`, Cooldown `3`, Bounce Range `5.5`, Stagger Duration `0.1`을 사용한다.
- Orb Lv.1/Lv.2 Base Bounce는 `0/1`이며 Lightning Mastery Bonus를 더한다.
- 각 Pulse는 현재 SkillLoadout의 Orb/Mastery Level과 현재 PlayerMagicPower를 다시 읽어 이미 존재하는 Orb에도 Upgrade를 반영한다.
- Pause 중 이동, Lifetime과 Pulse Timer를 모두 정지한다.

### Lightning Mastery Runtime
- 별도 MonoBehaviour 없이 `SkillLoadout.GetSkillLevel("lightning-mastery")`를 조회한다.
- Lv.0/Lv.1/Lv.2 Bounce Bonus는 각각 `0/+1/+2`다.
- Chain Lightning과 Lightning Orb의 모든 Chain에 적용하지만 Damage에는 영향을 주지 않는다.
- Lightning Mastery Bounce와 Lightning 6 Bounce는 합산하며 Lightning 4 Hit Scaling은 Chain Lightning과 Orb Pulse 모두에 적용한다.

### Frost Slow Runtime
- `SlowStatus`는 Slime Root에 하나만 부착하는 Frost 전용 상태 Component다.
- 재적용 시 Duration과 현재 전달된 Move/Attack Speed Multiplier를 갱신하지만 활성 Slow의 경과 시간은 Reset하지 않는다.
- 기본 Slow는 Duration `2.5`, Move Multiplier `0.7`, Attack Speed Multiplier `1`이다.
- Frost Mastery Lv.1은 Attack Speed Multiplier를 `0.65`, Lv.2는 추가로 Move Multiplier를 `0.5`로 만든다.
- `SlimeController`는 직렬화 Move Speed `2.6`과 Attack Cooldown `1.2`를 바꾸지 않고 최종 이동 거리와 Cooldown 진행률에만 Slow 배율을 곱한다.
- Stagger 중에는 기존처럼 전체 Slime 행동과 Cooldown 진행이 먼저 정지하며 Slow/Burn/Stagger는 서로 제거하지 않는다.
- Frost 2/4는 Slow Duration을 `4/5.5`초로 늘리고 Frost 6은 Slow 경과 시간당 Move Multiplier를 `0.06`씩 줄이되 `0.25` 아래로 내리지 않는다.
- Pause 중 Slow Duration과 경과 시간은 진행하지 않고 완전 종료 시 경과 시간을 Reset하며 두 Multiplier는 자동으로 `1`로 돌아온다.
- `FrostSlowUtility`는 Ice Bolt와 Blizzard만 공유하는 작은 Frost 전용 Mastery 계산 helper이며 별도 상태 Counter나 Generic Modifier Framework를 만들지 않는다.

### Ice Bolt Runtime
- `ice-bolt`가 SkillLoadout에 Lv.1 이상 장착된 경우 XZ 최근접 살아 있는 Slime 방향으로 non-homing 직선 Projectile을 자동 발사한다.
- Base Damage `1`, Cooldown `0.85`, Speed `8`, Lifetime `4`, Collision Radius `0.2`를 사용한다.
- 이전→다음 XZ Segment에서 첫 충돌 Enemy를 찾으며 Lv.1은 그 Enemy 한 명에게 Damage와 Slow를 적용하고 Projectile을 Destroy한다.
- Lv.2는 Impact Position Radius `1.8` 안의 살아 있는 Enemy 모두에게 Damage와 Slow를 적용하되 직접 Target을 포함해 Enemy별 한 번만 처리한다.
- Lifetime 동안 Hit가 없으면 AoE 없이 Destroy한다.
- Hit 시 현재 PlayerMagicPower와 Frost Mastery Level을 읽으며 Rigidbody, Collider와 Generic Projectile Framework를 사용하지 않는다.

### Blizzard Runtime
- `blizzard`가 SkillLoadout에 Lv.1 이상 장착된 경우 Player 기준 최근접 살아 있는 Slime의 Cast 시점 위치에 고정 Area를 생성한다.
- Base Damage `1`, Cooldown `4.5`, Duration `4`, Tick Interval `1`을 사용한다.
- Radius는 Lv.1 `2.4`, Lv.2 `3.6`이며 Runtime이 Visual Diameter를 각각 `4.8/7.2`로 설정한다.
- 생성 즉시 반복 Hit하지 않고 1초마다 범위 내 살아 있는 Enemy를 한 번씩 Damage하고 Slow를 갱신한다.
- Damage는 Tick 시점 PlayerMagicPower, Slow는 적용 시점 Frost Mastery Level을 사용한다.
- Area는 Enemy를 따라가지 않으며 Pause 중 Duration과 Tick Timer를 모두 정지한다.
- Frost 2/4/6은 Ice Bolt와 Blizzard가 적용하는 모든 Slow에 적용하며 Frost VFX는 아직 없다.

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
- Player Movement, Slime AI/Attack, Enemy Spawn, Magic Missile Runtime, Projectile, Orb Pickup은 `Time.timeScale = 0`일 때 Update Gameplay를 처리하지 않는다.
- Starting Spell Selection은 게임 시작 `Awake`에서 `Time.timeScale = 0`을 소유하고, 유효한 1회 선택 성공 후에만 `1`로 복구한다.
- 별도 Pause Manager는 만들지 않으며, Starting Selection 중에는 XP 획득과 Level-Up Pause가 발생하지 않는 정상 Gameplay 경로를 사용한다.

### Level-Up Choice UI
- 기존 uGUI `2.0.0`의 Canvas, Button, Legacy Text를 사용하고 새 Package를 설치하지 않는다.
- `LevelUpChoiceUI`는 Panel 하나, 서로 다른 Button 세 개와 Label 세 개만 관리한다.
- Label은 Common/School/Type/Skill 이름, 현재 Level, 다음 Level과 짧은 기능 설명을 세 줄로 표시한다.
- Level Up 발생 시 Controller가 Pause와 Pending 누적 후 UI를 표시한다.
- Button 클릭 시 해당 화면의 세 Button을 즉시 잠그고 선택 효과를 적용한 뒤 Pending을 정확히 하나만 감소시킨다.
- Pending이 남으면 `Time.timeScale = 0`을 유지하고 현재 Loadout 기준 Candidate Pool을 다시 만든 뒤 같은 Panel에 새로운 세 Choice를 즉시 표시한다.
- Pending이 `0`일 때만 Panel을 숨기고 Gameplay를 Resume한다.
- UI가 숨겨진 상태의 Choice, 범위 밖 Choice Index, 중복 Button Reference는 안전하게 거부한다.
- Candidate Pool은 항상 포함되는 Common 3종과 `SkillLoadout.CanAcquireOrUpgrade`를 통과한 Catalog School Skill로 구성한다.
- Candidate 전체를 Unity Random 기반 Fisher–Yates 방식으로 섞고 앞의 세 개만 사용하므로 한 화면의 중복 Choice가 없다.
- School Skill 선택은 `SkillLoadout.AcquireOrUpgrade(SkillDefinition)`을 사용하고 Slot/Level 규칙을 Level-Up System에서 복제하지 않는다.
- 기존 `LevelUpController`와 `SkillLoadout`이 같은 GameSystems에 있으므로 `GetComponent`로 연결하며 새 Inspector Reference가 없다.

### Common Upgrade
- Common Upgrade는 Slot과 School Point를 사용하지 않으며 반복 선택할 수 있다.
- Maximum Health, Magic Power, Regeneration Level은 각각 `0`으로 시작하고 선택 시 `+1`된다.
- Maximum Health는 Maximum HP와 Current HP를 각각 `+10`하며 Current HP는 Maximum HP를 넘지 않는다.
- Magic Power는 별도 `PlayerMagicPower`의 Magic Damage Bonus를 `+1`한다.
- Magic Missile Runtime은 Inspector Damage `3`을 Base Damage로 유지하고 발사 시 Bonus를 더한 최종 Damage를 Projectile에 전달한다.
- Regeneration은 PlayerHealth가 보유하며 선택당 `+0.5 HP/sec`다. Player HP가 `0`보다 크고 Maximum 미만일 때만 `Time.deltaTime`으로 회복한다.
- Common 3종은 Slot 상태와 무관하게 Level-Up Candidate Pool에 항상 들어가며 기존 Level과 효과 적용 경로를 재사용한다.
- Maximum Level, weighted RNG, rarity, reroll, ScriptableObject Upgrade Database, 범용 Damage/Ability Framework는 구현하지 않는다.

### Skill Loadout Core
- School Skill Slot은 Active Slot 1, Active Slot 2, Passive Slot 1로 고정한다.
- 게임 시작 시 세 Slot은 빈 Skill ID와 Level `0`으로 초기화한다.
- 새 Active는 Active 1부터 채우고 다음 Active를 Active 2에 넣으며, 두 Slot이 차면 새로운 Active를 거부한다.
- Passive는 하나만 장착하며 다른 Passive가 있으면 새로운 Passive를 거부한다.
- 이미 장착된 같은 Skill ID는 같은 SkillType일 때만 Level을 올리고 Maximum Level `2`에서 거부한다.
- 같은 Skill ID를 두 Slot에 중복 장착하지 않는다.
- Common Upgrade는 Slot을 사용하지 않으며 SkillLoadout에 넣지 않는다.
- U7-A Debug Placeholder ID는 규칙 검증용 상태일 뿐 실제 Skill Content가 아니다.
- `GetSkillLevel(string)`은 외부 Runtime이 직렬화 Slot Field를 직접 건드리지 않고 현재 장착 Level을 조회하는 최소 read-only API다.
- Magic Missile은 자동 장착하지 않으며, `MagicMissileCaster`는 Loadout의 `magic-missile` Level이 `1` 이상일 때만 동작한다.
- Level-Up 후보 판정과 적용도 동일한 `CanAcquireOrUpgrade`/`AcquireOrUpgrade` 공개 API를 사용한다.

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

현재 규칙:
Active 1 Empty / Active 2 Empty / Passive Empty
→ 8 Active 중 하나 선택
→ 선택한 Spell Lv.1을 Active 1에 장착

- Starting Choice는 별도 Content Database가 아니라 `SkillCatalog`의 Active Definition 8개에서 만든다.
- 게임 시작 즉시 `Time.timeScale = 0`, `Awaiting Selection = true` 상태가 된다.
- 선택은 `TrySelectStartingSpell(string)`을 통해 정확히 한 번만 성공한다.
- Passive Mastery, Common Upgrade, Unknown ID와 비어 있지 않은 시작 Loadout은 거부한다.
- 선택 성공 후 `Awaiting Selection = false`, `Time.timeScale = 1`로 Gameplay를 시작한다.
- Magic Missile도 여덟 선택지 중 하나일 뿐이며 자동 지급하지 않는다.
- `MagicMissileCaster`는 실제 `magic-missile` 장착 여부로 활성화된다.
- 다른 일곱 Active는 선택과 Loadout/Resume만 지원하며 실제 공격은 아직 구현하지 않는다.
- Starting UI는 기존 Canvas 아래의 별도 `StartingSpellPanel`을 사용하고 기존 `LevelUpPanel`과 상태를 공유하지 않는다.
- `StartingSpellSelectionUI`는 고정 Button/Text 8쌍의 표시와 클릭만 관리하며 SkillLoadout을 직접 수정하지 않는다.
- Choice 순서와 Label은 Controller가 제공하는 Catalog Active 8개의 `DisplayName`을 사용한다.
- UI 클릭 흐름은 `StartingSpellSelectionUI → StartingSpellSelectionController → SkillLoadout`이다.
- 시작 시 UI를 표시하고 선택 성공 시 숨기며, 실패 시 Pause/UI를 유지하고 Button을 다시 활성화한다.
- Scene의 기존 Canvas, Canvas Scaler `1920x1080`, EventSystem과 Input System UI Input Module을 재사용한다.
- Dynamic Prefab List, Generic Menu Builder와 새 Canvas/EventSystem을 만들지 않는다.

### School Point
- School Point의 Source of Truth는 현재 `SkillLoadout`의 세 Slot뿐이다.
- `GetSchoolPoints(SkillSchool)` 호출 시 Active 1/2와 Passive 1을 다시 조회한다.
- 각 Slot의 Catalog Definition School이 요청 School과 같으면 Current Level을 합산한다.
- Skill Level 1당 Point 1이며 현재 Active 2 + Passive 1, 각 Max Lv.2 규칙으로 School별 최대는 자연스럽게 `6`이다.
- Empty, Level `0`, Catalog에 없는 Debug Placeholder Skill은 Point를 제공하지 않는다.
- Loadout Reset 뒤 별도 Point Reset 없이 계산 결과가 자동으로 모두 `0`이 된다.
- 별도 Point Field, 누적 Counter, Manager를 만들지 않는다.
- 2/4/6 Synergy는 이 실시간 Point API에서 직접 파생하며 활성 상태를 별도로 저장하지 않는다.

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
- 저장된 `player` Root는 `PlayerMovement`, `PlayerHealth`, `PlayerExperience`, `PlayerMagicPower`와 8개 Active의 Caster Component를 사용하며 Position `(0, 0, 0)`, Rotation `(0, 0, 0)`이다.
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
- `player`의 `MagicMissileCaster`에는 실제 Magic Missile Prefab, `EnemySpawner`, `PlayerMagicPower`, `GameSystems`의 `SkillLoadout`이 연결되어 있으며 U9-A Loadout gating과 기존 Base Damage + Bonus 적용을 검증했다.
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
- `GameSystems`의 `StartingSpellSelectionController`에는 같은 Object의 `SkillLoadout`과 Canvas의 `StartingSpellSelectionUI`가 연결되어 있다.
- 기존 Canvas에는 `StartingSpellSelectionUI`, Inactive 저장된 별도 `StartingSpellPanel`, Title과 서로 다른 Button/Label 8쌍이 연결되어 있다.
- U9-B Starting UI 표시/선택/Close/Resume와 Level-Up UI 분리는 Unity Editor에서 검증 완료됐다.
- `player`에는 `MagicBoltCaster`와 `MagicBolt.prefab`, EnemySpawner, PlayerMagicPower, SkillLoadout Reference 및 요청된 기본 수치가 저장되어 있고 U10-A Editor 검증이 완료됐다.
- `player`에는 `FireballCaster`와 `FireZoneCaster`, 각 Prefab/EnemySpawner/PlayerMagicPower/SkillLoadout Reference와 요청된 수치가 저장되어 있고 U10-Fire Editor 검증이 완료됐다.
- `player`에는 `ChainLightningCaster`와 `LightningOrbCaster`, 각 Prefab/EnemySpawner/PlayerMagicPower/SkillLoadout Reference와 요청된 수치가 저장되어 있고 U10-Lightning Editor 검증이 완료됐다.
- `player`에는 `IceBoltCaster`와 `BlizzardCaster`, 각 Prefab/EnemySpawner/PlayerMagicPower/SkillLoadout Reference와 요청된 수치가 저장되어 있고 U10-Frost Editor 검증이 완료됐다.
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
- Slime Root에는 `BurnStatus`가 추가되어 있고 U10-Fire Editor 검증이 완료됐다.
- Slime Root에는 `StaggerStatus`가 추가되어 있고 U10-Lightning Editor 검증이 완료됐다.
- Slime Root에는 `SlowStatus`가 추가되어 있고 U10-Frost Editor 검증이 완료됐다.
- U3-B Separation은 Script 기본값 Radius `0.75`, Strength `0.35`로 Editor 검증 완료됐다.
- Magic Missile 실제 경로: `Assets/Prefabs/Projectiles/MagicMissile.prefab`
- Magic Missile Root에는 `MagicMissileProjectile`, Visual Child에는 Circle `SpriteRenderer`와 `BillboardToCamera`가 있다.
- Magic Bolt 실제 경로: `Assets/Prefabs/Projectiles/MagicBolt.prefab`
- Magic Bolt Root에는 `MagicBoltProjectile`, Visual Child에는 SpriteRenderer와 `BillboardToCamera`가 있으며 Player Caster 연결과 Play Mode 검증이 완료됐다.
- Fireball 실제 경로: `Assets/Prefabs/Projectiles/Fireball.prefab`
- Fireball Root에는 `FireballProjectile`, Visual Child에는 Circle SpriteRenderer와 `BillboardToCamera`가 있으며 Player Caster 연결과 Play Mode 검증이 완료됐다.
- Fire Zone 실제 경로: `Assets/Prefabs/Areas/FireZone.prefab`
- Fire Zone Root에는 `FireZoneArea`, XZ Ground 방향의 반투명 Circle Visual이 있으며 Player Caster 연결과 Play Mode 검증이 완료됐다.
- Lightning Orb 실제 경로: `Assets/Prefabs/Projectiles/LightningOrb.prefab`
- Lightning Orb Root에는 `LightningOrbProjectile`, Visual Child에는 SpriteRenderer와 `BillboardToCamera`가 있으며 U10-Lightning Editor 검증이 완료됐다.
- Ice Bolt 실제 경로: `Assets/Prefabs/Projectiles/IceBolt.prefab`
- Ice Bolt Root에는 `IceBoltProjectile`, Visual Child에는 SpriteRenderer와 `BillboardToCamera`가 있으며 U10-Frost Editor 검증이 완료됐다.
- Blizzard 실제 경로: `Assets/Prefabs/Areas/Blizzard.prefab`
- Blizzard Root에는 `BlizzardArea`, XZ Ground 방향의 반투명 Circle Visual이 있으며 U10-Frost Editor 검증이 완료됐다.
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
  - 같은 Root의 `StaggerStatus`를 캐시하고 Stagger 중 이동/Separation/Attack/Cooldown 정지
  - 같은 Root의 `SlowStatus`를 캐시하고 최종 이동 거리와 Attack Cooldown 진행률에 현재 Slow Multiplier 적용
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
  - Magic Missile Runtime Targeting용 `SpawnedEnemies` 읽기 전용 목록과 Billboard Camera 제공
  - 필수 Reference Null 방어
  - U3-A Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/Combat/MagicMissileCaster.cs`
  - Player의 기존 Magic Missile Runtime Component
  - `SkillLoadout` read-only 조회로 `magic-missile` Lv.1 이상 장착 여부 확인
  - Magic Missile 미장착 또는 Gameplay Pause 중에는 Target 검색과 발사를 수행하지 않음
  - EnemySpawner 목록에서 필요한 수만큼 XZ 최근접 살아 있는 Slime을 서로 다르게 우선 선택하고 부족하면 Target 재사용
  - Lv.1/Lv.2 Base Damage `3/4`, 기본 Projectile Count `1/2`에 Arcane 2/6 Bonus 적용
  - Lv.2는 서로 다른 최근접 Target을 우선하고 Enemy가 하나면 같은 Target 사용
  - Lv.2 Launch Spacing `0.32`
  - 각 Projectile에 `PlayerMagicPower` Bonus 합산
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
- `Assets/Scripts/Combat/MagicBoltCaster.cs`
  - `magic-bolt` Loadout Lv.1 이상 gating
  - 필요한 수만큼 XZ 최근접 살아 있는 Slime을 선택하고 Projectile별 발사 순간 고정 Direction 계산
  - Lv.1/Lv.2 Base Damage `4/5`, 기본 Projectile Count `1/2`에 Arcane 2/6 Bonus 적용
  - Lv.2는 서로 다른 최근접 Target을 우선하고 Enemy가 하나면 같은 방향 Target 사용
  - Lv.2 Launch Spacing `0.22`
  - 각 Projectile에 PlayerMagicPower Bonus 적용
  - Cooldown `0.9`, Speed `9`, Lifetime `4`, Collision Radius `0.2`
  - Enemy 없음/Gameplay Pause에서 발사와 Cooldown 소비 없음
  - 필수 Prefab/Spawner/MagicPower/Loadout/Billboard Camera Reference 방어
- `Assets/Scripts/Combat/MagicBoltProjectile.cs`
  - Target Reference 없는 XZ non-homing 직선 이동
  - 이전→다음 Position Segment와 살아 있는 Slime의 XZ Collision 검사
  - 가장 이른 첫 Hit 하나에 Damage 후 Root Destroy
  - 원래 Target Death와 무관하게 Lifetime까지 진행
  - Runtime Billboard Camera 연결과 고정 Visual Height
  - Pause 중 이동/Lifetime 정지, Invalid Setup 방어
- `Assets/Scripts/Combat/BurnStatus.cs`
  - Slime별 단일 Burning 상태와 Inspector Runtime 값
  - 재적용 시 현재 Loadout의 Fire/Arcane/Mastery/Magic Power로 Duration/Damage/Interval 갱신, Tick Progress 유지
  - `SlimeController.TakeDamage`를 사용하는 `1`초 Tick과 Pause 정지
  - Fire 6에서 반경 `3.2`, 간격 `1`초의 비-Burning Enemy 전염
- `Assets/Scripts/Combat/FireballCaster.cs`
  - `fireball` Loadout gating과 XZ 최근접 Target 방향 선택
  - Direct Arcane/Magic Power와 Burn Runtime 계산 Reference 전달
  - 요청된 Fireball 수치, Lv.1/Lv.2 Explosion Radius와 Arcane 추가 Projectile 적용
- `Assets/Scripts/Combat/FireballProjectile.cs`
  - Target Reference 없는 non-homing XZ 직선 이동과 Segment Collision
  - 첫 Enemy Direct Damage 후 Impact Radius 내 Burn 적용
  - Runtime Billboard Camera, Lifetime, Pause와 Invalid Setup 방어
- `Assets/Scripts/Combat/FireZoneCaster.cs`
  - `fire-zone` Loadout gating과 최근접 Enemy 현재 위치에 고정 Zone 생성
  - 요청된 Zone/Burn 수치, Lv.1/Lv.2 Radius와 현재 Build 계산 Reference 전달
- `Assets/Scripts/Combat/FireZoneArea.cs`
  - 고정 XZ Area, Duration `4`, Apply Interval `0.5`
  - 범위 내 살아 있는 Slime Burn 반복 갱신과 Radius 기반 Visual Scale
  - Pause 중 Timer 정지와 Direct Damage 없음
- `Assets/Scripts/Combat/StaggerStatus.cs`
  - Slime별 단일 Stagger 상태와 Inspector Runtime 값
  - 더 긴 Remaining Duration 우선 Refresh와 Pause 중 Timer 정지
- `Assets/Scripts/Combat/LightningChainUtility.cs`
  - 최초 Target과 Bounce Range 내 최근접 미타격 Slime Chain 처리
  - 같은 Chain 중복 Hit 방지와 각 Hit Damage/Stagger 및 Lightning 2/4/6 적용
- `Assets/Scripts/Combat/ChainLightningCaster.cs`
  - `chain-lightning` Loadout gating과 Player 기준 최근접 최초 Target
  - Lv.1/Lv.2 Damage `1/2`, Cooldown `1.1`, Base Bounce `2`, Range `5.5`
  - PlayerMagicPower와 Lightning Mastery Bounce 적용
- `Assets/Scripts/Combat/LightningOrbCaster.cs`
  - `lightning-orb` Loadout gating과 Cast 순간 XZ 고정 방향 선택
  - Damage `1`, Cooldown `3`, Speed `2.2`, Lifetime `6`, Pulse `0.75/4.5`
  - Orb Prefab과 Runtime Reference/수치 전달
- `Assets/Scripts/Combat/LightningOrbProjectile.cs`
  - Target Reference와 Collision Destroy 없는 non-homing 직선 이동
  - Pulse마다 반경 내 최근접 Target부터 Lightning Chain 실행
  - 현재 Orb/Mastery Level과 PlayerMagicPower 재조회
  - Runtime Billboard Camera와 Pause 중 이동/Lifetime/Pulse 정지
- `Assets/Scripts/Combat/SlowStatus.cs`
  - Slime별 단일 Slow 상태, Remaining Duration과 Move/Attack Speed Multiplier Inspector 표시
  - 재적용 시 Duration/Multiplier 갱신과 활성 경과 시간 유지, 종료 시 Multiplier `1`/경과 시간 복구
  - Frost 6 Progressive Move Slow `0.06/sec`, 최소 Multiplier `0.25`, Pause 중 Timer 정지
- `Assets/Scripts/Combat/FrostSlowUtility.cs`
  - 현재 `frost-mastery` Level로 공통 Move/Attack Speed Multiplier를 선택해 `SlowStatus`에 전달
  - Ice Bolt와 Blizzard만 공유하는 Frost 전용 helper
- `Assets/Scripts/Combat/IceBoltCaster.cs`
  - `ice-bolt` Loadout gating과 XZ 최근접 Target 발사 방향 선택
  - Damage `1`, Cooldown `0.85`, Speed `8`, Lifetime `4`, Collision Radius `0.2`, Lv.2 AoE Radius `1.8`
  - Projectile Prefab과 EnemySpawner/PlayerMagicPower/SkillLoadout/Billboard Camera Runtime 전달
- `Assets/Scripts/Combat/IceBoltProjectile.cs`
  - non-homing XZ Segment Collision과 첫 Hit 처리
  - Lv.1 Direct Damage/Slow, Lv.2 Impact AoE의 Enemy별 단일 Damage/Slow
  - Miss 시 AoE 없음, Runtime Billboard Camera와 Pause 중 이동/Lifetime 정지
- `Assets/Scripts/Combat/BlizzardCaster.cs`
  - `blizzard` Loadout gating과 최근접 Enemy 현재 위치에 고정 Area 생성
  - Damage `1`, Cooldown `4.5`, Duration `4`, Tick `1`, Lv.1/Lv.2 Radius `2.4/3.6`
- `Assets/Scripts/Combat/BlizzardArea.cs`
  - 고정 XZ Area와 1초 간격 범위 Damage/Slow
  - PlayerMagicPower/Frost Mastery Runtime 적용, Radius 기반 Visual Diameter와 Pause 정지
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
  - 같은 GameSystems의 `SkillLoadout`을 `GetComponent`로 재사용
  - Common 3종과 `CanAcquireOrUpgrade`가 true인 School Skill의 통합 Candidate Pool 생성
  - Unity Random Fisher–Yates Shuffle 후 중복 없는 세 Choice를 `LevelUpChoiceUI`에 표시
  - Common은 `CommonUpgradeController`, School Skill은 `SkillLoadout.AcquireOrUpgrade`로 적용
  - Pending이 남으면 현재 Loadout에서 새 Pool/Choice 생성, 마지막 Pending에서 UI 숨김과 Resume
  - School Skill별 Lv.1/Lv.2 짧은 기능 설명 Label 생성
  - Debug Context Menu로 Pending 하나씩 완료
  - Pending `0`에서 `Time.timeScale = 1` Resume
  - Disable/Destroy UI 숨김/Pause 복구와 필수 Reference Null 방어
- `Assets/Scripts/Progression/StartingSpellSelectionController.cs`
  - 게임 시작 Pause와 `Awaiting Selection` 상태 관리
  - SkillCatalog Active Definition 8개를 read-only Starting Choice로 제공
  - Empty 시작 Loadout 검증과 `TrySelectStartingSpell(string)` 1회 선택 API
  - `StartingSpellSelectionUI` 초기화와 시작 UI 표시
  - 선택 Active를 Active Slot 1 Lv.1로 장착하고 Starting UI를 숨긴 뒤 Gameplay Resume
  - Passive/Unknown/중복 선택과 비정상 시작 Loadout 안전 거부
  - 8개 Active Debug Context Menu 제공
  - U9-A Core와 U9-B UI 연결 Editor 검증 완료
- `Assets/Scripts/UI/StartingSpellSelectionUI.cs`
  - 기존 Canvas에 부착하는 Starting Spell 전용 uGUI Component
  - Starting Panel과 서로 다른 Button/Legacy Text Reference 8쌍 검증
  - Controller의 Active 8개 Definition을 받아 `DisplayName` Label 적용
  - 선택 중 Button 잠금, 성공 시 Hide, 실패 시 재활성화
  - SkillLoadout 직접 접근 없음
  - U9-B Editor 연결 및 Play Mode 검증 완료
- `Assets/Scripts/UI/LevelUpChoiceUI.cs`
  - Panel 한 개와 서로 다른 uGUI Button/Text Reference 세 쌍 관리
  - 시작 시 Panel 숨김
  - Controller가 제공하는 Common 또는 School Skill의 현재/다음 Level과 효과 Label 표시
  - Button 클릭 잠금과 UI 미표시 상태 중복 처리 방어
  - Controller에 Choice Index 전달
- `Assets/Scripts/Progression/CommonUpgradeController.cs`
  - PlayerHealth와 PlayerMagicPower Reference 보유
  - Maximum Health, Magic Power, Regeneration 선택 Level을 각각 Inspector에 표시
  - Common 3종 효과 적용과 `Common · 이름` 형식의 현재 Level 기반 Label 생성
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
- `Assets/Scripts/Combat/ProjectileTargetingUtility.cs`
  - Arcane 추가 Projectile 대상인 다섯 Caster가 공유하는 최소 최근접 Target 선택 helper
  - 서로 다른 살아 있는 Enemy 우선, Target 부족 시 기존 Target 재사용
- `Assets/Scripts/Skills/SkillLoadout.cs`
  - `SkillType.Active/Passive` 최소 enum
  - Active Slot 1/2와 Passive Slot 1의 private 직렬화 Skill ID/Level 상태
  - 시작 Empty/Level `0`, School Skill Maximum Level `2`
  - `CanAcquireOrUpgrade`와 `AcquireOrUpgrade`의 슬롯/Type/Level 규칙
  - Null/공백 ID, Type 불일치, 중복 Slot, Level Cap 방어
  - 실제 콘텐츠가 아닌 Debug Placeholder Context Menu와 Reset
  - `SkillDefinition` 기반 Can/Acquire 오버로드와 Definition MaxLevel 적용
  - Magic Missile/Magic Bolt/Fireball/Fire Zone/Chain Lightning/Lightning Orb/Ice Bolt/Blizzard와 Arcane/Fire/Lightning/Frost Mastery Definition Debug Context Menu
  - Strict Empty 상태 조회와 `GetSkillLevel(string)` read-only Runtime API
  - 현재 세 Slot을 조회하는 `GetSchoolPoints(SkillSchool)` 실시간 계산 API
  - Arcane Mastery Lv.0/Lv.1/Lv.2의 `1/0.9/0.85`를 반환하는 read-only Spell Cooldown 계산 API
  - Invalid/0 이하 Base Cooldown 안전 처리와 8 Active Caster의 다음 Cast Cooldown 연동
  - Empty/Level 0/Unknown Debug ID 제외와 Invalid School 안전한 `0`
  - `Debug Log School Points` Context Menu
  - `Debug Log Active Synergies` Context Menu
  - Starting Selection과 Magic Missile Runtime이 공개 API만 사용하며 직렬화 Slot Field에 직접 접근하지 않음
- `Assets/Scripts/Skills/SchoolSynergyUtility.cs`
  - 현재 `SkillLoadout.GetSchoolPoints`만 조회하는 Arcane/Fire/Lightning/Frost 2/4/6 계산 helper
  - 별도 mutable Point State나 MonoBehaviour 없이 Projectile/Damage/Burn/Chain/Slow 보정값 제공
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
- U12의 Arcane/Fire/Lightning/Frost 2/4/6 Runtime 효과는 Play Mode 검증 전이다.
- Fireball Lv.2는 Explosion Radius만, Fire Zone Lv.2는 Radius만 증가하며 요청 범위 외 추가 효과는 없다.
- Chain Lightning Arc와 Stagger VFX는 아직 없으며 Inspector/행동으로 검증해야 한다.
- Frost Impact, Slow 표시와 Blizzard Snow Particle VFX는 아직 없다.
- U9-A Debug Context Menu는 개발용 fallback으로 남아 있다.
- Skill Definition은 Metadata뿐이며 실제 Spell/Passive 효과를 실행하지 않는다.
- U7-A Debug Placeholder Skill ID는 슬롯 규칙 검증용이며 Catalog의 실제 Skill이 아니다.
- Synergy 효과 UI/HUD와 전용 VFX는 아직 구현되지 않았다.
- Player Death와 HP UI는 구현되지 않았다.
- `ProjectSettings.asset`의 프로젝트 템플릿 메타데이터에는 `templateDefaultScene: Assets/Scenes/SampleScene.unity`가 남아 있다. 실제 Build Scene과 활성 Scene은 모두 `Main.unity`를 사용한다.
- Git commit / push는 현재 작업 범위에서 의도적으로 수행하지 않았다.

## Deferred Work
- Spawn Difficulty Scaling, Wave, Spawn Interval 감소
- Enemy Manager Framework와 Object Pool
- Physics2D 활용 범위
- ScriptableObject 활용 범위
- Common Upgrade 최대 Level
- 다중 Lightning Source에서 Stagger anti-permastun이 필요한지 여부
- 최종 Balance
- Synergy UI/HUD와 전용 VFX
- XP Magnet, Pickup Attraction, XP Merge, Loot System, Object Pool

위 항목은 해당 Phase에서 실제 필요가 생길 때 결정한다.

## Next Phase
**Pending U12 Editor Verification**

Unity Editor에서 새 Component나 Reference 없이 네 School의 2/4/6 누적 효과, 기존 Mastery/Common Upgrade 결합, Pause와 기존 U1~U11 Regression을 검증한다.

검증 성공 후 다음 Phase로 `U13 — Prototype Demo Finish Pass`를 제안한다. 이번 작업에서는 Timer/Boss/HUD/VFX/Spawn·Balance 조정과 Demo 완료 판정을 구현하지 않는다.
