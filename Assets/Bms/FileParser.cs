using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileParser : MonoBehaviour {

	public GameObject NoteBar;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void ReadFromFile() {
		int i = 0;
		string[] lines = System.IO.File.ReadAllLines("Assets/00.bms");

		if (lines == null ){
			return;
		}
		foreach ( string line in lines) {

			if( lines[i].StartsWith("#")) {
				char[] seps = new char[]{ ' ', ':'};
				string[] StringList = lines[i].Split(seps);

				if( StringList[0].Equals("GENRE")) {

				}else if (StringList[0].Equals("#TITLE")){

				}else if (StringList[0].Equals("#PLAYER")){

				}else if (StringList[0].Equals("#ARTIST")){

				}else if (StringList[0].Equals("#PLAYLEVEL")){

				}else if (StringList[0].Equals("#RANK")){

				}
				else if (StringList[0].Equals("#TOTAL")){

				}else if (StringList[0].Equals("#STAGEFILE")){
				}else if (StringList[0].Equals("#MIDIFILE")){
				}else if (StringList[0].Equals("#VIDEOFILE")){
				}else if (StringList[0].Substring(0, 4).Equals("#WAV")){
					// int wavIndex = int.Parse( StringList[0].Substring(4, 2),NumberStyles.HexNumber);
					// refBMSPlayer.AddWav( wavIndex, pathname + "/" + StringList[1]);
				}else if (StringList[0].Substring(0, 4).Equals("#BMP")){
					// int bitmapIndex = int.Parse(StringList[0].Substring(4, 2), NumberStyles.HexNumber);
					// refBMSPlayer.AddBmp(bitmapIndex, pathname + "/" + StringList[1]);
				}else {// 그 외에는 모두 데이터 파일이라고 한다.{
					try{
						int BarNum = GetBarNum(StringList[0]);
					    int ChannelNum = GetChannelNum(StringList[0]);
						// GetComponent<NoteBar>().BlockProcess(ChannelNum, BarNum, StringList[i], "KEY");
					}catch (System.Exception e) {
						Debug.Log(e);
					}
				}
			}
			i++;
		}
	}

	private int GetBarNum(string data) {
		return int.Parse(data.Substring(1,3));
	}
	private int GetChannelNum(string data){
		return int.Parse(data.Substring(4,2));
	}
}
