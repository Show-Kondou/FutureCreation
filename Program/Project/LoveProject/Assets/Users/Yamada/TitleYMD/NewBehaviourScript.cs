using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : StateMachineBehaviour {


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("CameraTiltUp"))
        {
            Title.Instance.intencity += Time.deltaTime * 8;
            Title.Instance.cameraAnim.speed = 0;
        }
        else if (stateInfo.IsName("CameraTiltDown"))
        {
            Title.Instance.intencity -= Time.deltaTime * 8;
            Title.Instance.cameraAnim.speed = 0;
        }


    }
}
