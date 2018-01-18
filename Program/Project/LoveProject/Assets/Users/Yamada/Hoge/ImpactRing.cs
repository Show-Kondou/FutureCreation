using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///	衝撃波リングエフェクト
/// </summary>
public class ImpactRing : MonoBehaviour {

	private const float SPEED = 5.0F;
	private Vector3 scalingValue = new Vector3(SPEED, 0, SPEED);
	
	private GameObject child;
	private SpriteRenderer childSprite;

	// Use this for initialization
	void Start () {
		//	子取得
		child = transform.GetChild(0).gameObject;
		childSprite = child.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		Kakudai();
		SubAlpha();
	}


	/// <summary>
	///	拡大してく
	/// </summary>
	private void Kakudai(){
		transform.localScale += scalingValue * Time.deltaTime;
	}


	/// <summary>
	///	アルファ値を減らしていく
	/// </summary>
	private void SubAlpha(){
		var col = childSprite.color;
		if(col.a <= 0) Destroy(this.gameObject);

		col.a -= Time.deltaTime;
		childSprite.color = col;
	}
}
