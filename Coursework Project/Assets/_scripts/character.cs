using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{

    public float moveSpeed = 6.0f;
    public float rotateSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private int jumps;
    private float sprintSpeedMultiplier = 2f;
    private float _yAxisVelocity;
    private float _gravity = -10f;
    // Start is called before the first frame update
    private float jumpHeight = 3f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            vertical *= sprintSpeedMultiplier;

        Vector3 movement = horizontal * moveSpeed * Time.deltaTime * transform.right + vertical * moveSpeed * Time.deltaTime * transform.forward;

        if (characterController.isGrounded)
            _yAxisVelocity = -0.5f;

        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            _yAxisVelocity = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }

        _yAxisVelocity += _gravity * Time.deltaTime;
        movement.y = _yAxisVelocity * Time.deltaTime;

        characterController.Move(movement);
    }
}
