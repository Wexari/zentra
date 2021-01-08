using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    public AudioClip music;
    [SerializeField]
    public AudioClip ambience;

    public Transform GetSpawnPosition()
    {
        return spawnPosition;
    }

}
