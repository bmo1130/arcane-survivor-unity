# Arcane Survivor Unity — Codex Instructions

## Project Goal
이 프로젝트의 현재 목표는 상용 게임 완성이 아니다.

목표는:

**Unity Prototype Demo를 완성하고 Arcane Survivor의 핵심 Gameplay와 Build System이 실제로 재미있는지 검증하는 것.**

## Required Reading
새 작업을 시작하기 전에 다음 순서로 읽는다.

1. `AGENTS.md`
2. `PROJECT_STATE.md`
3. `GAME_SPEC.md`
4. `MIGRATION_NOTES.md`
5. 현재 작업과 관련된 실제 Unity Source / Scene / Prefab 구조

## Source of Truth
정보가 충돌할 경우 다음 우선순위를 사용한다.

현재 실제 Unity 구현
→ PROJECT_STATE.md의 최근 결정
→ GAME_SPEC.md
→ MIGRATION_NOTES.md의 과거 참고 정보

추측으로 과거 설계를 복구하지 않는다.

## Development Style
- 작은 Phase 단위로 작업한다.
- 한 번에 여러 시스템을 과도하게 구현하지 않는다.
- 현재 기능을 보존하면서 하나의 목표를 완료한다.
- 필요 이상으로 기존 코드를 리팩터링하지 않는다.

## Do Not Prematurely Optimize
실제 성능 문제가 확인되기 전에는 다음 구조를 선행 구현하지 않는다.

- ECS
- Custom Object Pooling Framework
- Spatial Partition
- 복잡한 Event Bus
- 범용 Ability Framework
- 범용 Status Framework
- 과도한 Dependency Injection
- 미래 확장만을 위한 추상화

Unity 기본 기능으로 충분하면 단순한 구현을 선호한다.

## Balance Rule
사용자가 요청하지 않는 한 기존 Gameplay Balance 수치를 임의로 변경하지 않는다.

Balance는 별도 단계에서 Playtest 후 조정한다.

버그 수정과 Balance 변경을 혼동하지 않는다.

## Unity Editor Responsibility
사용자는 C#보다 Playtest / Debug / Game Design에 더 집중한다.

코드를 작성한 뒤 Unity Editor에서 사용자가 직접 해야 하는 작업이 있다면 반드시 정확히 설명한다.

예:
- 어떤 GameObject를 생성해야 하는지
- 어떤 Component를 추가해야 하는지
- 어떤 Script를 붙여야 하는지
- Inspector에서 어떤 Field에 무엇을 연결해야 하는지
- 어떤 Prefab을 만들어야 하는지
- 어떤 Layer / Tag 설정이 필요한지
- Play Mode에서 무엇을 확인해야 하는지

"Unity에서 적절히 설정하세요"처럼 모호하게 설명하지 않는다.

## Code Expectations
사용자가 직접 코드를 작성해야 한다고 가정하지 않는다.

가능한 코드 변경은 직접 구현한다.

사용자에게 코드 수정이 필요한 경우:
- 파일 이름
- 정확한 위치
- 변경 이유

를 명확히 설명한다.

## Gameplay Preservation
사용자가 명시적으로 요청하지 않는 한 다음을 변경하지 않는다.

- Active Slot 2
- Passive Slot 1
- School Skill Max Lv.2
- 4 School 구조
- 2/4/6 Synergy
- 8 Active Starting Selection
- Common Upgrade 구조
- XP 직접 수집
- 자동 Spell Casting

특히 Magic Missile Forced Starter를 다시 추가하지 않는다.

## Unity Migration Principle
기존 Three.js 코드 구조를 기계적으로 복제하지 않는다.

Unity Engine이 제공하는 적절한 기능은 사용한다.

다만 엔진 기능을 사용한다는 이유로 Gameplay Rule을 바꾸지 않는다.

## Verification
각 작업 후 가능한 범위에서 다음을 확인한다.

- C# Compile Error
- Unity Console Error 가능성
- Missing Reference
- Null Reference 가능성
- Prefab Reference
- Scene Reference
- 기존 기능 Regression

자동 검증할 수 없는 Unity Editor 상태는 사용자에게 수동 확인 방법을 알려준다.

## Completion Report
각 Phase 작업 완료 후 다음 형식으로 보고한다.

### Implemented
이번 작업에서 실제 구현한 기능.

### Preserved
기존 기능 중 변경하지 않은 부분.

### Editor Setup
사용자가 Unity Editor에서 직접 해야 할 작업. 없으면 `None`.

### Verification
컴파일/테스트 결과와 수동 확인 사항.

### Changed Files
변경한 파일 목록.

### Known Issues
현재 남아 있는 문제.

### Next Phase
다음으로 진행할 작은 Phase를 제안한다.

사용자 요청 없이 Next Phase까지 구현하지 않는다.

## Project State Maintenance
큰 Phase가 끝날 때마다 실제 구현을 확인한 뒤 `PROJECT_STATE.md`를 갱신한다.

문서에는 다음을 유지한다.

- Current Phase
- Completed Features
- Important Decisions
- Current Scene / Prefab / Script 구조
- Known Issues
- Deferred Work
- Next Phase

폐기된 설계를 현재 규칙처럼 남겨두지 않는다.

문서보다 실제 구현이 우선한다.
