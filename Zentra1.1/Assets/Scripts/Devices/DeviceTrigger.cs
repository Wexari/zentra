using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField]
    private List<Device> devices;
    [SerializeField]
    private bool oneOff;
    [SerializeField]
    private AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if (actor)
        {
            foreach (Device device in devices)
            {
                //if(!device.operating)
                //{
                    device.Operate();
                    if (sound)
                    {
                        Managers.soundManager.PlaySoundEffectAt(sound, GetComponent<AudioSource>());
                    }
                //}
            }
            if (oneOff)
            {
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }
    }
}
