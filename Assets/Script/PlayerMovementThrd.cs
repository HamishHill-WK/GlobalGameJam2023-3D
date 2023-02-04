using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;

public class PlayerMovementThrd : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float turnSmoothTime = 0.1f;

	[Header("Ground Check")]
    public Transform ground_check;
    public float ground_distance = 0.4f;
    public LayerMask ground_mask;
	
	[Header("Ranged Attack Settings")]
	[SerializeField] private GameObject CrosshairModel;
	[SerializeField] private GameObject AttackModel;
	[SerializeField] private float RangedAttackCoolDown = 3f;
	[SerializeField] private float RangedAttackRange = 15f;
	private bool _isRangedAttackActivated;

	Vector3 velocity;
    bool isGrounded;

    float turnSmoothVelocity;

    private void Start()
    {
		_isRangedAttackActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
	    PlayerMovement();
		RangedAttack();
    }

    private void PlayerMovement()
    {
	    if (Input.GetButton("Sprint"))
	    {
		    speed = 18f;
	    }
	    else
	    {
		    speed = 12;
	    }

	    float horizontal = Input.GetAxisRaw("Horizontal");
	    float vertical = Input.GetAxisRaw("Vertical");

	    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
	    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
	    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
	    transform.rotation = Quaternion.Euler(0f, angle, 0f);

	    isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, ground_mask);

	    if (isGrounded && velocity.y < 0)
	    {
		    velocity.y = -0f;
	    }

	    Vector3 move = transform.right * horizontal + transform.forward * vertical;


	    controller.Move(move * speed * Time.deltaTime);

	    if (Input.GetButtonDown("Jump") && isGrounded)
	    {
		    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
	    }

	    velocity.y += gravity * Time.deltaTime;
	    controller.Move(velocity * Time.deltaTime);
    }

    private void RangedAttack()
    {
	    if (_isRangedAttackActivated)
		    return;

	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("whatIsGround")))
		{
			CrosshairModel.transform.position = hit.point;

			if (
				Vector2.Distance(
					new Vector2(transform.position.x, transform.position.z),
					new Vector2(CrosshairModel.transform.position.x, CrosshairModel.transform.position.z)
				)
				>= RangedAttackRange)
			{
				CrosshairModel.SetActive(false);
				return;
			}

			CrosshairModel.SetActive(true);
		}

		if (Input.GetMouseButtonDown(1))
		{
			StartCoroutine(RangedAttackSequence());
		}
    }

    private IEnumerator RangedAttackSequence()
    {
	    CrosshairModel.SetActive(false);
	    AttackModel.SetActive(true);
	    _isRangedAttackActivated = true;

		AttackModel.transform.position = CrosshairModel.transform.position;

		yield return new WaitForSeconds(RangedAttackCoolDown);

	    CrosshairModel.SetActive(true);
		AttackModel.SetActive(false);
		_isRangedAttackActivated = false;
    }
}
