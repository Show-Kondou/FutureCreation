using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//	Shootがこいつを投げられる弾の数生成する。
public class Bullet : Item {

	#region Member

	bool isAction = false;	//	飛んでいくよ

	//[SerializeField]
	//float speed = 2;	//	飛んでくスピード
    
	//float hogecount = 0;    //	テスト用

    //  開始地点
    Vector3 offset = Vector3.zero;

    //  落下地点
    Vector3 target = Vector3.zero;

    //  角度
    float deg = 0;

    #endregion Member



    #region Method

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<SphereCollider>();
    }

        // Update is called once per frame
    void Update () {

		if(isAction == false) return;
		//	以下、動作
		
		//	Z方向へ直進
		//transform.position += transform.forward * speed * Time.deltaTime;

		//テスト動作
		//{
		//	hogecount += 1 * Time.deltaTime;
		//	if(hogecount >= 5){
		//		hogecount = 0;
		//		ItemManager.Instance.NotActive(this);
		//	}
		//}
	}
	

	/// <summary>
	/// 
	/// </sammary>
	public override void Action(){
		Debug.LogError("Bullet.cs Action()ここは呼ばれないはずよ");
	}

	

	/// <summary>
	/// 
	/// </sammary>
	public override int EatItem(){
		Debug.LogError("Bullet.cs EatItem()ここは呼ばれないはずよ");
		return healPoint;
	}
	


    /// <summary>
    /// 
    /// </summary>
    /// <param name="_offset">運動開始地点</param>
    /// <param name="_target"></param>
    /// <param name="_deg"></param>
	public void ActionBullet(Vector3 _offset, Vector3 _target, float _deg){
		isAction = true;	//	行動可能

        offset = _offset;
        target = transform.TransformPoint(_target - offset);
        deg = _deg;

        Debug.Log("off:" + offset);
        Debug.Log("tar:" + target);

        StartCoroutine("Throw");
	}



	/// <summary>
	/// 衝突検知
	/// </sammary>
	void OnTriggerEnter(Collider other){

		//	盾アイテム、またはプレイヤーのみ判定
		var other_tag = other.gameObject.tag;

		//	タグで分岐
		if(other_tag == "Item"){//	アイテム

			//	当たったオブジェクトがアイテムだった時、アイテム取得
			var item = other.gameObject.GetComponent<Item>();

			if(item.IsPicked == false) return;// 相手が、落ちているオブジェクトなら判定しない

			//	Typeで分岐
			switch(item.Type){
				//	クッキー
				case ItemManager.ItemType.Cookie:
					gameObject.SetActive(false);	//	非表示へ
				break;
				
				//	せんべい
				case ItemManager.ItemType.Senbei:
					gameObject.SetActive(false);	//	非表示へ
				break;
			}
		}else if(other_tag == "Player"){//	プレイヤー
			var other_id = other.GetComponent<Player>().PlayerID;
			
			//	拾ったプレイヤーと同じなので、判定しない
			if(other_id == playerID) return;

			//gameObject.SetActive(false);	//	非表示へ
		}



	}

    //TODO:　衝突時の自壊




    /// <summary>
    /// 放物挙動コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Throw()
    {

        float b = Mathf.Tan(deg * Mathf.Deg2Rad); //  角度を何とかしている
        float a = (target.y - b * target.z) / (target.z * target.z);    //  ？？？

        //  傾きの計算
        float katamuki = (target.z - offset.z) / (target.x - offset.x);

        for (float z = 0; z <= target.z; z += 0.5f)
        {

            float y = a * z * z + b * z;    //  Y軸の位置計算
            float x = z / katamuki;         //  X軸の位置計算 x = y/a

            //  座標反映
            transform.localPosition = new Vector3(x, y, z) + offset;
            yield return null;
        }

        //  到達後にここにくるから、ここで爆発とか
        coll.GetComponent<SphereCollider>().radius = 5;

		Destroy( this.gameObject );
    }

    #endregion Method

}
