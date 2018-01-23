using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePop : MonoBehaviour {

	[Header("こいつがお菓子を生成できる確率")]
	public uint popPriority;


	void Awake(){
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
		// ItemManager.Instance.Pop( type, transform.position);
		StartCoroutine( EventPop( type ) );
	}


	IEnumerator EventPop(ItemManager.ItemType type) {

		var pos = transform.position;
		pos.y += 1.2F;
		ParticleManager.Instance.PlayParticle(ParticleManager.ParticleName.Pop, pos );
		yield return new WaitForSeconds( 0.8F );
		ItemManager.Instance.Pop( type, transform.position );

	}
	


}
