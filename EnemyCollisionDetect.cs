using UnityEngine;
using System.Collections;

public class EnemyCollisionDetect : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer _enemyMaterial;

    [SerializeField]
    private Material[] _materialSequence;

    [SerializeField]
    private float _animSpeed;

    [SerializeField]
    private GameObject _enemyParent;

    [SerializeField]
    private Collider _triggerVolume;

    [SerializeField]
    private Collider _hitBox;

    private int _spriteIndex = 0;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer != 6)
            return;

        if (collision.gameObject.GetComponent<ObjectGrab>().ObjectWeight != 2)
            return;

        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {

        yield return new WaitForSeconds(_animSpeed);

        if (_spriteIndex >= _materialSequence.Length)
        {

            _triggerVolume.enabled = false;
            _hitBox.enabled = false;
            gameObject.GetComponent<EnemyAnimation>().HasDied();
            _enemyParent.GetComponent<EnemyMovement>().StopMovement();

        }
        else
        {

            _enemyMaterial.material = _materialSequence[_spriteIndex];
            _spriteIndex++;
            StartCoroutine(Animate());

        }   

    }

}
