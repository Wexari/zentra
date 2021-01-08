using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : Device
{
    public float rotateTime;
    public float delay;
    public bool activated;
    public bool constant;
    public MoverDirection direction;

    private bool operating;

    private float timer;

    [SerializeField]
    private List<GameObject> objs;
    [SerializeField]
    private List<float> rotateRates;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !constant)
        {
            if (!operating)
                StartCoroutine(OperateRotater());
        }

        if (constant && !operating)
        {
            StartCoroutine(OperateRotater());
        }
    }

    IEnumerator OperateRotater()
    {
        operating = true;
        if (!activated)
        {
            timer = 0f;
            while (timer <= rotateTime)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < objs.Count; i++)
                {
                    Vector3 rot = objs[i].transform.localRotation.eulerAngles;
                    switch (direction)
                    {
                        case MoverDirection.X:
                            {
                                rot.x += rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Y:
                            {
                                rot.y += rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Z:
                            {
                                rot.z += rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    objs[i].transform.localRotation = Quaternion.Euler(rot);
                }
                yield return new WaitForSeconds(delay);
            }
        }
        else
        {
            timer = 0f;
            while (timer <= rotateTime)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < objs.Count; i++)
                {
                    Vector3 rot = objs[i].transform.localRotation.eulerAngles;
                    switch (direction)
                    {
                        case MoverDirection.X:
                            {
                                rot.x -= rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Y:
                            {
                                rot.y -= rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Z:
                            {
                                rot.z -= rotateRates[i] * Time.deltaTime;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    objs[i].transform.localRotation = Quaternion.Euler(rot);
                }
                yield return new WaitForSeconds(delay);
            }
        }
        activated = !activated;

        operating = false;
    }

    public override void Operate()
    {
        if (!operating)
            StartCoroutine(OperateRotater());
    }
}