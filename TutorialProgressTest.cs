using UnityEngine;

public class TutorialProgressTest : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer _wallMesh;

    [SerializeField]
    private Collider _wallCollider;

    [SerializeField]
    private GameObject _endPoint;

    [SerializeField]
    private Sprite _atlasStoneSprite;

    [SerializeField]
    private GameObject _padlock;

    private GameObject _objectTrack;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<ObjectGrab>().HUDSprite != _atlasStoneSprite)
            return;

        FenceDown();

    }

    void FenceDown()
    {

        _wallCollider.enabled = false;
        _wallMesh.enabled = false;

        Destroy(_padlock);

        _endPoint.GetComponent<TimerManagement>().AddToSmashCount();

        _objectTrack = gameObject;

    }

}
