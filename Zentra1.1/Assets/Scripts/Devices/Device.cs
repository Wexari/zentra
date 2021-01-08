using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    public bool operating;
    public abstract void Operate();
    //maybe Ienumerator?
}