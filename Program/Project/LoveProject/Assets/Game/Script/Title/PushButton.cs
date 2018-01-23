using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタン押してね的なガイドのクラス
/// </sammary>
public class PushButton : MonoBehaviour {


	#region Member
	private Image myImage = null;	//	画像

	#endregion Member


	#region Method
	
	// Use this for initialization
	void Start () {
		myImage = GetComponent<Image>();
	}
	


	// Update is called once per frame
	void Update () {
		Flashing();
		//Scaling();
	}



	/// <summary>
	/// 点滅
	/// </sammary>
	private void Flashing(){
		var col = myImage.color;
		col.a = Mathf.Abs( Mathf.Sin(Time.time) );
		myImage.color = col;
	}



	/// <summary>
	/// 拡縮
	/// </sammary>
	private void Scaling(){
		var scale = myImage.transform.localScale;
		scale.x = scale.y = 0.3F + Mathf.Abs( Mathf.Sin(Time.time) );
		myImage.transform.localScale = scale;
	}

	#endregion Method
}
