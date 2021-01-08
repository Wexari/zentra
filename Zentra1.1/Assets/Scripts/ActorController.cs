using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public CharacterController controller;

    public bool freeze;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool hitsCeiling;

    //
    public float mouseSensitivity = /*150f;*/ /*300f;*/ /*500f;*/ 300f;

    float xRotation = 0f;
    [SerializeField]
    private new GameObject camera;
    [SerializeField]
    private Zentragun gun;


    private void Update()
    {
        if(!freeze)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            hitsCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance / 2, groundMask);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1))
            {
                if (isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

            }

            velocity.y += gravity * Time.deltaTime;

            if (hitsCeiling)
            {
                velocity.y = gravity / 4;
                GetComponent<ImpactReciever>().impact.y = 0f;
                //velocity.y += gravity * Time.deltaTime;
                //GetComponent<ImpactReciever>().impact.y -= gravity * Time.deltaTime;
                //controller.Move(Vector3.zero);
            }

            if(!isGrounded)
            {
                velocity.y -= Time.deltaTime;
            }



            controller.Move(velocity * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            transform.Rotate(Vector3.up * mouseX);

            if(gun.gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Shoot(false);
                }

                if(Input.mouseScrollDelta.y > 0)
                {
                    gun.SwitchZentragunMode(true);
                }
                if (Input.mouseScrollDelta.y < 0)
                {
                    gun.SwitchZentragunMode(false);
                }
                if(Input.GetMouseButtonDown(2))
                {
                    Debug.Log("wheeel");
                    gun.SwitchZentragunLaser();
                }

                //Debug.Log(Input.mouseScrollDelta);
            }
        }
    }

    public void SwitchGun()
    {
        gun.gameObject.SetActive(!gun.gameObject.activeSelf);
    }

    public void SwitchGun(bool enable)
    {
        gun.gameObject.SetActive(enable);
    }

    public bool IsGunActive()
    {
            return gun.gameObject.activeSelf;
    }

    public void SetAmmo(int value)
    {
        gun.SetAmmo(value);
    }

    //public void IncreaseVelocity(float value)
    //{
    //    velocity *= value;
    //}

    public void IncreaseVelocity(float value)
    {
        velocity.y += value;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ActorController : MonoBehaviour
//{

//    public CharacterController controller;

//    public bool freeze;

//    public float speed = 12f;
//    public float gravity = -9.81f;
//    public float jumpHeight = 3f;

//    public Transform groundCheck;
//    public Transform ceilingCheck;
//    public float groundDistance = 0.4f;
//    public LayerMask groundMask;

//    Vector3 velocity;
//    bool isGrounded;
//    bool hitsCeiling;

//    //
//    public float mouseSensitivity = 400f;

//    float xRotation = 0f;
//    [SerializeField]
//    private new GameObject camera;
//    [SerializeField]
//    private Zentragun gun;


//    private void Update()
//    {
//        if (!freeze)
//        {
//            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
//            hitsCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance / 2, groundMask);
//            //using "charCon.IsGrounded" sucks ass for some reason duh
//            //(and that reason is that you can jump only when moving (?????))
//            float x = Input.GetAxis("Horizontal");
//            float z = Input.GetAxis("Vertical");

//            if (isGrounded && velocity.y < 0)
//            {
//                velocity.y = -2f;
//            }

//            Vector3 move = transform.right * x + transform.forward * z;

//            controller.Move(move * speed * Time.deltaTime);

//            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1))
//            {
//                if (isGrounded)
//                {
//                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//                }

//            }
//            if (hitsCeiling)
//            {
//                velocity.y += gravity * Time.deltaTime;
//                //GetComponent<ImpactReciever>().impact.y += gravity * Time.deltaTime;
//            }

//            velocity.y += gravity * Time.deltaTime;

//            controller.Move(velocity * Time.deltaTime);
//            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
//            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

//            xRotation -= mouseY;
//            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

//            transform.Rotate(Vector3.up * mouseX);

//            if (gun.gameObject.activeSelf)
//            {
//                if (Input.GetMouseButtonDown(0))
//                {
//                    gun.Shoot();
//                }

//                if (Input.mouseScrollDelta.y > 0)
//                {
//                    gun.SwitchZentragunMode(true);
//                }
//                if (Input.mouseScrollDelta.y < 0)
//                {
//                    gun.SwitchZentragunMode(false);
//                }

//                //Debug.Log(Input.mouseScrollDelta);
//            }
//        }
//    }

//    public void SwitchGun()
//    {
//        gun.gameObject.SetActive(!gun.gameObject.activeSelf);
//    }

//    public bool IsGunActive()
//    {
//        return gun.gameObject.activeSelf;
//    }

//    public void SetAmmo(int value)
//    {
//        gun.SetAmmo(value);
//    }

//    //public void IncreaseVelocity(float value)
//    //{
//    //    velocity *= value;
//    //}

//    public void IncreaseVelocity(float value)
//    {
//        velocity.y += value;
//    }
//}