# Arcane Survivor — Three.js → Unity Migration Notes

## 1. 목적
이 문서는 기존 Three.js Prototype에서 Unity Prototype으로 넘어올 때 무엇을 계승하고 무엇을 버리는지 기록한다.

Three.js Repository는 Reference Prototype으로 보존한다.

Unity Repository가 생성된 이후부터는 Unity 구현 상태가 새로운 개발 기준이다.

## 2. 계승해야 하는 것

### Game Loop
- Movement
- Auto Combat
- Enemy Kill
- XP Drop
- XP Pickup
- Level Up
- Upgrade Choice
- Build Development

### Build Structure
- Active 2
- Passive 1
- School Skill Max Lv.2
- Common Upgrade는 Slotless
- 4 Schools
- School Point 2/4/6
- Pure 6
- 4+2
- 2+2+2

### Starting Rule
Empty Loadout으로 시작한다.

8 Active Spell 중 하나를 Lv.1로 직접 선택한다.

Magic Missile 자동 지급 없음.

### School Identity
Arcane:
- Projectile
- Universal Magic Damage

Fire:
- Burning
- AoE
- Spread

Lightning:
- Bounce
- Stagger

Frost:
- Slow
- Control

## 3. 계승할 필요가 없는 것
Unity에서 다음 Three.js 구현 방식은 그대로 복제할 필요가 없다.

- THREE.Scene
- THREE.Group
- THREE.Object3D
- Three.js Renderer 관리
- Geometry / Material dispose 구조
- CanvasTexture Placeholder Character
- HTML/CSS HUD
- DOM 기반 Upgrade UI
- Three.js 전용 Camera 코드
- Browser Rendering 구조

Unity에서 같은 Gameplay Result를 더 자연스럽게 구현할 수 있다면 Unity 방식을 사용한다.

## 4. Reference Repository의 역할
기존 Three.js Repository는 다음 상황에서 참고한다.

- Spell 동작이 기억나지 않을 때
- Status Effect 처리 확인
- Synergy 계산 확인
- Damage 적용 순서 확인
- Level Up Candidate 규칙 확인
- 과거 설계 결정 확인

Three.js 코드를 C#으로 줄 단위 번역하는 것은 목표가 아니다.

## 5. Unity 대응 개념 예시
- THREE.Object3D → GameObject / Transform
- 직접 Sprite / Mesh 생성 → SpriteRenderer / Prefab
- Enemy 생성 코드 → Prefab Instantiate
- HTML/CSS UI → Unity UI
- 수동 리소스 dispose → Unity Object Lifecycle

이는 강제 Mapping이 아니라 참고 방향이다.

## 6. Migration 원칙
Unity로 옮기면서 기능을 동시에 대량 추가하지 않는다.

먼저 Three.js Prototype과 비슷한 Gameplay를 복구한다.

Migration 중:
- Boss 추가 금지
- Meta Progression 추가 금지
- Skill 추가 금지
- School 추가 금지
- 대규모 Visual Polish 금지
- 최종 Balance 금지

Unity에서 기존 Prototype의 핵심 Loop가 다시 동작한 뒤 다음 판단을 한다.

## 7. 폐기된 과거 설계

### Forced Magic Missile Starter
폐기.

### Automatic Starter Replacement
폐기.

현재:
빈 Active 2칸
→ 시작 Active 직접 선택
→ 이후 정상적인 Slot 규칙 사용

## 8. Migration 완료 판단
Unity에서 다음 질문에 Yes가 나오면 Migration이 성공한 것이다.

- Three.js Prototype의 Build 구조가 유지되는가?
- 각 School의 플레이 감각이 구분되는가?
- Level Up 선택이 Build 결정에 영향을 주는가?
- 다른 Build로 다시 플레이하고 싶은가?
- Unity에서 이후 콘텐츠를 추가하는 것이 Three.js보다 자연스러운가?
