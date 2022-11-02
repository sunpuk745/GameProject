using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutsceneStateController : MonoBehaviour
{
    private Animator anim;

    public bool IsRunning = false;

    private void update()
    {
        if(IsRunning == true)
        {
            anim.SetTrigger("Running");
        }
    }
}
