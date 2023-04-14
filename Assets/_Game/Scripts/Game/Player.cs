using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _movingSpeed;

	[Header("FX")]
	[SerializeField] private ParticleSystem _walkFx;
	[SerializeField] private Transform _walkFxLeftPos;
	[SerializeField] private Transform _walkFxRightPos;

	[Space]
	[SerializeField] private SpriteRenderer head;
	[SerializeField] private SpriteRenderer toros;
	
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
		if (!Tree.GameManager.gameStarted)
			return;

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

	public void ChangeHeadDesign(Sprite sprite)
	{
		head.sprite = sprite;
	}

	public void ChangeTorosDesign(Sprite sprite)
	{
		toros.sprite = sprite;
	}

	public void StepFxShowLeft()
	{
		ParticleSystem m_walkFx = Instantiate(_walkFx, _walkFxLeftPos.position, Quaternion.identity);
	}

	public void StepFxShowRight()
	{
		ParticleSystem m_walkFx = Instantiate(_walkFx, _walkFxRightPos.position, Quaternion.identity);
	}
}
