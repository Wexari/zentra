using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoverDirection { X, Y, Z }

public class Mover : Device
{
    public float moveTime;
    public float delay;
    public bool activated;
    public bool constant;
    public MoverDirection direction;

    private float timer;

    [SerializeField]
    private List<GameObject> objs;
    [SerializeField]
    private List<float> moveRates;
    [SerializeField]
    private AudioClip moveSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !constant)
        {
            if (!operating)
                StartCoroutine(OperateMover());
        }

        if (constant && !operating)
        {
            StartCoroutine(OperateMover());
        }
    }

    IEnumerator OperateMover()
    {
        operating = true;
        if (!activated)
        {
            if(moveSound)
            Managers.soundManager.PlaySoundEffectAt(moveSound, GetComponent<AudioSource>());
            timer = 0f;
            while (timer <= moveTime)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < objs.Count; i++)
                {
                    Vector3 pos = objs[i].transform.localPosition;
                    switch (direction)
                    {
                        case MoverDirection.X:
                            {
                                pos.x += moveRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Y:
                            {
                                pos.y += moveRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Z:
                            {
                                pos.z += moveRates[i] * Time.deltaTime;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    objs[i].transform.localPosition = pos;
                }
                yield return new WaitForSeconds(delay);
            }
        }
        else
        {
            if (moveSound)
                Managers.soundManager.PlaySoundEffectAt(moveSound, GetComponent<AudioSource>());
            timer = 0f;
            while (timer <= moveTime)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < objs.Count; i++)
                {
                    Vector3 pos = objs[i].transform.localPosition;
                    switch (direction)
                    {
                        case MoverDirection.X:
                            {
                                pos.x -= moveRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Y:
                            {
                                pos.y -= moveRates[i] * Time.deltaTime;
                                break;
                            }
                        case MoverDirection.Z:
                            {
                                pos.z -= moveRates[i] * Time.deltaTime;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    objs[i].transform.localPosition = pos;
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
            StartCoroutine(OperateMover());
    }
}