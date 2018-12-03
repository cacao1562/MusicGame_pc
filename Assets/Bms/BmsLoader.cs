using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
 
public class BmsLoader : MonoBehaviour {
 
    public string bmsFileName = "BS_5a.bms";
    public bool isFinishLoad;
    public Bms bms;
 
    // Use this for initialization
    void Start () {
        BmsLoad();
    }
   
    // Update is called once per frame
    void Update () {
   
    }
 
    private void BmsLoad()
    {
        bms = gameObject.AddComponent<Bms>();
 
        string[] lineData = System.IO.File.ReadAllLines("Assets/BmsFiles/BS_5a.bms");
		//File.ReadAllLines(Application.dataPath + "/BmsFiles/" + bmsFileName);
 
        foreach (string line in lineData)
        {
            if (line.StartsWith("#"))
            {
                string[] data = line.Split(' ');
 
                // 데이터 섹션이 아니면서 헤더 데이터가 없는 경우에는 건너 뜀.
                if (data[0].IndexOf(":") == -1 && data.Length == 1)
                {
                    continue;
                }
 
                // 헤더 섹션
                if (data[0].Equals("#TITLE"))
                {
                    bms.setTitle(data[1]);
                }
                else if (data[0].Equals("#ARTIST"))
                {
                    bms.setArtist(data[1]);
                }
                else if (data[0].Equals("#BPM"))
                {
                    bms.setBpm(double.Parse(data[1]));
                }
                else if (data[0].Equals("#PLAYER"))
                {
                }
                else if (data[0].Equals("#GENRE"))
                {
                }
                else if (data[0].Equals("#PLAYLEVEL"))
                {
                }
                else if (data[0].Equals("#RANK"))
                {
                }
                else if (data[0].Equals("#TOTAL"))
                {
                }
                else if (data[0].Equals("#VOLWAV"))
                {
                }
                else if (data[0].Equals("#MIDIFILE"))
                {
                }
                else if (data[0].Equals("#WAV"))
                {
                }
                else if (data[0].Equals("#BMP"))
                {
                }
                else if (data[0].Equals("#STAGEFILE"))
                {
                }
                else if (data[0].Equals("#VIDEOFILE"))
                {
                }
                else if (data[0].Equals("#BGA"))
                {
                }
                else if (data[0].Equals("#STOP"))
                {
                }
                else if (data[0].Equals("#LNTYPE"))
                {
                    bms.setLnType(int.Parse(data[1]));
                }
                else if (data[0].Equals("#LNOBJ"))
                {
                }
                else
                {
                    // 위의 경우에 모두 해당하지 않을 경우, 데이터 섹션
                    int bar = 0;
                    Int32.TryParse(data[0].Substring(1, 3), out bar);
 
                    int channel = 0;
                    Int32.TryParse(data[0].Substring(4, 2), out channel);
 
                    string noteStr = data[0].Substring(7);
                    List<int> noteData = getNoteDataOfStr(noteStr);
 
                    Note note = gameObject.AddComponent<Note>();
                    note.setBar(bar);
                    note.setChannel(channel);
                    note.setNoteData(noteData);
                    // note.debug();
 
                    bms.addNote(note);
                }
            }
        }
 
        if (bms.getNoteList().Count != 0)
        {
            isFinishLoad = true;
        }
 
    }
 
    private List<int> getNoteDataOfStr(string str)
    {
        string tempStr = str;
        List<int> noteList = new List<int>();
 
        while (true)
        {
            if (tempStr.Length > 2)
            {
                int data = 0;
                Int32.TryParse(tempStr.Substring(0, 2), out data);
 
                noteList.Add(data);
                tempStr = tempStr.Substring(2);
            }
            else
            {
                int data = 0;
                Int32.TryParse(tempStr, out data);
                break;
            }
        }
 
        // 총노트수 증가
        foreach (int note in noteList)
        {
            if (note != 0)
            {
                bms.sumTotalNoteCount();
            }
        }
 
        return noteList;
    }
 
}