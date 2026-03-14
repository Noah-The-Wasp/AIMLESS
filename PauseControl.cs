using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PauseControl : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset _playerInputs;

    [SerializeField]
    private TextMeshProUGUI _pauseTitle;

    [SerializeField]
    private Camera _mainCamera;

    private InputAction _pauseInput;

    private bool _isPaused = false;

    private void Awake()
    {

        _pauseInput = _playerInputs.FindActionMap("Player").FindAction("Pause");


    }
    private void OnEnable()
    {

        _pauseInput.Enable();

    }

    private void OnDisable()
    {

        _pauseInput.Disable();

    }

    // Update is called once per frame
    void Update()
    {

        if (_pauseInput.triggered == true)
            Pause();

    }

    private void Pause()
    {

        if (_isPaused == false)
        {

            _pauseTitle.enabled = true;
            Time.timeScale = 0;
            _mainCamera.GetComponent<CameraTracking>().Pause();
            _isPaused = true;

        }
        else
        {

            _pauseTitle.enabled = false;
            Time.timeScale = 1;
            _mainCamera.GetComponent<CameraTracking>().UnPause();
            _isPaused = false;

        }

    }

}
