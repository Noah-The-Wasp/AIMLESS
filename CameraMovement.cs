using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject _cameraPosition;

    void Update()
    {

        transform.position = _cameraPosition.transform.position;

    }

}
