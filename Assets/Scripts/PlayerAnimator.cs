using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private InputManager _inputManager;
    private Rigidbody _body;
    private RaycastController _raycastController;
    private float _runSpeed;
    private float _horizontalInput;
    
	void Start ()
	{
	    _body = GetComponent<Rigidbody>();
	    _animator = GetComponent<Animator>();
	    _inputManager = GetComponent<InputManager>();
	    _raycastController = GetComponent<RaycastController>();
	    
	}
	
	void Update () {
        UpdateFacingDirection();

	    _horizontalInput = Input.GetAxisRaw(_inputManager.HorizontalAxisName);

        if (_raycastController.Collisions.RayHit.rigidbody != null)
           _runSpeed = _body.linearVelocity.x * Input.GetAxisRaw(_inputManager.HorizontalAxisName);
        else
        {
            _runSpeed = _body.linearVelocity.x;
        }

        _animator.SetFloat("RunSpeed", Mathf.Abs(_runSpeed));
	    _animator.SetBool("Running", Mathf.Abs(_runSpeed) > 0.1f);

	    if (transform.up.y > 0)
	    {
	        if (_body.linearVelocity.y > 0 && !_raycastController.Collisions.Below)
	        {
	            _animator.SetBool("Jumping", true);
                _animator.SetBool("Running", false);
	        }
	        else if (_body.linearVelocity.y < 0)
	        {
	            _animator.SetBool("Jumping", false);
	            _animator.SetBool("Falling", true);
	        }
	        else _animator.SetBool("Falling", false);
        }
	    else
	    {
	        if (_body.linearVelocity.y < 0 && !_raycastController.Collisions.Below)
	        {
	            _animator.SetBool("Running", false);
	            _animator.SetBool("Jumping", true);
	        }
	        else if (_body.linearVelocity.y > 0)
	        {
	            _animator.SetBool("Jumping", false);
	            _animator.SetBool("Falling", true);
	        }
            else _animator.SetBool("Falling", false);
        }

	}

    /// <summary>
    /// Handles all cases for player's facing direction depending on input and velocity.
    /// </summary>
    private void UpdateFacingDirection()
    {
        float direction = Mathf.Sign(_body.linearVelocity.x);

        Vector3 horizontalFacingDirection =
            new Vector3(transform.localEulerAngles.x, 90 * direction, transform.localEulerAngles.z);

        if (_horizontalInput != 0)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90 * _horizontalInput, transform.localEulerAngles.z); 
        else
        {
            transform.localEulerAngles =
                new Vector3(transform.localEulerAngles.x, -180, transform.localEulerAngles.z);

            //If RayHit is not touching the player
            if (_raycastController.Collisions.RayHit.rigidbody == null && Mathf.FloorToInt(Mathf.Abs(_body.linearVelocity.x)) != 0)
            {
                transform.localEulerAngles = horizontalFacingDirection;
            }
        }
    }
}
