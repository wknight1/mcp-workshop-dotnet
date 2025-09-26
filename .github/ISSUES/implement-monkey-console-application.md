### Implement Monkey Console Application

**개요**

간단한 C# 콘솔 애플리케이션을 추가합니다. 이 앱은 사용 가능한 원숭이 목록을 나열하고, 이름으로 특정 원숭이의 세부 정보를 조회하며, 무작위 원숭이를 선택할 수 있어야 합니다. 또한 데이터 모델로 `Monkey` 클래스를 사용하고, 실행 시 보여줄 ASCII 아트를 포함해 시각적 즐거움을 더합니다.

**요구사항**

- 모든 사용 가능한 원숭이를 나열하는 기능
- 이름으로 특정 원숭이의 세부 정보를 반환하는 기능
- 무작위 원숭이를 선택하는 기능
- `Monkey` 모델 클래스 포함(예: `Name`, `Location`, `Population` 등)
- 콘솔 출력에 사용할 ASCII 아트 포함

**수용 기준**

- `dotnet run`으로 콘솔 앱을 실행하면 메뉴(또는 커맨드)를 통해 위 3가지 기능을 수행할 수 있다.
- `Monkey` 모델과 최소한의 샘플 데이터(하드코딩 또는 간단한 로컬 저장소)가 포함되어 있다.
- README 또는 이슈에 간단한 사용 예시가 문서화되어 있다.

**작업 계약(작동 기준)**

- 입력: 콘솔에서 사용자가 선택하는 명령 또는 원숭이 이름(문자열)
- 출력: 콘솔에 출력되는 원숭이 목록, 원숭이 상세, 또는 선택된 무작위 원숭이 정보
- 에러 모드: 존재하지 않는 원숭이 이름 조회 시 적절한 메시지 출력(예: "Monkey not found")
- 성공 기준: 위 기능이 정상 동작하고 단순한 테스트로 검증 가능

**설계 및 구현 아이디어**

- 모델: `Monkey` 클래스(속성: `Name`, `Location`, `Population`, 필요시 `Description`)
- 데이터: `MonkeyHelper` 또는 `MonkeyRepository` 정적 클래스에 샘플 데이터 배열/리스트 구현
- UI: `Program.cs` 에서 간단한 텍스트 메뉴(1: list, 2: get by name, 3: random, q: quit)를 제공
- ASCII 아트: 프로그램 시작시 또는 리스트 출력 시 간단한 원숭이 아트 출력
- 테스트: 최소한의 유닛 테스트(예: `GetMonkeys()`, `GetMonkeyByName()` happy path) 추가 권장
- 위치: 기존 `workshop/MyMonkeyApp/` 디렉터리가 있으므로 그 하위에 구현을 두는 것을 권장

**샘플 ASCII 아트**

```
   .-"""-.
  /       \
 |  .-. .- |
 |  |_| |_| |
 |  (o) (o) |
 |   \   /  |
  \  '---' /
   '-.___.-'
```

**체크리스트**

- [ ] `Monkey` 모델 클래스 생성(`Name`, `Location`, `Population`, `Description?`)
- [ ] `MonkeyHelper` 또는 `MonkeyRepository`에 샘플 데이터 추가
- [ ] 콘솔 앱 `Program.cs`에 메뉴(또는 명령 파서) 구현
- [ ] 기능 구현: `List`, `GetByName`, `GetRandom`
- [ ] ASCII 아트 추가 및 출력 타이밍 결정
- [ ] README에 사용 방법 및 예시 추가
- [ ] (선택) 단위 테스트 추가: `GetMonkeys()`, `GetMonkeyByName()`, `GetRandomMonkey()`
- [ ] (선택) CI 작업(예: GitHub Actions)으로 `dotnet build`/`dotnet test` 추가

**추가 메모**

- 구현은 `C# 13`, `.NET 9` 프로젝트 규칙을 따르고, 리포지토리 내 `workshop/MyMonkeyApp/` 디렉터리를 활용하세요.
- 단순하고 유지보수 쉬운 구조(모델, 데이터, 프로그램 분리)를 권장합니다.

**상태 및 권장 조치**

- 저장소의 GitHub Issues 기능이 비활성화되어 있어 공식 GitHub 이슈를 생성할 수 없습니다.
- 저장소 설정에서 Issues를 활성화하면(Repository settings → Features → Issues) 제가 동일한 이슈를 GitHub에 직접 생성해 드릴 수 있습니다.
- 즉시 진행을 원하시면 이 마크다운 파일을 이슈 대체물로 사용하거나, 제가 바로 기능의 예시 구현(브랜치 + PR)을 만들어 드릴 수 있습니다.

**레이블(권장)**

- enhancement
- good first issue

