using UnityEngine;

public class ObjectGrab : MonoBehaviour
{

    [SerializeField]
    private bool _isSecondaryObjective;

    [SerializeField]
    private bool _isDestructable;

    private GameObject _endPoint;

    public Sprite HUDSprite;

    public GameObject ThrowObject;

    public int SpeedBoostAmount;

    public int ObjectWeight;

    private void Start()
    {

        _endPoint = GameObject.Find("EndPoint");

    }

    public void ObjectGrabed()
    {

        if (_isSecondaryObjective == true)
            _endPoint.GetComponent<TimerManagement>().AddToSOCount();

        if (_isDestructable == true)
            return;

        Destroy(gameObject);

    }

}
