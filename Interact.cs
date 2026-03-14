using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class Interact : MonoBehaviour
{

    [SerializeField]
    private Material _highlightMaterial;

    [SerializeField]
    private Material _unhighlightedMaterial;

    [SerializeField]
    private GameObject _playerObj;

    [SerializeField]
    private GameObject[] _speedBoostObj;

    [SerializeField]
    private Image _playerHUD;

    [SerializeField]
    private Image _weightHUD;

    [SerializeField]
    private Image _speedHUD;

    [SerializeField]
    private InputActionAsset _playerInputs;

    [SerializeField]
    private Sprite _flipOff;

    [SerializeField]
    private Sprite[] _spriteSequence;

    [SerializeField]
    private Sprite[] _speedSpriteSequence;

    [SerializeField]
    private float _wallCheckDistance;

    [SerializeField]
    private GameObject _particleSmoke;

    private InputAction _interactInput;

    private bool _isHolding;

    private GameObject _throwableObject;

    private bool _isHitting;

    private bool _isTooCloseToWall;

    private int _objectWeight;

    private int _speedBoostAmount;

    private MeshRenderer _targetMesh = null;
    //FlipOff();
    void Awake()
    {

        //_interactInput stores the mappings from Unity's in-build input system
        _interactInput = _playerInputs.FindActionMap("Player").FindAction("Interact");

    }

    void Update()
    {

        Debug.Log(_isTooCloseToWall);

        //Checks if the player is currently looking at an object
        HittingObject();

        //Allows the player to throw an object if they press E and are not already holding something
        if (_interactInput.triggered == true && _isHolding == true && _isTooCloseToWall == false)
            Throw();

        //Checks if the player is holding an object if E is not pressed
        if (_isHolding == true)
            return;

        //If E is then pressed a ray is cast to see if there is an object in front of the player
        if (_interactInput.triggered)
            RayCast();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Wall")
            return;

        _isTooCloseToWall = true;

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag != "Wall")
            return;

        _isTooCloseToWall = false;

    }

    void RayCast()
    {

        //Casts a ray, if nothing is hit the function is terminated
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit rayHit, 2) == false)
            return;

        //If the ray hits an NPC who is holding a phone this statement is run
        if (rayHit.transform.gameObject.tag == "Enemy")
        {

            //GetComponent is quite a system intensive function so this is only ran twice to save processing power, this changes the NPC's sprite
            rayHit.transform.gameObject.GetComponent<PhoneGrab>().GrabPhone();

            //And this instance of GetComponent assigns getIndex which allows this script to access components of the phoneGrab script on the NPC
            PhoneGrab getIndex = rayHit.transform.gameObject.GetComponent<PhoneGrab>();

            //This code gets a sprite attatched to the NPC and makes this this appear on the player's UI, this was done to avoid having an array of all sprites
            _playerHUD.sprite = getIndex.HUDSprite;
            _playerHUD.color = Color.white;

            //This assigns the object that the player can now throw to the object attatched to the NPC, also to avoid large array of objects
            _throwableObject = getIndex.ThrowObject;

            _isHolding = true;

        }

        if (rayHit.transform.gameObject.tag == "Object")
        {

            ObjectGrab getIndex = rayHit.transform.gameObject.GetComponent<ObjectGrab>();
            _playerHUD.sprite = getIndex.HUDSprite;
            _playerHUD.color = Color.white;

            _objectWeight = getIndex.ObjectWeight;

            ObjectWeight(_objectWeight);

            _speedBoostAmount = getIndex.SpeedBoostAmount;

            _throwableObject = getIndex.ThrowObject;

            _isHolding = true;

            rayHit.transform.gameObject.GetComponent<ObjectGrab>().ObjectGrabed();

        }


    }

    public void ObjectWeight(int objWeight)
    {

        _weightHUD.sprite = _spriteSequence[objWeight];
        _weightHUD.color = Color.white;

    }

    void HittingObject()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit rayHit, 2) == true)
        {

            if (rayHit.transform.gameObject.tag != "Object")
                return;

            if (_isHitting == true)
                return;

            _targetMesh = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            Material[] materialList = _targetMesh.materials;
            materialList[materialList.Length - 1] = _highlightMaterial;
            _targetMesh.materials = materialList;

            _isHitting = true;

        }
        else
        {

            if (_isHitting == false || _targetMesh == null)
                return;

            Material[] materialList = _targetMesh.materials;
            materialList[materialList.Length - 1] = _unhighlightedMaterial;
            _targetMesh.materials = materialList;

            _isHitting = false;

        }

    }

    void Throw()
    {

        _isHitting = false;
         
        if (_speedBoostObj.Contains(_throwableObject))
        {

            Color transparentColor = Color.white;
            transparentColor.a = 0f;
            _playerHUD.color = transparentColor;
            _weightHUD.color = transparentColor;

            _playerHUD.sprite = _flipOff;

            _playerObj.GetComponent<PlayerController>().IsSprinting = true;
            _playerObj.GetComponent<PlayerController>().SprintAmount = _speedBoostAmount;

            if (_speedBoostAmount < 6)
                Smoke();

            if (_speedBoostAmount == 9)
                _speedHUD.sprite = _speedSpriteSequence[0];

            if (_speedBoostAmount == 12)
                _speedHUD.sprite = _speedSpriteSequence[1];

            if (_speedBoostAmount == 15)
                _speedHUD.sprite = _speedSpriteSequence[2];

            _speedHUD.color = Color.white;

            StartCoroutine(WaitToResetHolding());

        }
        else
        {

            GameObject thrownInstantiation;

            thrownInstantiation = Instantiate(_throwableObject);
            thrownInstantiation.transform.position = transform.position;
            thrownInstantiation.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            StartCoroutine(WaitToResetHolding());

            _playerHUD.sprite = _flipOff;

            Color transparentColor = Color.white;
            transparentColor.a = 0f;
            _playerHUD.color = transparentColor;
            _weightHUD.color = transparentColor;

        }

    }

    void FlipOff()
    {

        if (_isHitting == true || _isHolding == true)
            return;

        if (_interactInput.inProgress == true)
        {

            _playerHUD.sprite = _flipOff;
            _playerHUD.color = Color.white;

        }
        else
        {

            Color transparentColor = Color.white;
            transparentColor.a = 0f;
            _playerHUD.color = transparentColor;

        }

    }

    void Smoke()
    {

        Instantiate(_particleSmoke, transform.position, transform.rotation);

    }

    IEnumerator WaitToResetHolding()
    {

        yield return new WaitForEndOfFrame();
        _isHolding = false;

    }

}
