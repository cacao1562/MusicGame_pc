using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingMonster : MonoBehaviour {

	public Image monsterImg;
	public Transform parentObj;
	public Vector3[] positionArr = new Vector3[4];
	public List<Image> monsterList = new List<Image>();
	public Sprite[] spriteArr = new Sprite[7];
	private int cloneNum = 3;
	private int randomNUm;
	private int randomNUm2;
	private float time;

	// Use this for initialization
	void Start () {

		for(int i=0; i < cloneNum; i++) {

			for(int j=0; j < positionArr.Length; j++) {
				int r = Random.Range(0,7);
				monsterList.Add(Instantiate(monsterImg, Vector3.zero, Quaternion.identity) );
				monsterList[(i*positionArr.Length)+j].sprite = spriteArr[r];
				monsterList[(i*positionArr.Length)+j].transform.SetParent(parentObj);
				monsterList[(i*positionArr.Length)+j].gameObject.SetActive(false);

			}
		}

		for(int i=0; i < cloneNum; i++) {

			for(int j=0; j < positionArr.Length; j++) {

				monsterList[(i*positionArr.Length)+j].transform.localPosition = positionArr[j];
				monsterList[(i*positionArr.Length)+j].rectTransform.localScale = new Vector3(1,1,1);
			}

		}

		

		StartCoroutine( startFalling() );
		
	}

	IEnumerator startFalling() {

		randomNUm = Random.Range(0, cloneNum*positionArr.Length);
		randomNUm2 = Random.Range(0, cloneNum*positionArr.Length);


		if(monsterList[randomNUm].gameObject.activeSelf) {

		}else{

			monsterList[randomNUm].gameObject.SetActive(true);

		}
		if(monsterList[randomNUm2].gameObject.activeSelf) {

		}else{

			monsterList[randomNUm2].gameObject.SetActive(true);

		}
		yield return null;

		// yield return new WaitForSeconds(1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		
		time += Time.deltaTime;

		if(time > 1 ){

			StartCoroutine( startFalling() );
			time = 0; 

		}
	}

}
