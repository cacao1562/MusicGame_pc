using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMonster : MonoBehaviour {

	private Vector3 originalPos;
	private float speed = 0;
	private float time;
	public Image[] EdgeImage = new Image[4];
	public Image[] EdgeImage2 = new Image[4];
	public SocketManager socketManager;
	public EdgeManager edgeManager;
	public GameObject particle;
	private int spNum;

	public ParticleSystem ps;
	void Start () {
		Debug.Log(" move ");
		spNum = int.Parse(GetComponent<Image>().sprite.name);
		
		// Debug.Log("sprite name = " + name );
		for(int i=0; i < EdgeImage.Length; i++) {

			EdgeImage[i].gameObject.SetActive(false);
			EdgeImage2[i].gameObject.SetActive(false);

		}
		
		originalPos = transform.localPosition;
		speed = Random.Range(0.1f,0.5f);	

		// Debug.Log("LoPOS = " + transform.localPosition );
		// Debug.Log("POS = " + transform.position );
	}
	
	// Update is called once per frame
	void Update () {
		
		time += Time.deltaTime;
		
		if(transform.localPosition.y < -400) {

			edgeManager.missMonster();
			transform.localPosition = originalPos;
			time = 0;
			gameObject.SetActive(false);

		}

		transform.Translate(Vector2.down * speed * time);
	}

	// void OnCollisionEnter2D(Collision2D collisionInfo)
	// {

	// 	if(collisionInfo.collider.tag == "Enemy"){

	// 		socketManager.mSore += 1;
	// 		socketManager.catchSocket();
			
	// 		StartCoroutine( showParticle() );
	// 		edgeManager.catchMonster();
			
	// 		transform.localPosition = originalPos;
	// 		time = 0;
	// 		gameObject.SetActive(false);

	// 	}

	// }


	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.tag == "Enemy"){
		
			socketManager.mSore += 1;
			socketManager.catchSocket(spNum);
			
			StartCoroutine( showParticle() );
			edgeManager.catchMonster();
			
			transform.localPosition = originalPos;
			time = 0;
			gameObject.SetActive(false);

		}
	}


	IEnumerator showParticle() {

		particle.SetActive(true);
		ps.Play();
		yield return new WaitForSeconds(0.5f);
		ps.Stop();
		particle.SetActive(false);
		
	}

}
