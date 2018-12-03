using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class SocketManager : MonoBehaviour
{
	private SocketIOComponent socket;
	private string roomNumber = "music";  //방 번호
	private JSONObject mobj;
	public Image[] imgArr = new Image[4];
	public Image[] enemyArr = new Image[4];
	private bool check = false;
	public GameObject dragon;
	public int mSore = 0;

	public void Start() 
	{

        Debug.Log("roomNum = "+roomNumber);
	
		socket = GameObject.Find ("SocketIO").GetComponent<SocketIOComponent> ();
		socket.On ("open", OnOpen);
		socket.On ("drawing", OnGetValue);
		socket.On ("error", OnError);
		socket.On ("close", OnClose);

		for(int i = 0; i < enemyArr.Length; i++) {
			enemyArr[i].gameObject.SetActive(false);
		}
		
	}

	public void catchSocket(int spNum){

		// Debug.Log("send socket");
		JsonDataStr jstr = new JsonDataStr();
		jstr.sendStr = "catch";
		jstr.spNum = spNum;
		jstr.score = mSore;
		JSONObject jo = new JSONObject(JsonUtility.ToJson(jstr) );
		socket.Emit("send", jo );

	}

	void Update()
	{

		if(!socket.IsConnected){
			Debug.Log("소켓 연결 안됨");
			return;
		}
		if(mobj == null){
			return;
		}

		if(mobj.Count <= 0){
			return;
		}	
   		

		if (check) {

			JsonData jd = JsonUtility.FromJson<JsonData>(mobj.ToString());
			
			for(int i=0; i<jd.idxList.Count; i++){
			
				imgArr[jd.idxList[i]].color = Color.white; 
				enemyArr[jd.idxList[i]].gameObject.SetActive(true);
				// planObj[jd.idxList[i]].GetComponent<MeshRenderer>().material.color = Color.black;
			}
			// int id = int.Parse(mobj.GetField("PacketID").ToString());
  
			

		}else{

			for(int i=0; i<imgArr.Length; i++){
				
				imgArr[i].color = Color.black; 
				enemyArr[i].gameObject.SetActive(false);
			}

		}

		check = false;
		
		
	}
	// void DownKey(){
		
	// 	float velocity = Random.Range(1,10);
	// 	Vector2[] v2 = new Vector2[2];
	// 	for (int i = 0; i<2; i++){
	// 		float startx = Random.Range(10,1000);
	// 		float starty = Random.Range(70, 730);
	// 		v2[i] = new Vector2(startx,starty);
	// 	}
	// 	if (Input.GetKeyDown("1")){
	// 		mGTParticleEmitter.Emit(GetWorldPos(v2[0]), GetWorldPos(v2[1]), velocity, 1);
	// 	}else if(Input.GetKeyDown("2")) {
	// 		mGTParticleEmitter.Emit(GetWorldPos(v2[0]), GetWorldPos(v2[1]), velocity, 2);
	// 	}else if(Input.GetKeyDown("3")) {
	// 		mGTParticleEmitter.Emit(GetWorldPos(v2[0]), GetWorldPos(v2[1]), velocity, 3);
	// 	}else if(Input.GetKeyDown("4")){
	// 		mGTParticleEmitter.Emit(GetWorldPos(v2[0]), GetWorldPos(v2[1]), velocity, 4);
	// 	}else if(Input.GetKeyDown("q") ){
	// 		gTParticleEmitter.dd = 2;
	// 		cameraResolution.widthV = 504 * 2;
	// 		cameraResolution.heightV = 240 * 2;
	// 		cameraResolution.changTempCamVal();
			
	// 	}else if(Input.GetKeyDown("w")) {
	// 		gTParticleEmitter.dd = 1;
	// 		cameraResolution.widthV = 504;
	// 		cameraResolution.heightV = 240;
	// 		cameraResolution.changTempCamVal();
			
	// 	}
	// }



	public void OnOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open(): " + e.data);
	
		socket.Emit("joinRoom", JSONObject.StringObject(roomNumber)); //방번호
			
	}
	
	public void OnGetValue(SocketIOEvent e)
	{
		// Debug.Log("get_Value: " + e.data);
		JsonDataStr js = JsonUtility.FromJson<JsonDataStr>(e.data.ToString());
		if( js.sendStr == "dragon"){
			StartCoroutine( showDragon() );
			mSore = 0;
		}
		// if( js.sendStr == "resetScore"){
		// 	mSore = 0;
		// }
		
		if (e.data == null) { return; }
		mobj = e.data; 
		check = true;
		//mTextListManager.AddItem (e.data.GetField ("UserText").str);
		
	}
	
	public void OnError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error(): " + e.data);
	}
	
	public void OnClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close(): " + e.data);
	}

	IEnumerator showDragon() {

		dragon.SetActive(true);
		yield return new WaitForSeconds(7.1f);
		dragon.SetActive(false);

	}

}

[System.Serializable]
public class JsonData{
	public List<int> idxList;
}


[System.Serializable]
public class JsonDataStr{
	public string sendStr;
	public int score;
	public int spNum;
}