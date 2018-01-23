using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Maskをコンポーネントしたやつにつける
/// </summary>
public class MaskGauge : MonoBehaviour {

	private RawImage	rawImage;   //	RawImageのキャッシュ
	private Rect		uvRect;     //	RawImage.uvRectのキャッシュ
	private float		tmpX = 0.0f;//	x移動量蓄積用

	// Use this for initialization
	void Start () {
		rawImage = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {

		//	キャッシュ
		uvRect = rawImage.uvRect;

		//	UV操作
		tmpX += 0.1f * Time.deltaTime;  //	移動後のUV値
		uvRect.x = tmpX % 1.0f;			//	1.0fを超えたら0.0fに戻るやつ(∞防止)
		

		//	値を反映
		rawImage.uvRect = uvRect;

	}


	/// <summary>
	/// UV操作　Yのみ
	/// </summary>
	public void TransUV(float hp){

		if(rawImage == null) 
			rawImage = GetComponent<RawImage>();
		
		//	キャッシュ
		uvRect = rawImage.uvRect;

		//	UV操作 Y
		uvRect.y = 1 - (hp * 0.01f);

		//	値を反映
		rawImage.uvRect = uvRect;

	}
}
