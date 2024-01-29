using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class StoneMovement : MonoBehaviour
{
	[SerializeField] private float gravity;
	[SerializeField] private float reboundSpeed;
	[SerializeField] private float horizontalSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float gravityOffset;

	private bool UseGravity;
	private Vector3 velocity;

	private void Awake() => velocity.x = -Mathf.Sign(transform.position.x)* horizontalSpeed;

	private void Update()
	{
		if (!LevelState.isActiveStoneMovement) return;
		TryEnableGravity();
		Move();
	}

	private void TryEnableGravity()
	{
		if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffset) UseGravity = true;
	}

	private void Move()
	{
		if (UseGravity)
		{
			velocity.y -= gravity * Time.deltaTime;
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}

		velocity.x = Mathf.Sign(velocity.x) * horizontalSpeed;
		transform.position += velocity * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//if (!collision.transform.root.TryGetComponent(out LevelEdge levelEdge)) return;
		if (!collision.TryGetComponent(out LevelEdge levelEdge)) return;
		
		if (levelEdge.Type is EdgeType.Bottom) velocity.y = reboundSpeed;

		if (levelEdge.Type is EdgeType.Left && velocity.x < 0 ||
		    levelEdge.Type is EdgeType.Right && velocity.x > 0)
			velocity.x *= -1;
	}

	public void AddVerticalVelocity(float velocity) => this.velocity.y += velocity;
	public void SetHorizontalDirection(float direction) => velocity.x = Math.Sign(direction) * horizontalSpeed;
}