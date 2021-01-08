using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZentragunStand : MonoBehaviour
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if(actor && !actor.IsGunActive())
        {
            actor.SwitchGun();
            gun.SetActive(!gun.activeSelf);
            gun.SetActive(false);
            Managers.soundManager.PlayCloseSoundEffect(pickupSound);
        }
    }
}
