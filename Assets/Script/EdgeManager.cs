using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EdgeManager : MonoBehaviour {

	public Image[] EdgeRed = new Image[4];
	public Image[] EdgeGreen = new Image[4];

	void Start () {

		for(int i=0; i < EdgeRed.Length; i++) {

			EdgeRed[i].gameObject.SetActive(false);
			EdgeGreen[i].gameObject.SetActive(false);

		}
		
	}

	public void missMonster() {

		for(int i=0; i < EdgeRed.Length; i++) {

			EdgeGreen[i].gameObject.SetActive(false);
			EdgeRed[i].gameObject.SetActive(true);

		}

	}

	public void catchMonster() {

		for(int i=0; i < EdgeRed.Length; i++) {

			EdgeRed[i].gameObject.SetActive(false);
			EdgeGreen[i].gameObject.SetActive(true);

		}

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
