using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 放物運動クラス
/// </summary>
public class Parabola : MonoBehaviour {

    //  開始地点
    Vector3 offset = Vector3.zero;

    //  落下地点
    Vector3 target = Vector3.zero;

    //  角度
    float deg = 0;

    /// <summary>
    /// 外部からのコルーチン呼び出しメソッド
    /// </summary>
    /// <param name="_offset"></param>
    /// <param name="_target"></param>
    /// <param name="_deg"></param>
    public void CallThrow(Vector3 _offset, Vector3 _target, float _deg)
    {
        offset = _offset;
        target = _target;
        deg = _deg;

        //  放物運動コルーチン呼び出し
        StartCoroutine("Throw");
    }

    /// <summary>
    /// 放物挙動コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator Throw()
    {

        float b = Mathf.Tan(deg * Mathf.Deg2Rad); //  角度を何とかしている
        float a = (target.y - b * target.z) / (target.z * target.z);    //  ？？？

        //  傾きの計算
        float katamuki = (target.z - transform.localPosition.z) / (target.x - transform.localPosition.x);

        for (float z = 0; z <= target.z; z += 0.5f)
        {

            float y = a * z * z + b * z;    //  Y軸の位置計算
            float x = z / katamuki;         //  X軸の位置計算 x = y/a

            //  座標反映
            transform.position = new Vector3(x, y, z) + offset;
            yield return null;
        }

        //  到達後にここにくるから、ここで爆発とか

    }
}
