using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public List<Device> devices;
    public bool received;
    public bool repeatable;
    public float repeatTime;
    [SerializeField]
    private GameObject receiverIndicator;
    [SerializeField]
    private AudioClip receiveSound;

    private Renderer renderer;
    private void Awake()
    {
        renderer = receiverIndicator.GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>() && !received)
        {
            received = true;
            OnReceived();
            if(repeatable)
            {
                StartCoroutine(Repeat());
            }
        }
    }

    private void OnReceived()
    {
        foreach (Device device in devices)
        {
            device.Operate();
        }
        renderer.material.color = Color.green;
        Managers.soundManager.PlaySoundEffectAt(receiveSound, GetComponent<AudioSource>());
    }

    private IEnumerator Repeat()
    {
        yield return new WaitForSeconds(repeatTime);
        received = false;
        renderer.material.color = Color.red;
    }
}