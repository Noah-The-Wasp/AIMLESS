using UnityEngine;

public class VendingMachine : MonoBehaviour
{

    [SerializeField]
    private GameObject _spawnLocation;

    [SerializeField]
    private GameObject _canOfBoke;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Object")
            return;

        Instantiate(_canOfBoke, _spawnLocation.transform.position, _canOfBoke.transform.rotation);

    }

}
