using UnityEngine;
using FMODUnity;

public class SecondaryObjectiveDisposal : MonoBehaviour
{

    [SerializeField]
    private string _objectTagName;

    [SerializeField]
    private StudioEventEmitter _soundPlayer;

    private GameObject _endPoint;

    private void Start()
    {

        _endPoint = GameObject.Find("EndPoint");

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == _objectTagName)
        {

            _endPoint.GetComponent<TimerManagement>().AddToSOCount();

            _soundPlayer.Play();

            Destroy(gameObject);

        }

    }

}
