using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabpla : MonoBehaviour {

    //  落下地点オブジェクト
    public GameObject DropPos;

    //  開始地点
    Vector3 offset = Vector3.zero;

    //  落下地点
    Vector3 target = Vector3.zero;

    //  角度
    float deg = 0;

    //  ラインレンダラー関係
    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();


    /// <summary>
    /// 放物挙動コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Throw(){

        float b = Mathf.Tan( deg * Mathf.Deg2Rad ); //  角度を何とかしている
        float a = (target.y - b * target.z) / (target.z * target.z);    //  ？？？

        //  傾きの計算
        float katamuki = (target.z - offset.z) / (target.x - offset.x);

        for (float z = 0; z <= target.z; z += 0.5f){

            float y = a * z * z + b * z;    //  Y軸の位置計算
            float x = z / katamuki;         //  X軸の位置計算 x = y/a

            //  座標反映
            transform.position = new Vector3(x, y, z) + offset;
            yield return null;
        }

        //  到達後にここにくるから、ここで爆発とか

    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_deg"></param>
    public void SetTarget(Vector3 _target, float _deg)
    {
        offset = transform.localPosition;
        target = _target;
        deg = _deg;
        
    }

    public void CallThrow()
    {
        StartCoroutine("Throw");
    }


	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha8))
        {
            offset = transform.localPosition;
            target = DropPos.transform.localPosition;// - offset;

            // 予測線の軌道をクリア
            renderLinePoints.Clear();

            float b = Mathf.Tan(60 * Mathf.Deg2Rad);
            float a = (target.y - b * target.z) / (target.z * target.z);

            float katamuki = (target.z - transform.localPosition.z) / (target.x - transform.localPosition.x);

            for (float z = 0; z <= target.z; z += 0.5f)
            {
                float y = a * z * z + b * z;
                float x = z / katamuki;

                Vector3 nextPosition = new Vector3(x, y, z) + offset;


                // 線のリストに加える
                renderLinePoints.Add(nextPosition);
            }
            // LineRenderer で描画
            lineRenderer.positionCount = renderLinePoints.Count;
            lineRenderer.SetPositions(renderLinePoints.ToArray());
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            transform.parent = null;
            SetTarget(DropPos.transform.localPosition, 60);
            CallThrow();
        }

    }


    public void DrawLine()
    {

        //  落下地点セット
        SetTarget(DropPos.transform.localPosition, 60);

        // 予測線の軌道をクリア
        renderLinePoints.Clear();

        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.z) / (target.z * target.z);

        float katamuki = (target.z - transform.localPosition.z) / (target.x - transform.localPosition.x);

        for (float z = 0; z <= target.z; z += 0.5f)
        {
            float y = a * z * z + b * z;
            float x = z / katamuki;

            Vector3 nextPosition = new Vector3(x, y, z) + offset;


            // 線のリストに加える
            renderLinePoints.Add(nextPosition);
        }
        // LineRenderer で描画
        lineRenderer.positionCount = renderLinePoints.Count;
        lineRenderer.SetPositions(renderLinePoints.ToArray());
    }



    //IEnumerator DrawPredictionLine()
    //{

    //    // 予測線の軌道をクリア
    //    renderLinePoints.Clear();
        

    //    float b = Mathf.Tan(deg * Mathf.Deg2Rad);
    //    float a = (target.y - b * target.z) / (target.z * target.z);
        
    //    for (float z = 0; z <= target.z; z += 0.3f)
    //    {

    //        float y = a * z * z + b * z;

    //        Vector3 nextPosition = new Vector3(0, y, z) + offset;


    //        // 線のリストに加える
    //        renderLinePoints.Add(nextPosition);
    //        yield return null; 
    //    }


    //    // LineRenderer で描画
    //    lineRenderer.positionCount = renderLinePoints.Count;
    //    lineRenderer.SetPositions(renderLinePoints.ToArray());

    //}
}
