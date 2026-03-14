using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Vector3 _direction;

    private Rigidbody _player;

    private bool _isGrounded;

    private bool _isJumpCooled = true;

    private InputAction _movementInputs;

    private InputAction _jumpInput;

    private Vector2 _movementAxis;

    [SerializeField]
    private InputActionAsset _playerInputs;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _playerRot;

    [SerializeField]
    private float _drag;

    [SerializeField]
    private float _height;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private float _jumpCooldown;

    [SerializeField]
    private float _airMult;

    [SerializeField]
    private Image _armOverlay;

    [SerializeField]
    private Sprite _grabbingArms;

    [SerializeField]
    private Image _playerFace;

    [SerializeField]
    private Sprite _jumpFace;

    [SerializeField]
    private Sprite _normalFace;

    [SerializeField]
    private Sprite _speedFace;

    [SerializeField]
    private Sprite _fallFace;

    [SerializeField]
    private Sprite[] _screamSequence;

    [SerializeField]
    private float _animSpeed;

    [SerializeField]
    private Image _speedHUD;

    [SerializeField]
    private Sprite[] _blinkSequence;

    [SerializeField]
    private float[] _blinkRatios;

    private int _blinkSpriteIndex = 0;

    private Collider _getEnemyCollider;

    private int _spriteIndex;

    private bool _isBlinking = true;

    private bool _isGrabbed = false;

    public bool IsSprinting = false;

    public int SprintAmount;

    void Awake()
    {

        _player = GetComponent<Rigidbody>();
        _player.freezeRotation = true;

        StartCoroutine(AnimateBlink());

        _movementInputs = _playerInputs.FindActionMap("Player").FindAction("Move");
        _jumpInput = _playerInputs.FindActionMap("Player").FindAction("Jump");

        _movementInputs.performed += context => _movementAxis = context.ReadValue<Vector2>();
        _movementInputs.canceled += context => _movementAxis = Vector2.zero;

    }

    private void OnEnable()
    {

        _movementInputs.Enable();
        _jumpInput.Enable();

    }

    private void OnDisable()
    {

        _movementInputs.Disable();
        _jumpInput.Disable();

    }

    void Update()
    {

        if (IsSprinting == true)
        {

            _speed = SprintAmount;

            StartCoroutine(ResetSprint());

        }
        else if (_isGrabbed == false)
        {

            _speed = 7;

        }

        Inputs();
        CapVelocity();

        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _height * 0.5f + 0.2f);

        if (_isGrounded == true)
        {

            _player.linearDamping = _drag;

        }
        else
        {

            _player.linearDamping = 0;

        }

    }

    private void FixedUpdate()
    {

        PlayerMovement();

    }

    private void Inputs()
    {

        if (_jumpInput.triggered && _isJumpCooled && _isGrounded)
        {

            _isJumpCooled = false;

            Jump();

            Invoke(nameof(ResetJump), _jumpCooldown);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Combatant")
            return;

        _isGrabbed = true;

        _playerFace.sprite = _screamSequence[0];

        StartCoroutine(AnimateScream());

        _getEnemyCollider = other.GetComponent<BoxCollider>();
        _speed = 0;
        _armOverlay.sprite = _grabbingArms;
        _armOverlay.color = Color.white;

        other.gameObject.GetComponent<EnemyMovement>().StopWhileGrabbed();

        StartCoroutine(Grabbed());

        _getEnemyCollider.enabled = false;

    }

    private void PlayerMovement()
    {

        _direction = _playerRot.forward * _movementAxis.y + _playerRot.right * _movementAxis.x;

        if (_isGrounded)
        {

            _player.AddForce(_direction.normalized * _speed * 10, ForceMode.Force);

        }
        else
        {

            _player.AddForce(_direction.normalized * _speed * 10f * _airMult, ForceMode.Force);

        }

    }

    private void CapVelocity()
    {

        Vector3 CurrentVelocity = new Vector3(_player.linearVelocity.x, 0f, _player.linearVelocity.z);

        if (CurrentVelocity.magnitude <= _speed)
            return;

        Vector3 CappedVelocity = CurrentVelocity.normalized * _speed;
        _player.linearVelocity = new Vector3(CappedVelocity.x, _player.linearVelocity.y, CappedVelocity.z);

    }

    private void Jump()
    {

        _player.linearVelocity = new Vector3(_player.linearVelocity.x, 0f, _player.linearVelocity.z);

        _player.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {

        _isJumpCooled = true;

    }

    IEnumerator AnimateScream()
    {

        yield return new WaitForSeconds(_animSpeed);

        if (_spriteIndex == 0 && _isGrabbed == true)
        {

            _playerFace.sprite = _screamSequence[_spriteIndex];
            _spriteIndex = 1;
            StartCoroutine(AnimateScream());

        }
        else if (_spriteIndex == 1 && _isGrabbed == true)
        {
            _playerFace.sprite = _screamSequence[_spriteIndex];
            _spriteIndex = 0;
            StartCoroutine(AnimateScream());

        }

    }

    IEnumerator AnimateBlink()
    {

        _isBlinking = true;

        yield return new WaitForSeconds(_blinkRatios[_blinkSpriteIndex]);

        if (_blinkSpriteIndex == 0 && _isGrabbed == false && IsSprinting == false)
        {

            _playerFace.sprite = _blinkSequence[_blinkSpriteIndex];
            _blinkSpriteIndex = 1;
            StartCoroutine(AnimateBlink());

        }
        else if (_blinkSpriteIndex == 1 && _isGrabbed == false && IsSprinting == false)
        {
            _playerFace.sprite = _blinkSequence[_blinkSpriteIndex];
            _blinkSpriteIndex = 0;
            StartCoroutine(AnimateBlink());

        }

    }

    IEnumerator ResetSprint()
    {

        if (_speed > 8)
            _playerFace.sprite = _speedFace;
        else
            _playerFace.sprite = _normalFace;

        _isBlinking = false;

        yield return new WaitForSeconds(5);
        _playerFace.sprite = _normalFace;

        if (_isBlinking == false)
            StartCoroutine(AnimateBlink());

        Color transparentColor = Color.white;
        transparentColor.a = 0f;
        _speedHUD.color = transparentColor;

        IsSprinting = false;

    }

    IEnumerator Grabbed()
    {

        _isBlinking = false;

        yield return new WaitForSeconds(3);
        _isGrabbed = false;

        Color transparentColor = Color.white;
        transparentColor.a = 0f;
        _armOverlay.color = transparentColor;

        _playerFace.sprite = _normalFace;

        if (_isBlinking == false)
            StartCoroutine(AnimateBlink());

        StartCoroutine(RestoreCollision());

    }

    IEnumerator RestoreCollision()
    {

        yield return new WaitForSeconds(5);
        _getEnemyCollider.enabled = true;

    }

}
