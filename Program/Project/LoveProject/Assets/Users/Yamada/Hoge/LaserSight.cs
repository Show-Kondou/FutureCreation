using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour {

    //  ラインレンダラー関係
    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();


    //  開始地点
    Vector3 offset = Vector3.zero;

    //  落下地点
    Vector3 target = Vector3.zero;

    //  角度
    float deg = 0;


    // Use this for initialization
    void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    

    public void DrawLine(Vector3 _offset, Vector3 _target, float _deg)
    {

        //  落下地点セット
        offset = _offset;
        target = _target;
        deg = _deg;

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
}
