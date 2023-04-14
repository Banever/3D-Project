using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private float Horizontalmove;

    private float Verticalmove;

    public float MoveSpeed;

    public CharacterController Player;

    public Transform groundcheck;

    public float groundDistance = 0.4f;

    public LayerMask groundmask;

    Vector3 velocity;

    private float gravity = -9.3f;

    bool IsGrounded;

    public Transform Firepoint;

    public GameObject BulletPreFab;

    void Shoot()
    {
        Instantiate(BulletPreFab, Firepoint.position, Firepoint.rotation);
    }


    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        Run();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        IsGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundmask);

        if(IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Horizontalmove = Input.GetAxis("Horizontal");
        Verticalmove = Input.GetAxis("Vertical");

        Vector3 move = transform.right * Horizontalmove + transform.forward * Verticalmove;

        Player.Move(move * MoveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        Player.Move(velocity * Time.deltaTime);

    }

    void Run()
    {
        if (Input.GetButton("Fire3"))
        {
            MoveSpeed = 30f;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            MoveSpeed = 10f;
        }
    }
}
