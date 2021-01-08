using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float destructionTime;
    public bool halflife;
    [SerializeField]
    private Explosion ex;
    [SerializeField]
    private string ignoreTag;

    [SerializeField]
    private AudioClip destructionSound;
    
    public float multiplier;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        halflife = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != ignoreTag && !halflife)
        StartCoroutine(Destruction());
    }

    private IEnumerator Destruction()
    {
        halflife = true;
        rigidBody.velocity = Vector3.zero;
        ex.enabled = true;
        ex.multiplier = multiplier;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        Managers.soundManager.PlaySoundEffectAt(destructionSound, GetComponent<AudioSource>());
        yield return new WaitForSeconds(destructionTime);
        ex.enabled = false;
        while(GetComponent<AudioSource>().isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}