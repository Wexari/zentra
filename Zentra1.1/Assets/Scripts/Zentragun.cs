using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZentragunMode
{
    LOW, MEDIUM, HIGH
}
public class Zentragun : MonoBehaviour
{
    [SerializeField]
    private Projectile projectile;
    public ZentragunMode mode;
    public int curAmmo;
    public int maxAmmo;
    public float projectileSpeed;
    public float projectileDestrTime;



    [SerializeField]
    private List<GameObject> ammoIndicators;
    [SerializeField]
    private List<GameObject> modeIndicators;
    [SerializeField]
    private LineRenderer laser;
    [SerializeField]
    private Transform firePoint;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip emptySound;
    [SerializeField]
    private AudioClip reloadSound;
    [SerializeField]
    private AudioClip laserSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        OnAmmoAmountChange();
        OnZentragunModeChange();
    }

    private void Update()
    {
        if(laser.enabled)
        {
            laser.SetPosition(0, laser.gameObject.transform.position);
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
            Physics.Raycast(ray, out hit);
            Vector3 pos = hit.point;
            laser.SetPosition(1, pos);
            laser.SetWidth(0.1f, 0.1f);
        }
    }

    public void Shoot(bool shootForward)
    {
        if (curAmmo > 0)
        {
            Projectile projTmp = Instantiate(projectile);
            switch (mode)
            {
                case ZentragunMode.LOW:
                    {
                        UpdateAmmo(-1);
                        projTmp.multiplier = 1f;  /*1.5f;*/ /*2f;*/
                        break;
                    }
                case ZentragunMode.MEDIUM:
                    {
                        if(curAmmo >= 2)
                        {
                            UpdateAmmo(-2);
                            projTmp.multiplier = 1.5f; /*3f;*/ /*5f;*/
                        }
                        else
                        {
                            goto default;
                        }

                        break;
                    }
                case ZentragunMode.HIGH:
                    {
                        if (curAmmo >= 3)
                        {
                            UpdateAmmo(-3);
                            projTmp.multiplier = 2.5f; /*5f;*/ /*7f;*/
                        }
                        else
                        {
                            goto case ZentragunMode.MEDIUM;
                        }
                        break;
                    }
                default:
                    {
                        UpdateAmmo(-1);
                        projTmp.multiplier = 1;
                        break;
                    }
            }
            projTmp.transform.position = firePoint.transform.position;
            if (shootForward)
            {
                projTmp.rigidBody.velocity = firePoint.transform.forward * projectileSpeed;
            }
            else
            {
                int x = Screen.width / 2;
                int y = Screen.height / 2;

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
                Physics.Raycast(ray, out hit);
                Vector3 pos = hit.point;
                Debug.Log(pos);
                projTmp.transform.LookAt(pos);
                projTmp.rigidBody.velocity = projTmp.transform.forward * projectileSpeed;
            }
            projTmp.destructionTime = projectileDestrTime;

            Managers.soundManager.PlaySoundEffectAt(fireSound, audioSource);
        }
        else
        {
            Managers.soundManager.PlaySoundEffectAt(emptySound, audioSource);
        }
    }

    public void UpdateAmmo(int amount)
    {
        curAmmo += amount;
        if (curAmmo > maxAmmo)
        {
            curAmmo = maxAmmo;
        }
        if (curAmmo < 0)
        {
            curAmmo = 0;
        }

        OnAmmoAmountChange();
    }

    public void SetAmmo(int amount)
    {
        if(amount > curAmmo)
        {
            OnAmmoIncrease();
        }
        curAmmo = amount;
        if (curAmmo > maxAmmo)
        {
            curAmmo = maxAmmo;
        }
        if (curAmmo < 0)
        {
            curAmmo = 0;
        }

        OnAmmoAmountChange();
    }

    public void SwitchZentragunMode(bool forward)
    {
        if(forward)
        {
            mode++;
        }
        else
        {
            mode--;
        }
        if((int)mode > 2)
        {
            mode = 0;
        }
        if((int)mode < 0)
        {
            mode = (ZentragunMode)2;
        }
        OnZentragunModeChange();
    }

    public void SwitchZentragunLaser()
    {
        laser.enabled = !laser.enabled;
        if(laserSound)
        Managers.soundManager.PlaySoundEffectAt(laserSound, audioSource);
    }

    public void SwitchZentragunLaser(bool enable)
    {
        laser.enabled = enable;
        if (laserSound)
        Managers.soundManager.PlaySoundEffectAt(laserSound, audioSource);
    }

    private void OnAmmoAmountChange()
    {
        foreach(GameObject obj in ammoIndicators)
        {
            obj.GetComponent<Renderer>().material.color = Color.white;
        }

        for(int i = 0; i < curAmmo; i++)
        {
            ammoIndicators[i].GetComponent<Renderer>().material.color = Color.green;
        }

    }

    private void OnAmmoIncrease()
    {
        if (reloadSound)
            Managers.soundManager.PlaySoundEffectAt(reloadSound, audioSource);
    }

    private void OnZentragunModeChange()
    {
        switch (mode)
        {
            case ZentragunMode.LOW:
                {
                    foreach(GameObject indicator in modeIndicators)
                    {
                        indicator.GetComponent<Renderer>().material.color = Color.green;
                    }
                    //laser.SetColors(Color.green, Color.green);
                    laser.material.color = Color.green;
                    break;
                }
            case ZentragunMode.MEDIUM:
                {
                    foreach (GameObject indicator in modeIndicators)
                    {
                        indicator.GetComponent<Renderer>().material.color = Color.yellow;
                        //laser.SetColors(Color.yellow, Color.yellow);
                        laser.material.color = Color.yellow;
                    }
                    break;
                }
            case ZentragunMode.HIGH:
                {
                    foreach (GameObject indicator in modeIndicators)
                    {
                        indicator.GetComponent<Renderer>().material.color = Color.red;
                        //laser.SetColors(Color.red, Color.red);
                        laser.material.color = Color.red;
                    }
                    break;
                }
            default:
                {
                    foreach (GameObject indicator in modeIndicators)
                    {

                    }
                    break;
                }
        }
    }


}