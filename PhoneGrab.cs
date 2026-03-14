using UnityEngine;
using System.Collections;

public class PhoneGrab : MonoBehaviour
{

    [SerializeField]
    private bool _isRotLocked;

    [SerializeField]
    private MeshRenderer _objectMaterial;

    [SerializeField]
    private Material _phonelessMaterial;

    [SerializeField]
    private Material _backMaterial;

    [SerializeField]
    private GameObject _interactor;

    [SerializeField]
    private int _objectWeight;

    [SerializeField]
    private Material[] _phoneSequence;

    [SerializeField]
    private Material[] _phonelessSequence;

    [SerializeField]
    private float[] _blinkRatios;

    private Material[] _currentArray;

    private int _spriteIndex = 0;

    public Sprite HUDSprite;

    public GameObject ThrowObject;

    private void Start()
    {

        _currentArray = _phoneSequence;

        StartCoroutine(Animate());

    }

    public void GrabPhone()
    {

        if (_isRotLocked == true)
        {

            Material[] materialList = _objectMaterial.materials;
            materialList[2] = _backMaterial;
            materialList[0] = _phonelessMaterial;
            _objectMaterial.materials = materialList;

            _interactor.GetComponent<Interact>().ObjectWeight(_objectWeight);

            _currentArray = _phonelessSequence;

            gameObject.tag = "Untagged";

        }
        else
        {

            _objectMaterial.material = _phonelessMaterial;

            _interactor.GetComponent<Interact>().ObjectWeight(_objectWeight);

            _currentArray = _phonelessSequence;

            gameObject.tag = "Untagged";

        }

    }

    IEnumerator Animate()
    {

        yield return new WaitForSeconds(_blinkRatios[_spriteIndex]);

        if (_spriteIndex == 0)
        {

            _objectMaterial.material = _currentArray[_spriteIndex];
            _spriteIndex = 1;
            StartCoroutine(Animate());

        }
        else
        {
            _objectMaterial.material = _currentArray[_spriteIndex];
            _spriteIndex = 0;
            StartCoroutine(Animate());

        }

    }

}
