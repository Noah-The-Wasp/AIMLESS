using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTracking : MonoBehaviour
{

    private float _cameraX = 5;
    private float _cameraY = 5;

    private InputAction _cameraInputs;

    private Vector2 _mouseAxis;

    [SerializeField]
    private InputActionAsset _playerInputs;

    [SerializeField]
    private Transform _playerRotation;

    private float _xRotation;
    private float _yRotation;

    private bool _isPaused = false;

    void Awake()
    {

        _cameraInputs = _playerInputs.FindActionMap("Player").FindAction("Look");

        _cameraInputs.performed += context => _mouseAxis = context.ReadValue<Vector2>();
        _cameraInputs.canceled += context => _mouseAxis = Vector2.zero;

    }

    private void OnEnable()
    {

        _cameraInputs.Enable();

    }

    private void OnDisable()
    {

        _cameraInputs.Disable();

    }

    private void Update()
    {

        if (_isPaused == true)
            return;

        float MouseX = _mouseAxis.x / _cameraX;
        float MouseY = _mouseAxis.y / _cameraY;

        _yRotation += MouseX;
        _xRotation -= MouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _playerRotation.rotation = Quaternion.Euler(0, _yRotation, 0);

    }

    public void Pause()
    {

        _isPaused = true;

    }

    public void UnPause()
    {

        _isPaused = false;

    }

}
