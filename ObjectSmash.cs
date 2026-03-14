using UnityEngine;

public class ObjectSmash : MonoBehaviour
{

    [SerializeField]
    private GameObject _smashedObject;

    [SerializeField]
    private GameObject _particleExplode;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 2)
            return;

        Instantiate(_smashedObject, transform.position, transform.rotation);
        Instantiate(_particleExplode, transform.position, transform.rotation);
        Destroy(gameObject);

    }

}
