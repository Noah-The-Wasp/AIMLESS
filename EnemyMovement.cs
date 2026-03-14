using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private bool _doesPatrol = false;

    [SerializeField]
    private float _enemySpeed;

    [SerializeField]
    private GameObject _playerPoint;

    [SerializeField]
    private NavMeshAgent _enemyAgent;

    [SerializeField]
    private Vector3[] _patrolTargets;

    private bool _isDead;

    private bool _isGrabbed;

    private bool _isPatrolling;

    private int _currentPatrolIndex;

    void Update()
    {

        Vector3 moveLocation = _playerPoint.transform.position;

        float distanceToPlayer = Vector3.Distance(moveLocation, gameObject.transform.position);

        if (distanceToPlayer < 15f && _isDead == false && _isGrabbed == false)
        {

            _isPatrolling = false;
            _enemyAgent.isStopped = false;
            _enemyAgent.SetDestination(moveLocation);

        }
        else if (_isGrabbed == true)
        {

            _enemyAgent.isStopped = true;

        }
        else
        {

            if (_doesPatrol == false)
            {

                _enemyAgent.isStopped = true;

            }
            else
            {

                if (_isPatrolling == false)
                {

                    _currentPatrolIndex = CalculateNearestPoint();
                    _isPatrolling = true;

                }

                _enemyAgent.SetDestination(_patrolTargets[_currentPatrolIndex]);

                if (Vector3.Distance(gameObject.transform.position, _patrolTargets[_currentPatrolIndex]) < 1)
                {

                    if (_currentPatrolIndex == _patrolTargets.Length - 1)
                    {

                        _currentPatrolIndex = 0;

                    }
                    else
                    {

                        _currentPatrolIndex += 1;

                    }

                }

                Debug.Log(_currentPatrolIndex);

            }

        }

    }

    int CalculateNearestPoint()
    {

        float[] patrolDistances = new float[_patrolTargets.Length];

        for (int i = 0; i < _patrolTargets.Length; i++)
        {

            patrolDistances[i] = Vector3.Distance(_patrolTargets[i], gameObject.transform.position);

        }

        float shortestDistance = 10000;
        int shortestIndex = 0;

        for (int j = 0; j < _patrolTargets.Length; j++)
        {

            if (patrolDistances[j] < shortestDistance)
            {

                shortestDistance = patrolDistances[j];
                shortestIndex = j;

            }

        }

        return shortestIndex;

    }

    public void StopWhileGrabbed()
    {

        StartCoroutine(GrabCoolDown());

    }

    private IEnumerator GrabCoolDown()
    {

        _isGrabbed = true;

        yield return new WaitForSeconds(4);

        _isGrabbed = false;

    }

    public void StopMovement()
    {

        _isDead = true;
        _doesPatrol = false;

    }

}
