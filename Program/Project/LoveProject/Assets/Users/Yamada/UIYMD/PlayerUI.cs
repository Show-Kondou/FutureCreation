using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	[Header("プレイヤー"),SerializeField]
	private Player _player;

	uint playerID;
	int hp;
	ItemManager.ItemType type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnTriggerEnter(Collider other){
		
	}


}
