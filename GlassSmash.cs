using UnityEngine;

public class GlassSmash : MonoBehaviour
{

    [SerializeField]
    private GameObject _brokenGlassPrefab;

    [SerializeField]
    private GameObject _endPoint;

    private Rigidbody _attatchedRB;

    private void Start()
    {

        _attatchedRB = gameObject.GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 6)
        {

            _endPoint.GetComponent<TimerManagement>()._objectSmashCount += 1;

            //Instantiates a new version of _brokenGlassPrefab at the current location of the glass in game, destroying the non-broken glass afterwards
            Instantiate(_brokenGlassPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

        }

        if (_attatchedRB.linearVelocity.y > 1 || _attatchedRB.linearVelocity.y < -1)
        {

            _endPoint.GetComponent<TimerManagement>()._objectSmashCount += 1;
            Instantiate(_brokenGlassPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

        }

    }

}
