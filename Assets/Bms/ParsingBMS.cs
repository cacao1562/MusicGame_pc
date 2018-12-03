using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParsingBMS : MonoBehaviour {

	string filename;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Parse() {
		StreamReader stream = new StreamReader(filename, System.Text.Encoding.Default);
		string linedata;
		do{
			linedata = stream.ReadLine();
			Process(linedata);
		}while (linedata != null);

		stream.Close();

	}


	public void Process(string linedata)
	{
	// BMS파일 명세에 따라서 동작한다.

	// 읽은 데이터가 없다면 리턴한다.
	if (linedata == null ) return;

	if (linedata.StartsWith("#"))
		{
			char[] seps = new char[]{' ',':'};
			string[] StringList = linedata.Split(seps);
					
	if ( StringList[0].Equals("#PLAYER") )
			{
				// Player데이터를 얻어 온다.                    
				int Player = int.Parse(StringList[1]);
			}
			else if (StringList[0].Equals("#GENRE"))
			{
			}
			else if (StringList[0].Equals("#TITLE"))
			{
			}
			else if (StringList[0].Equals("#ARTIST"))
			{
			}
			else if (StringList[0].Equals("#BPM"))
			{
				// refBMSPlayer.BPM = double.Parse(StringList[1]);
			}
			else if (StringList[0].Equals("#PLAYLEVEL"))
			{
			}
			else if (StringList[0].Equals("#RANK"))
			{
			}
			else if (StringList[0].Equals("#VOLWAV"))
			{
			}
			else if (StringList[0].Equals("#STAGEFILE"))
			{
			}
			else if (StringList[0].Equals("#TOTAL"))
			{
			}
			else if (StringList[0].Equals("#MIDIFILE"))
			{
			}
			else if (StringList[0].Equals("#VIDEOFILE"))
			{
			}
			else if (StringList[0].Substring(0, 4).Equals("#WAV"))
			{
				// int wavIndex = int.Parse( StringList[0].Substring(4, 2),NumberStyles.HexNumber);
				// refBMSPlayer.AddWav( wavIndex, pathname + "/" + StringList[1]);
			}
			else if (StringList[0].Substring(0, 4).Equals("#BMP"))
			{
				// int bitmapIndex = int.Parse(StringList[0].Substring(4, 2), NumberStyles.HexNumber);
				// refBMSPlayer.AddBmp(bitmapIndex, pathname + "/" + StringList[1]);
			}
			else // 그 외에는 모두 데이터 파일이라고 한다.
			{
				try
				{
					//마디를 등록한다.
					// int BarNum = GetBarNum(StringList[0]);
					// int ChannelNum = GetChannelNum(StringList[0]);

					//채널의 두자리에서 10의 자리에 있는 수로 파싱이되며
					// int ChannelFirst = GetChannelFirst(StringList[0]);

					//1의 자리에 있는 수는 그에 따라서 결정된다.
					// int ChannelSecond = GetChannelSecond(StringList[0]);

					// refBMSPlayer.AddBar(BarNum, ChannelFirst, ChannelSecond, StringList[1]);
				}
				catch (System.Exception e)
				{
					// MessageBox.Show(e.Message);
				}
			}
		}
		else
		{
			// #으로 시작하지 않는 문자는 모두 무시한다.
			return;
		}
	}
}
