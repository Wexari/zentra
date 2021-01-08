using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : Device
{
    public float time;

    [SerializeField]
    private List<Device> devices;
    [SerializeField]
    private AudioClip tickSound;
    [SerializeField]
    private TextMeshPro text;

    public override void Operate()
    {
        if (!operating)
        {
            StartCoroutine(TickTime());
        }
        else
        {
            StopAllCoroutines();
            text.text = "0";
        }

    }

    private IEnumerator TickTime()
    {
        operating = true;
        StartCoroutine(Sound());
        yield return new WaitForSeconds(time);
        foreach(Device device in devices)
        {
            if(device.GetType() == typeof(Portal))
            {
                device.GetComponent<Portal>().SetActor(Managers.actorManager.actor);
            }
            device.Operate();
        }
        operating = false;
    }

    private IEnumerator Sound()
    {
        float ticksLeft = time;
        while(operating)
        {
            ticksLeft--;
            Managers.soundManager.PlayCloseSoundEffect(tickSound);
            if(text)
            {
                text.text = ticksLeft.ToString();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
