using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZentraTurret : Device
{
    [SerializeField]
    List<Zentragun> guns;
    public ZentragunMode mode;
    public float fireDelay;

    public void Awake()
    {
        foreach(Zentragun gun in guns)
        {
            gun.UpdateAmmo(5);
            gun.mode = mode;
            gun.SwitchZentragunLaser(false);
        }
    }

    public override void Operate()
    {
        operating = !operating;
        if (operating)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        while(operating)
        {
            foreach (Zentragun gun in guns)
            {
                gun.UpdateAmmo(5);
                gun.Shoot(true);
                yield return new WaitForSeconds(fireDelay);
            }
        }
    }
}