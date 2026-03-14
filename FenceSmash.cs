using UnityEngine;

public class FenceSmash : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _doorRB;

    [SerializeField]
    private Collider _doorCollider;

    [SerializeField]
    private Collider _backDoorCollider;

    [SerializeField]
    private bool _isFrontOrBack;

    [SerializeField]
    private GameObject _soundPlayer;

    [SerializeField]
    private GameObject _endPoint;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer != 6)
            return;

        if (collision.gameObject.GetComponent<ObjectGrab>().ObjectWeight < 1)
            return;

        if (_isFrontOrBack == true)
            ForwardPush();

        if (_isFrontOrBack == false)
            BackwardPush();

    }

    void ForwardPush()
    {

        _endPoint.GetComponent<TimerManagement>()._doorSmashCount += 1;

        _soundPlayer.SetActive(true);
        _doorRB.constraints = RigidbodyConstraints.None;
        _doorRB.AddForce((gameObject.transform.forward) * 200);
        _doorCollider.enabled = true;
        _backDoorCollider.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

    }

    void BackwardPush()
    {

        _endPoint.GetComponent<TimerManagement>()._doorSmashCount += 1;

        _soundPlayer.SetActive(true);
        _doorRB.constraints = RigidbodyConstraints.None;
        _doorRB.AddForce((gameObject.transform.forward * -1) * 200);
        _doorCollider.enabled = true;
        _backDoorCollider.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

    }

}
