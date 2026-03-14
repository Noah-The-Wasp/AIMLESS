using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiftController : MonoBehaviour
{

    [SerializeField]
    private GameObject _liftPlatform;

    [SerializeField]
    private float _lowerY;

    [SerializeField]
    private float _upperY;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private TextMeshProUGUI _pauseIndicator;

    [SerializeField]
    private MeshRenderer _liftControl;

    [SerializeField]
    private Material _upMat;

    [SerializeField]
    private Material _downMat;

    [SerializeField]
    private GameObject _audioSource;

    private Vector3 _targetPos;

    private float _step;

    private bool _isTriggered;

    private void OnCollisionEnter(Collision collision)
    {

        if (_liftPlatform.transform.position.y == _lowerY)
        {

            _liftControl.material = _downMat;
            _targetPos = new Vector3(_liftPlatform.transform.position.x, _upperY, _liftPlatform.transform.position.z);
            _step = _speed * Time.deltaTime;
            _isTriggered = true;

        }

        if (_liftPlatform.transform.position.y == _upperY)
        {

            _liftControl.material = _upMat;
            _targetPos = new Vector3(_liftPlatform.transform.position.x, _lowerY, _liftPlatform.transform.position.z);
            _step = _speed * Time.deltaTime;
            _isTriggered = true;

        }  

    }

    private void FixedUpdate()
    {

        if (_isTriggered == false)
            return;

        if (_pauseIndicator.enabled == false)
        {

            _audioSource.SetActive(true);
            _liftPlatform.transform.position = Vector3.MoveTowards(_liftPlatform.transform.position, _targetPos, _step);

        }

        if (_liftPlatform.transform.position.y == _upperY || _liftPlatform.transform.position.y == _lowerY)
        {

            _audioSource.SetActive(false);
            _isTriggered = false;

        }

    }

}
