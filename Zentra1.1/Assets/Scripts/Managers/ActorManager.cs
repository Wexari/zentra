using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { private set; get; }

    [SerializeField]
    public ActorController actor;

    public void Startup()
    {
        Debug.Log("Actor manager starting...");

        status = ManagerStatus.Started;
    }

    public void SwitchControl()
    {
        if(!actor.freeze)
        {
            actor.freeze = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            actor.freeze = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
