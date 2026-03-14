using UnityEngine;

public class CharacterRotator : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerRotation;

    [SerializeField]
    private GameObject _rotChecker;

    [SerializeField]
    private GameObject _playerPosition;

    [SerializeField]
    private float _playerDistanceCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //lookRotation stores the rotation of the player, the y of this rotation is then applied to the NPC
        Quaternion lookRotation = _playerRotation.transform.rotation;
        transform.rotation = new Quaternion(0f, lookRotation.y, 0f, lookRotation.w);

    }

}
