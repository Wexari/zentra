using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : Device
{
    public List<GameObject> objects;
    public override void Operate()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
