using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePop : MonoBehaviour {

	[Header("こいつがお菓子を生成できる確率"), SerializeField]
	private uint popPriority;


	/// <summary>
	///	指定した種類のお菓子をマネージャで生成
	/// </sammary>
	public void Pop(ItemManager.ItemType type){
		ItemManager.Instance.Pop( type, transform.position);
	}
	


}
