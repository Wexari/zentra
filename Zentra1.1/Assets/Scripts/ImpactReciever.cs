using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactReciever : MonoBehaviour
{
    float mass = 2.0F; // defines the character mass
    public Vector3 impact = Vector3.zero;
    private CharacterController character;
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.2F) 
            character.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
#warning переделать
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        //if(impact.x > 0)
        //    impact.x -= Time.deltaTime;
        //if (impact.y > 0)
        //    impact.y -= Time.deltaTime * 5;
        //if (impact.z > 0)
        //    impact.z -= Time.deltaTime;
    }
    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
        {
            dir.y = -dir.y; // reflect down force on the ground
        }

        impact += dir.normalized * force / mass;
    }
}