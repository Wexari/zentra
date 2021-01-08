using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : Device
{
    [SerializeField]
    private Material material;
    public List<GameObject> objects;
    public float delay;
    public override void Operate()
    {
        StartCoroutine(Paint());
    }

    private IEnumerator Paint()
    {
        foreach (GameObject obj in objects)
        {
            yield return new WaitForSeconds(delay);
            obj.GetComponent<Renderer>().material = material;
        }
    }
}
