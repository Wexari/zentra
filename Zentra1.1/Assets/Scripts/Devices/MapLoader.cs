using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField]
    private string mapName;
    private void OnTriggerEnter(Collider other)
    {
        ActorController actor = other.GetComponent<ActorController>();
        if (actor)
        {
            Managers.mapManager.LoadMap(mapName);
        }
        else
        {
            Debug.Log("i don't know you");
        }
    }
}
