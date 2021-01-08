//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Repeater : MonoBehaviour
//{
//    public float delay;
//    [SerializeField]
//    private List<Device> devices;
//    private float timer;
//    private bool iteration;

//    public void Update()
//    {
//        if(!iteration)
//        {
//            iteration = true;
//            foreach (Device device in devices)
//            {
//                timer = 0;
//                while (timer <= delay)
//                {
//                    timer += Time.deltaTime;
//                }
//                device.Operate();
//            }
//            iteration = false;
//        }

//    }
//}
