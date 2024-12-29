

# ConsoleNecroDancer

1. **프로젝트 이름 및 제작 기간**
    
    - 네크로 댄서라는 인디게임을 모작할 예정인데 그전에 미니멥으로 타일정보부터 만들어 보려고 시작한 프로젝트 입니다.
    - 그러나 일단 게임처럼 가공하기위해 이것저것 살짝 덧붙였습니다.      
    - ConsoleNecroDancer - 리듬 액션 던전 탐험 게임
    - 2024/12/23 ~ 2024/12/29 (총 7일)
      
2. **게임 개요**
    
    - 화면 가운데 심장이 있고, 심장으로 다가오는 비트가 있습니다.
    - 이 비트를 심장안에서 맞추면 움직일수있고, 못맞추면 못움직인다.
    - 몬스터들은 2비트당 한번씩 움직이며,각각 다양한 패턴이 있었습니다. 
    - 다만 이 프로젝트는 리듬을 맞춰 움직이고 사냥하는 부분만 구현되어있습니다.
      
1. **기능 목록**
    
    - 구현할 주요 기능을 나열.
        - 게임 시작/종료
        - 사용자 입력 처리 (ReadKey를 이용한 키입력으로 구현)
        - 상태 출력 (멥 위에 몬스터 체력과 상태 그리고 플레이어 정보가 띄워져있습니다.)
        - 기본 전투 시스템
	        - 비트가 [ ] 모양의 심장안으로 들어왔을때 움직이는 키를 입력하면 움직일수있습니다.
	        - 몬스터가 근처에 있을때 그방향으로 공격하면 공격가능.
	        - 반대로 몬스터가 이동하는 방향에 있으면 공격받음.
	        - 그렇게 피가 다달은 유닛은 사망.
	        - 유저는 그렇게 게임오버.
        - 
        - 승리/패배 조건
	        - 몬스터 다죽이면 승리.
	        - 플레이어가 죽으면 끝.
            
1. **게임 흐름**
    
    - 게임의 전반적인 흐름을 단계별로 설명.
    - 
        1. 게임 시작 화면 출력.
        2. 플레이어가 돌아다니면서 몬스터 사냥.
        3. 던전 탐험(이동, 전투)
        4. 목표 달성 시 승리 화면 출력.
        5. 실패 조건 만족 시 패배 화면 출력.
           
5. **사용 데이터 구조**
    
    - 프로젝트에서 사용할 데이터 구조와 그 용도.
        - Hart 클래스 내에 Queue<Beat> beats; 가 있습니다.        
	        - 비트가 들어온 순서대로 없어져야 해서 큐를 사용했습니다.
	- BattleManager에서 몬스터를 관리해주기 위해  List<Monster> monsters 를 만들었습니다. 추후 추가되는 모든 몬스터를 이곳에 넣고 
         특수기능을 사용할때만 다형성으로 as 써서 해당 몬스터클래스로 바꿀예정이였습니다.
```
			ex) ' case "ⓢ"://슬라임 -혼자 노는녀석 '
					 'Slime slime = monsters[i] as Slime;				     
				     //다음좌표 저장
				       monsterNextPos = slime.NextMove();   
```


6. **클래스 설계**
    
    - 주요 클래스와 역할을 정의. 
		- `Player`: 플레이어 상태 관리  -> 체력,공격,이동
		- `Monster`: 몬스터 정보 (체력, 현재 움직이는 방향.)들의 부모 클래스 추후 추가할경우 이걸 기반으로 자식클래스들 만들어줄거임 . 지금은 슬라임 클래스만 구현됨.
		- `GameManager`: 게임의 흐름 제어. - 원래는 상점과 멥선택을 만들었을때 사용하려고 했음. 지금은 하트비트만 제어중.
		- `BattleManager`: 전투 로직 처리. 전투중 공격 혹은 이동에대한 제어를 하고있음. 몬스터 2비트당 움직임과 플레이어의 이동키값을 받았을때의 이동과 공격을 제어중.
      
7. **추가 구현 아이디어**
    
    - 시간 남으면 추가로 도전해볼 수 있는 요소.
        - 몬스터 추가
            -  기획단계에선 스켈레톤과 미노타우로스가 있었다. 
            - 스켈레톤은 인식범위 내에서 쫒아가는기능이 있었을 예정이였다.
            - 미노타우로스는 플레이어를 같은 y선상 즉 마주보고있을때 돌진할 예정이였었습니다.
            - 물론 직진이라 중간에 피하는 패턴을 생각했었습니다.
        - 아이템 추가
          -장비랑 골드 등 만들준비는 했으나...  시간이모잘랐습니다.   
        - 함정 및 벽 타일 추가
          -이 또한 enum문과 특수문자는 구해놨으나 시간모자라서 미구현했습니다.
            
8. **프로젝트 마감 일정**
    
    - 프로젝트 완료까지의 일정 및 목표.
        - 
            - Day 1: 클래스 설계 및 기초 구현.
            - Day 2: 기본 게임 로직 완성.
            - Day 3: 기본 로직을 이용해서 기능추가.
            - Day 4: 기능 테스트.
            - Day 5: 최종 제출 및 발표 준비.
              
9. **깃 전략**
    
    - 코딩을 하다보면 싹다 엎거나 그에 준하는 수정이 필요할때가 있다.
    - 그럴때를 대비해서 브런치를 나눠서 사용할것이다.
    - 혹은 테스트량이 방대한데 이걸 메인에다 해버리면 나중에 원본을 찾기가 힘들것 같을떄 사용예정입니다.
  
  
# 초기에 생각한 마인드맵

![마인드맵](https://github.com/user-attachments/assets/69e874e9-375d-4b4a-8086-25370eadbbf1)

# 코드 기능 정리

## Program.cs
### 멤버변수
```cs
	GameManager gameManager = new GameManager();
	BattleManager battleManager = new BattleManager();
	
	//프레임 시간조절용
	Stopwatch frameWatch = new Stopwatch();
	//비트 시간조절용
	Stopwatch beatWatch = new Stopwatch();
	//몬스터 시간조절용
	Stopwatch monsterWarch = new Stopwatch();
```
### 메소드
```cs
	//게임시작시 작동할 메소드
	static public bool GameStart();
	//게임시작시 보여줄 로고
	static void LogoPrint();

```

## GameManager.cs
### 멤버변수
```cs

	//하트의 위치를 정해줄때 쓰인 녀석들.
	public static int height;
	public static int width;
	
	Hart hart;
	
	//비트시간
	Stopwatch beatWatch = new Stopwatch();
	//전체적인 키값으로 이용
	public static ConsoleKeyInfo input;
	//게임매니저 작동여부
	public static bool isGameStart = true;
	//플레이어 움직임을 제어해줄 변수
	public bool isAction = false;
	//게임 전체적인 멥초기화용도
	static string strClear = "";
	
	
```
### 메소드
``` csharp
	//프로그램 클래스에서 시계를 받아옴.
	public void SetTimer(Stopwatch stopwatch);
	
	public void Init();
	//키를 눌렀을때 비트가 어떤상황인지에 따른 반응을 넣어둠.
	public void KeyInputAction();
	
	//키를 입력받고, 플레이어의 이동을 제어하고 , 비트의 생성을 해주는 녀석
	public void Update();
	//위의 정보를 받아 그려주는 녀석
	public void Render();
	
	//Console.Clear()대신 쓸 메소드
	//원래는 static이였으나 어차피 화면변화는 한번만해주면되길래 수정.
	public void ConSoleClear();
	
	public void End();
	public void YouDiE();
	public void YouWin();
	
```


## Hart.cs
### 멤버변수
```cs
	//비트가 들어온 순서대로 나가야해서 큐씀
	Queue<Beat> beats;
	//하트의 위치
	Point _point;
	//하트의 길이7
	int _len;
	//하트 이미지
	string _image = "[     ]";
	//하트 사이즈 기록용
	int size;
	int beatX;//비트 왼쪽의 x값
	//콤보값
	public int combo = 0;

```
### 메소드
```cs
	//하트의 좌표를 가져올 메소드
	public Point GetPos();
	//비트의 좌표를 지정해주면서 생성해줄 메소드
	public void Addbeat(int posY);
	//비트들을 한쌍으로 지워줄 메소드
	public void Removebeats();
	//비트들 전체를 움직일 함수
	public void beatMove();
	//비트가 있는지 체크하는 함수
	public bool Isbeats();
	//비트가 하트에 안맞았는지 체크
	public bool IsNonCheckHit();
	//비트가 하트에 맞았는지 체크
	public bool IsCheckHit();	
	//위의 메소드들을 조합해서 사용해줄 함수
	public void Update();
	//갱신된 정보를 그려줄 함수.
	public void Render();
```

## Beat.cs
### 멤버변수
```cs
	Point _point;	
	bool _isLeft;//true면 왼쪽     false이면 오른쪽        
	string _image = "|";

	//프로퍼티	
	//get만 사용하고 ,set은 차단해두었다.  게임이 끝날떄까지 변동이 될 예정이 없었기 때문입니다.
	public string Image{ get { return _image; } }
	//좌표지정을 생성할때만 할것이기 때문에 굳이 다시 set할필요 없어서 사용 안했다. 무엇보다 움직일때 다른곳으로 갑자기 튀어나가면 안되기 때문에 이렇게 했습니다.
	public Point Point { get { return _point; } }

```
### 메소드
```cs
    public void Move();

```

## BattleManager.cs
### 멤버변수
```cs
	
	Player player;
	
	List<Item> dropItemList;//바닥에 아이템 떨군거 표시 
	List<Monster> monsters;//나중에 몬스터들 여기다 다 넣을거임.
	
	Point playerTempPos;//플레이어가 이동할 다음좌표
	
	Point monsterNextPos;//몬스터 다음좌표 가지고있음.
	Point monsterPrevPos;//몬스터 이전좌표 가지고있음.
	
	//스폰할떄 위치 저장해둔데 안가기 만들려고 씀.
	List<Point> tempSpawnPos;
	//플레이어가 미리 이동해본 좌표로 이동할수 있나 체킹해주는 변수
	bool isPlayerMove = false;
	bool isAction = false;
	
	
	
	//들어온 값이랑 다른값이 나올때 까지 랜덤
	Random random1 = new Random();
	Random random2 = new Random();
	
	Stopwatch monsterWatch = new Stopwatch();

	
```
### 메소드
``` csharp
	//몬스터들 랜덤스폰할때 겹치지않게 사용된 함수.
	public Point RandomPos();
	//게임매니저의 액션값과 공유하게 해주는 함수
	public void SetAction(bool Action)
	//몬스터들의 타이머를 담당.
	public void SetTimer(Stopwatch stopwatch)
	//몹랜덤생성등 초기화 기능
	public void Init();
	//플레이어 이동값이랑 타일정보갱신 전투등 메인로직이 담겨있는 메소드
	public void Update();
	//위의 정보를 가져와 그려주는 메소드. 이곳엔 탐색의 로직으로 쓸수있는 맨해튼거리 로직이 담겨있다.
	public void Render();

```
---

