using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePop : MonoBehaviour {

	[Header("こいつがお菓子を生成できる確率")]
	public uint popPriority;


	void Start(){
		//	自身でマネージャのリストに飛び込む
		StageManager.Instance.popPoint.Add(this);

#if UNITY_EDITOR	//	エディター実行時は見やすさのために、マネージャの子にする。
		transform.parent = StageManager.Instance.transform;
#endif

	}

	/// <summary>
	///	指定した種類のお菓子をマネージャで生成
	/// </sammary>
	public void Pop(ItemManager.ItemType type){
		ItemManager.Instance.Pop( type, transform.position);
	}
	


}
