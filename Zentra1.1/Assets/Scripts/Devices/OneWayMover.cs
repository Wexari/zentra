using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMover : Device
{
    public MoverDirection direction;
    public float moveTime;
    public float returnDelay;

    private float timer;

    [SerializeField]
    private List<GameObject> objs;
    [SerializeField]
    private List<float> moveRates;
    private List<Vector3> originalPositions;

    private void Awake()
    {
        originalPositions = new List<Vector3>();
        foreach(GameObject obj in objs)
        {
            originalPositions.Add(new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z));
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(!operating)
    //    {
    //        Operate();
    //    }

    //}

    public override void Operate()
    {
        if(!operating)
        StartCoroutine(OperateMover());
    }

    IEnumerator OperateMover()
    {
        operating = true;

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
                yield return new WaitForEndOfFrame();

            }
            yield return new WaitForSeconds(returnDelay);
        for(int i = 0; i < objs.Count; i++)
        {
            objs[i].transform.position = originalPositions[i];
        }
        

        operating = false;
    }
}
