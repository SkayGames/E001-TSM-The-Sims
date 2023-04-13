using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _movingSpeed;

	private Rigidbody2D _rb;
	private Animator _animator;

	private Vector2 _moveDirection;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_moveDirection.x = Input.GetAxisRaw("Horizontal");
		_moveDirection.y = Input.GetAxisRaw("Vertical");

		if (_moveDirection.x != 0 || _moveDirection.y != 0)
		{
			if (_moveDirection.x != 0)
				transform.localScale = new Vector3(_moveDirection.x > 0 ? 0.2f : -0.2f, transform.localScale.y, transform.localScale.z);

			_animator.SetBool("Walk", true);
		}

		if (_moveDirection.x == 0 && _moveDirection.y == 0)
		{
			_animator.SetBool("Walk", false);
		}


		if (Input.GetKeyDown(KeyCode.H))
			Hit();
	}

	private void FixedUpdate()
	{
		_rb.MovePosition(_rb.position + _moveDirection * _movingSpeed * Time.fixedDeltaTime);
	}

	private void Hit()
	{
		_animator.SetTrigger("Hit");
	}
}
