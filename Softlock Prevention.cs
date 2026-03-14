using UnityEngine;

public class SoftlockPrevention : MonoBehaviour
{

    [SerializeField]
    private GameObject _softlockPrevention;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Player")
            return;

        _softlockPrevention.SetActive(true);
        gameObject.SetActive(false);

    }

}
