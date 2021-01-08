using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Device
{
    public Transform wayPoint;
    private ActorController actor;

    [SerializeField]
    private AudioClip portalSound;
    //private GameObject otherGO;
    private void OnTriggerEnter(Collider other)
    {
        actor = other.GetComponent<ActorController>();
        if (actor)
        {
            //otherGO = other.gameObject;
            Operate();
        }
    }
    public override void Operate()
    {
        actor.transform.localPosition = wayPoint.position;
        //actor.transform.rotation = wayPoint.rotation;
        //otherGO.transform.localPosition = wayPoint.position;
        //otherGO.transform.rotation = wayPoint.rotation;
        Managers.soundManager.PlayCloseSoundEffect(portalSound);
    }

    public void SetActor(ActorController actor)
    {
        this.actor = actor;
    }
}