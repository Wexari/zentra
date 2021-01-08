using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5f;
    public float explosionPower = 10f;
    public float multiplier;

#warning сделать одно отталкивание зависящее от расстояния к эпицентру
#warning зависимость от квадрата расстояния
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            ImpactReciever receiver = hit.transform.GetComponent<ImpactReciever>();
            if (receiver)
            {
                var dir = hit.transform.position - transform.position;
                var force = Mathf.Clamp(explosionPower / 3, 0, 15);
                receiver.AddImpact(dir, force);
                receiver.GetComponent<ActorController>().IncreaseVelocity(multiplier);
            }
            
        }
    }
}