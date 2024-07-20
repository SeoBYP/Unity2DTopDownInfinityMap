using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Vector2 moveVelocity;

	public Vector2 MoveDirection => moveInput.normalized;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();

	}


    // Update is called once per frame
    void Update()
    {
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput.normalized * moveSpeed;
    }

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}
