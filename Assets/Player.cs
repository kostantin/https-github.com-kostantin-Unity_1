using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 playerVelocity;

    private float playerSpeed = 3.0f;

    private float gravityValue = -9.81f;

    public Transform target;

    Transform tm;

    public FixedJoystick FixedJoystick;

    public bool isTarget;


    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        tm = transform;
    }

    void Update()
    {
        if (MoveJoy(FixedJoystick.Horizontal, FixedJoystick.Vertical) == 0.0f)
        {
            MoveToPoint();
        }
        else
        {
            isTarget = false;
            if(target.gameObject.activeSelf)
            {
                target.gameObject.SetActive(false);
            }
        }
    }

    public float MoveJoy(float Horizontal, float Vertical)
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Horizontal, 0.0f, Vertical);
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        return Horizontal;
    }

    void MoveToPoint()
    {
        if(!isTarget)
        return;


        if (target.position == tm.position)
        {
            target.gameObject.SetActive(false);
            isTarget = false;
            return;
        }

        Vector3 Diff = target.position - tm.position;
        Vector3 Dir = Diff.normalized * 5f * Time.deltaTime;

        if (Dir.sqrMagnitude < Diff.sqrMagnitude)
        {
            controller.Move (Dir);
        }
        else
        {
            controller.Move (Diff);
        }

        bool groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
