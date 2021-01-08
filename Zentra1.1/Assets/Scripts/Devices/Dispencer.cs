using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispencer : MonoBehaviour /* : Device*/
{
    public int ammo;

    [SerializeField]
    private List<GameObject> indicators;

    //public override void Operate()
    //{
    //    throw new System.NotImplementedException();
    //}

    private void Awake()
    {
        if(indicators.Count > 0)
        for(int i = 0; i < ammo; i++)
        {
            indicators[i].GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor && actor.IsGunActive())
        {
            Managers.actorManager.actor.SetAmmo(ammo);
        }
    }
}