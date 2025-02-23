using CollisionHandler;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(RaycastController))]
public class PlayerController : BaseCollisionHandler
{
    public float JumpHeight = 20;
    public float TimeToJumpApex = .4f;
 
    private float _storedVelocity;
    private Rigidbody _body;
    private InputManager _inputManager;
    
    private RaycastController _raycastController;
   
    private Vector2 _input;
    private Vector3 _velocity;
    private float _horizontalVelocity, _verticalVelocity;
    private float _gravity;

    [SerializeField]
    [RangeAttribute(0, 20)] private float _maxSpeedX;
    [SerializeField]
    [RangeAttribute(0.1f, 1f)] private float _accelerationX;
    [SerializeField]
    [RangeAttribute(0f, 1f)] private float _airAccelerationX;
    
    private float _velocitySmoothing;
    private Vector3 _platformVelocity;

    private void Start()
    {
        _raycastController = GetComponent<RaycastController>();
        _body = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();

        CalculateVerticalForces(JumpHeight, TimeToJumpApex);
    }

    /// <summary>
    /// Physics Update handles: platforms, horizontal and vertical movement.
    /// </summary>
    private void FixedUpdate()
    {
        _platformVelocity = Vector3.zero;
        _input = new Vector2(Input.GetAxisRaw(_inputManager.HorizontalAxisName), Input.GetAxisRaw(_inputManager.VerticalAxisName));

        _horizontalVelocity = _input.x * _maxSpeedX;

        _velocity = new Vector3(Mathf.SmoothDamp(_velocity.x, _horizontalVelocity, ref _velocitySmoothing, _raycastController.Collisions.Below ? _accelerationX : _airAccelerationX), _body.linearVelocity.y);
        
        if (_raycastController.Collisions.Below)
        {
            _storedVelocity = 0;
            HandlePlatforms();

            if (Mathf.Abs(_input.y) > 0)
                _velocity.y = _input.y * _verticalVelocity;
        }
        else
        {
            _body.mass = 1;
            _velocity.y += _gravity * Time.fixedDeltaTime;
        }

        _body.linearVelocity = _velocity + _platformVelocity;
    }
   
    /// <summary>
    /// Sets player's variables appropriate for when crossed into the upside down. 
    /// We store velocity so that we can apply it as a jump force on the other side. 
    /// Letting a natural jump be performed without resetting the velocity would cause the item to jump higher each time, going to infinity.
    /// </summary>
    public void ToggleUpsideDownValues()
    {
        _gravity = -_gravity;
        
        _storedVelocity = (Mathf.Abs(_storedVelocity) < 0.1f)?_body.linearVelocity.y : _storedVelocity;
   
        _storedVelocity = -_storedVelocity;
        _body.SetVelocityY(-_storedVelocity);

        //Reverse Jumping Velocity for controls to remain the same.
        _verticalVelocity = -_verticalVelocity;
    }

    /// <summary>
    ///  Automatically sets jump force and gravity to match the Jump Height and the time to get to the height.
    /// </summary>
    /// <param name="jumpHeight">Desired jump height expressed in units.</param>
    /// <param name="timeToJumpApex">Time it should take to reach the jumpHeight variable.</param>
    private void CalculateVerticalForces(float jumpHeight, float timeToJumpApex)
    {
        _gravity = -(2 * JumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        _verticalVelocity = Mathf.Abs(_gravity) * TimeToJumpApex;
    }

    /// <summary>
    /// Moves rigidbody with platform.
    /// </summary>
    private void HandlePlatforms() //Not useful in Unity 6:? Platforms have been deleted, we now land on pure ibb/obb mesh.
    {
        if (!_raycastController.Collisions.HitLayer.IsInLayerMask(1 << 12)) //?
            return;
        Debug.Log("Handling platforms");
        _platformVelocity.x = _raycastController.Collisions.RayHit.rigidbody.linearVelocity.x;
        _platformVelocity.y += _gravity * Time.fixedDeltaTime;
        _body.mass = 0;
    }

    #region Collision Response

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<EnemyCollisionHandler>().OnCollisionWithPlayer(this);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("UpsideDown"))
        {
            other.GetComponent<InteractableCollisionHandler>().OnCollisionWithPlayer(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UpsideDown"))
        {
            other.GetComponent<InteractableCollisionHandler>().OnExitCollisionWithPlayer(this);
        }
    }

    #endregion
}
