# Arcane Survivor — Game Specification

## 1. 프로젝트 목표
Arcane Survivor는 자동 공격 기반의 탑다운 생존 액션 게임이다.

플레이어는 직접 이동하고 마법은 자동으로 시전된다.

핵심 재미는 단순히 적을 많이 죽이는 것이 아니라 제한된 스킬 슬롯 안에서 학파를 조합해 빌드를 완성하는 데 있다.

현재 목표는 상용 본개발 확정이 아니라 **Unity에서 플레이 가능한 Prototype Demo를 완성하고 게임의 재미를 검증하는 것**이다.

## 2. 핵심 게임 루프
이동 → 적 회피 → 마법 자동 공격 → 적 처치 → XP Orb 드롭 → 직접 수집 → Level Up → 3개 강화 중 하나 선택 → Build 강화 → 더 많은 적과 전투

플레이어는 공격 버튼을 사용하지 않는다. 모든 Active Spell은 자동으로 시전된다.

## 3. Player
핵심 능력치:
- Current HP
- Maximum HP
- Move Speed
- Health Regeneration
- Magic Damage Bonus
- Experience
- Level

Player는 WASD로 이동한다. 게임플레이 수치는 Prototype 단계에서는 테스트용 값이며 최종 밸런스가 아니다.

## 4. Enemy
Prototype Demo의 기본 Enemy는 Slime이다.
- Player 주변에서 생성
- Player를 추적
- 일정 거리 이내에서 공격
- HP가 0 이하가 되면 사망
- 사망 위치에 XP Orb 생성

Prototype Demo 단계에서는 Enemy 종류를 불필요하게 늘리지 않는다.

## 5. Experience / Level Up
Enemy가 죽으면 XP Orb를 떨어뜨린다. Player는 Orb 근처까지 직접 이동해 획득한다. 자동 자석 시스템은 현재 필수 기능이 아니다.

경험치가 요구량에 도달하면 Level Up 한다.

Level Up:
게임 Pause → 현재 획득 가능한 Upgrade 후보 계산 → 무작위 3개 표시 → 하나 선택 → 적용 → 게임 재개

한 번의 XP 획득으로 여러 Level을 얻으면 필요한 횟수만큼 Upgrade 선택을 연속 진행한다.

## 6. Skill Slot
School Skill 슬롯:
- Active Slot 1
- Active Slot 2
- Passive Slot 1

각 School Skill의 최대 레벨은 2다. Common Upgrade는 Slot을 사용하지 않는다.

## 7. 시작 마법 선택
게임 시작 상태:
- Active 1: Empty
- Active 2: Empty
- Passive: Empty

게임 시작 시 전투는 Pause 상태다. Player는 아래 8개의 Active Spell 중 하나를 직접 선택한다.

1. Magic Missile
2. Magic Bolt
3. Fireball
4. Fire Zone
5. Chain Lightning
6. Lightning Orb
7. Ice Bolt
8. Blizzard

선택한 Active는 Lv.1로 Active Slot 1에 들어간 뒤 게임이 시작된다.

### Important
Magic Missile을 자동 지급하지 않는다. Magic Missile은 다른 Active Spell과 동등한 시작 선택지다.

과거의 Magic Missile 강제 Starter / 자동 교체 설계는 폐기됐다.

## 8. School
현재 School:
- Arcane
- Fire
- Lightning
- Frost

각 School은 Active 2개 + Passive Mastery 1개를 가진다.

School Skill Level 1당 해당 School Point 1을 제공한다. 한 School의 최대 Point는 6이다.

School Point는 별도 누적 상태보다 현재 Loadout의 Skill Level에서 계산하는 것을 기본 원칙으로 한다.

Synergy Breakpoint:
- 2
- 4
- 6

대표 Build:
- Pure 6
- 4 + 2
- 2 + 2 + 2

## 9. Arcane
정체성:
- 범용성
- Projectile 증가
- 전체 Magic Damage 증가
- 다른 School과 섞기 좋은 보조 School

### Magic Missile
- Active
- 가장 가까운 적을 추적
- Homing Projectile
- 타겟 사망 시 해당 Projectile 처리 필요
- Lv.2에서 Projectile 수와 Damage 강화

### Magic Bolt
- Active
- Non-homing
- 빠른 직선 Projectile
- 가장 가까운 적 방향으로 발사
- Lv.2에서 Projectile 수와 Damage 강화

### Arcane Mastery
모든 Spell Cooldown 감소.

### Arcane Synergy
- Arcane 2: Projectile 기반 Magic의 Projectile Count 증가
- Arcane 4: 모든 Magic Damage 증가
- Arcane 6: Projectile Count와 Magic Damage 추가 증가

Projectile Count 주요 적용 대상:
Magic Missile, Magic Bolt, Fireball, Ice Bolt, Lightning Orb

## 10. Fire
정체성:
- AoE
- Damage over Time
- Burning
- 전염
- 시간이 지날수록 강한 누적 피해

### Fireball
- 직선 Projectile
- 작은 Direct Damage
- Impact 범위에 Burning
- Lv.2에서 Explosion Radius 강화

### Fire Zone
- 적 위치에 Persistent Area 생성
- 직접 Damage 중심이 아님
- 범위 안 Enemy에게 Burning 반복 적용/갱신
- Lv.2에서 Radius 증가

### Fire Mastery
Burning Damage 강화.

### Burning
Enemy마다 하나의 Burn 상태를 가진다. 재적용 시 무제한 Stack을 추가하지 않고 Duration, Tick Interval, Tick Damage를 갱신하는 구조를 사용한다.

### Fire Synergy
- Fire 2: Burn Duration 증가
- Fire 4: Burn Tick Interval 감소
- Fire 6: Burning이 주변의 non-burning Enemy에게 전염

한 번의 전염 처리 안에서 새로 감염된 Enemy가 즉시 또 다른 Source가 되어 무한 Chain을 만드는 구조는 피한다.

## 11. Lightning
정체성:
- Bounce
- 다중 Target
- Stagger
- 적이 많을수록 강해지는 연쇄 공격

### Chain Lightning
- 가장 가까운 Enemy부터 공격
- 주변 Enemy로 Bounce
- 하나의 Chain 안에서 같은 Enemy를 중복 타격하지 않는다

### Lightning Orb
- 느리게 이동하는 Persistent Orb
- 일정 주기로 주변 Enemy에게 Lightning Attack
- Bounce 시스템과 연계

### Lightning Mastery
Lightning Spell의 Bounce 증가.

### Stagger
짧은 시간 Enemy 행동을 완전히 중단시킨다.

### Lightning Synergy
- Lightning 2: Stagger Duration 증가
- Lightning 4: Chain 내 Hit Index가 높을수록 해당 Hit Damage 증가
- Lightning 6: 모든 Lightning Spell의 Bounce 추가 증가

## 12. Frost
정체성:
- Slow
- Enemy Control
- 지속적인 제어
- 오래 노출될수록 강해지는 감속

### Ice Bolt
- 직선 Projectile
- Damage + Slow
- Lv.2에서 Impact 주변 작은 AoE + Slow

### Blizzard
- Persistent AoE
- 주기적으로 Damage + Slow
- Lv.2에서 Radius 증가

### Frost Mastery
Slow가 Enemy Attack Speed에도 영향을 주게 한다. 고레벨에서 Movement Slow도 강화한다.

### Frost Synergy
- Frost 2: Slow Duration 증가
- Frost 4: Slow Duration 추가 증가
- Frost 6: 연속 Slow 노출 시간이 길수록 Movement Slow가 점점 강해짐

최소 Movement Speed Multiplier Cap을 둔다.

## 13. Common Upgrade
Common Upgrade는 Slot을 사용하지 않고 School Point도 제공하지 않는다.

- Maximum Health: Maximum HP 증가, Current HP도 일정량 함께 회복
- Magic Power: 모든 Magic Damage 증가
- Regeneration: 초당 HP Regeneration 증가

정확한 수치와 최대 Level은 Prototype Playtest 후 결정한다.

## 14. Damage / Status 설계 원칙
공통 Build Modifier와 Spell 전용 Modifier를 가능한 한 구분한다.

Burn / Slow / Stagger 같은 Status Effect는 서로 동시에 존재할 수 있어야 한다.

Enemy AI 자체와 Status Effect 구현은 지나치게 강하게 결합하지 않는다.

## 15. Prototype Demo 완료 기준
- WASD Player Movement
- Slime Spawn / Chase / Attack / Death
- XP Orb
- Experience / Level
- Level Up 3-choice UI
- Starting Spell Selection
- Active 2 + Passive 1
- 8 Active Spell
- 4 Passive Mastery
- 3 Common Upgrade
- Arcane / Fire / Lightning / Frost
- 각 School 2/4/6 Synergy
- 5-minute Run Timer
- Gameplay 시간에 따라 증가하는 Enemy pressure
- Boss Slime
- Victory / Defeat
- 간단한 Sprite 표현
- 최소한의 Spell Effect
- 한 판을 실제로 플레이할 수 있음

현재 완료선에 포함하지 않음:
- Meta Progression
- Save
- Account
- Multiplayer
- Mobile
- Gamepad
- 다수의 Enemy Variety
- 상용 수준 Art/Audio
- Steam 출시 준비
- 최종 Balance

현재 Pacing의 Enemy Cap 최대 `100`, Spawn Interval `1.2 → 0.4`, XP Requirement `ceil(8 + 3n + 0.5n²)`는 5분 Demo와 성능을 검증하기 위한 Prototype Playtest 수치이며 최종 Balance가 아니다.

Prototype Demo 완성 후 가장 중요한 질문은:

**다른 Build로 한 판 더 해보고 싶은가?**
