using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZentraWatcher : Device
{
    [SerializeField]
    List<Zentragun> guns;
    public ZentragunMode mode;
    public float fireDelay;
    private float timer;
    private bool iteration;
    public GameObject target;

    public void Awake()
    {
        foreach (Zentragun gun in guns)
        {
            gun.UpdateAmmo(5);
            gun.mode = mode;
            gun.SwitchZentragunLaser(false);
        }
    }

    private void Update()
    {

    }

    //public void Update()
    //{
    //    if(operating && !iteration)
    //    {
    //        iteration = true;
    //        timer = 0;
    //        while(timer <= fireDelay)
    //        {
    //            timer += Time.deltaTime;
    //        }
    //        guns[0].Shoot();
    //        iteration = false;
    //    }

    //    //if(operating && !iteration)
    //    //{
    //    //    iteration = true;
    //    //    foreach(Zentragun gun in guns)
    //    //    {
    //    //        timer = 0;
    //    //        gun.UpdateAmmo(5);
    //    //        while (timer <= fireDelay)
    //    //        {
    //    //            timer += Time.deltaTime;
    //    //        }
    //    //        gun.Shoot();
    //    //    }
    //    //    iteration = false;

    //    //}
    //}

    public override void Operate()
    {
        target = Managers.actorManager.actor.gameObject;
        operating = !operating;
        if (operating)
        {
            StartCoroutine(Shoot());
            StartCoroutine(Watch());
            Debug.Log("shootin");
        }
        else
        {
            Debug.Log("relooodin");
        }

    }

    private IEnumerator Shoot()
    {
        while (operating)
        {
            foreach (Zentragun gun in guns)
            {
                gun.UpdateAmmo(5);
                gun.Shoot(true);
                yield return new WaitForSeconds(fireDelay);
            }
        }
    }

    private IEnumerator Watch()
    {
        while (operating)
        {
            foreach (Zentragun gun in guns)
            {
                gun.gameObject.transform.LookAt(target.transform);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}