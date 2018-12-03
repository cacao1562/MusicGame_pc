using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
 
public class SongInfoLoading : MonoBehaviour {
 
    public Transform ParentObj; // 파싱한 데이터를 저장하기 위한 부모 오브젝트
    public Transform[] tpList;  // 자식 오브젝트를 저장하기 위한 배열
    public string[] songList;   // 곡 이름을 저장하는 문자열 배열
    public int index;   // 몇번째 곡이 선택되었는지 표시하는 인덱스 변수
 
 
    //=================================== Data Parse
    public void ParseData(int index)    // 파싱한 곡의 정보를 입력하는 메소드, 정수형 파라미터로 오브젝트를 구분함.
    {
        string temp = "";   // 임시코드, Title, Artist 등 헤더 파일 값을 저장.
        tempStr = "";
        for (int i = 1; i < tempSplit.Length; i++)  // #TITLE ~ [#TITLE] 을 자른 나머지 데이터를 취합한다. (곡 제목, 아티스트 등) 데이터 저장 
        {
            tempStr += tempSplit[i] + " ";  // 공백 혹은 콜론 구분자로 문자열을 잘랐으나, 콜론의 경우 데이터 파싱 부분에서만 사용하기 때문에 공백을 추가하여 누적한다.
        }
        temp = tempSplit[0].Substring(1, tempSplit[0].Length-1);    // 18번 라인에서 언급한 [Title]의 문자열 저장.
        //Debug.Log(temp);
        tpList[index].GetComponent<Text>().text = temp + " : " + tempStr;   // 파싱한 데이터를 각 자식 오브젝트의 Text에 뿌려줌.
    }
    //=================================== Data Parse
 
    //=================================== Image Parse
    public void ParseBackImg(int index) // 파싱한 곡의 이미지를 뿌려주는 메소드, 이 또한 정수형 파라미터로 오브젝트 구분
    {
        SpriteRenderer spriteRenderer;  // 이미지 오브젝트의 spriteRenderer component를 사용하여 이미지를 변경한다.
        spriteRenderer = tpList[index].GetComponent<SpriteRenderer>();  // 오브젝트의 spriteRenderer component를 가져옴.
 
        Sprite sp = Resources.Load<Sprite>("Song/"+SongName+"/BG"); //이미지 리소스파일 이름은 BG로 통일 할 예정이며, 47번 라인에서 선언한 음악 파일 이름으로 이미지를 구분.
 
        spriteRenderer.sprite = sp; // sprite 변경
    }
    //=================================== Image Parse
 
 
    protected FileInfo fileNmae = null;     // BMS 파일을 열기위한 파일 제어 클래스
    protected StreamReader reader = null;   // 파일 스트림을 읽기 위한 StreamReader
    protected string path;       // 곡 파일 path
    protected string StrText;   // 파일을 한줄씩 읽었을때 저장하는 문자열
    protected string SongName;  // 이미지 파일을 파싱할 때 상대경로로 지정하는 임시 변수, 추후 수정 할 수 있음.
 
 
    private char[] seps;                    // 구분자, [공백과 콜론]으로 구분.
    private string[] tempSplit;      // 위 구분자로 파싱한 문자열을 잘라 담는 임시 문자열 배열
    private string tempStr;                 // 자른 문자열 데이터에서 자식 오브젝트의 Text에 저장할 임시 문자열, 추후 수정할 수 있음.
 
    // Use this for initialization
 
    void Start ()        // 스크립트 호출시 시작.
    {
        //========== initializing
        index = 0;
        tempSplit = null;
        tempStr = "";   
        StrText = "";
        SongName = "";
        path = "Assets/Resources/Song/";
        seps = new char[] { ' ', ':' };
        //========== initializing
 
        SongName = songList[index]; // 곡 이름은 List에서 담은 값으로 저장, 추후 변경 가능
        path = path + songList[index] + "/";    // 42번 라인에서 저장한 root path에서 해당하는 곡의 경로를 저장
        fileNmae = new FileInfo(path + songList[index]+".bms"); // 해당 곡의 BMS 파일을 파싱하기 위한 코드, BMS파일을 지정
 
        if (fileNmae != null)   //파일이 NULL이 아닐 경우 실행
        {
            reader = fileNmae.OpenText();   // BMS 파일을 Open한다.
        }
 
        else // 파일이 NULL일 경우, 디버그로 에러 검출
        {
            Debug.Log("65Line, BMS Parse Error");
        }
        
        tpList = ParentObj.gameObject.GetComponentsInChildren<Transform>(); // ParentObj의 Transform components가 있는 자식들을 가져옴 / 자식들 전체를 가져옴.
 
    }
    
    // Update is called once per frame
    void Update ()      // Update문은 추후 코루틴(Coroutines)을 사용하여 변경 예정
    {    
        if(StrText != null && StrText != "*---------------------- HEADER FIELD END")    // BMS파일에서 읽어온 문자열이 null이 아니면서 [*---------------------- HEADER FIELD END] 이 아닐때.
            // 원하는 헤더파일 부분만 읽기위해 작성.
        {
 
            StrText = reader.ReadLine();    // BMS파일을 한줄씩 읽음.
            tempSplit = StrText.Split(seps);    // 구분자(공백, 콜론)으로 읽어온 문자열을 자름
            
            if (tempSplit[0].Equals("#TITLE"))  // 자른 문자열이 #Title일때
            {
                ParseData(2);
            }else if (tempSplit[0].Equals("#ARTIST"))
            {
                ParseData(3);
            }else if (tempSplit[0].Equals("#GENRE"))
            {
                ParseData(4);
            }else if (tempSplit[0].Equals("#BPM"))
            {
                ParseData(5);
            }else if (tempSplit[0].Equals("#PLAYLEVEL"))
            {
                ParseData(6);
            }else if (tempSplit[0].Equals("#RANK"))
            {
                ParseData(7);
            }else if (tempSplit[0].Equals("#STAGEFILE")) // 자른 문자열이 이미지 파일 데이터일때
            {
                ParseBackImg(10);
            }
            else
            {
                Debug.Log("[NotErr]Nothing : " + StrText);    //에러X 확인용 디버그 코드
            }
        }
    }
}