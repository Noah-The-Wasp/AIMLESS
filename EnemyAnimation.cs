using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer _enemyMaterial;

    [SerializeField]
    private Material[] _materialSequence;

    [SerializeField]
    private float _animSpeed;

    [SerializeField]
    private GameObject _endPoint;

    private int _spriteIndex = 0;

    private bool _isDead;

    private void Start()
    {

        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {

        yield return new WaitForSeconds(_animSpeed);

        if (_isDead == true)
        {

            _enemyMaterial.material = _materialSequence[2];
            StopCoroutine(Animate());

        }
        else if (_spriteIndex == 0)
        {

            _enemyMaterial.material = _materialSequence[_spriteIndex];
            _spriteIndex = 1;
            StartCoroutine(Animate());

        }
        else
        {
            _enemyMaterial.material = _materialSequence[_spriteIndex];
            _spriteIndex = 0;
            StartCoroutine(Animate());

        }

    }

    public void HasDied()
    {

        _isDead = true;

        _endPoint.GetComponent<TimerManagement>()._enemiesKilled += 1;

    }

}
