using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Limiting the speed of the rigidbody
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 1);

        // Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 left = transform.TransformDirection(Vector3.left);
        float curSpeed = 0.05f * Input.GetAxis("Vertical");
        float leftSpeed = 0.05f * Input.GetAxis("Horizontal");
        Vector3 vector3Move = new Vector3(leftSpeed, 0, curSpeed);
        cc.Move(vector3Move);
    }

    public void TakeDamage(int _damage)
    {
        print(gameObject.name + " Took " + _damage + " damage");
    }
}
