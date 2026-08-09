# Arcane Survivor Unity — Project State

## Current Phase
**Unity Migration Preparation**

Unity 구현은 아직 시작하지 않았다.

Three.js Prototype은 별도 Repository에 보존한다.

Unity 프로젝트의 목표는 상용 본개발 확정이 아니라 **Prototype Demo 제작 및 재미 검증**이다.

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

### U1 — Player
- Player GameObject
- SpriteRenderer
- WASD Movement
- Camera

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

## Current Known Questions
1. 순수 2D Top-down vs 2.5D 표현
2. Physics2D 활용 범위
3. ScriptableObject 활용 범위
4. Common Upgrade 최대 Level
5. Ice Bolt Lv.2 Main Target 중복 Damage 유지 여부
6. Lightning Stagger anti-permastun 필요 여부
7. 최종 Balance

위 항목은 필요해질 때 결정한다.

## Next Action
Unity Project를 새로 생성한다.

프로젝트 생성 직후:
1. Git 초기화
2. Unity `.gitignore` 적용
3. `GAME_SPEC.md`
4. `PROJECT_STATE.md`
5. `MIGRATION_NOTES.md`
6. `AGENTS.md`

를 프로젝트 Root에 추가한다.

그 후 Codex에게 프로젝트를 읽기만 시켜 현재 상태를 확인시킨다.

아직 Gameplay 구현을 한 번에 요청하지 않는다.
