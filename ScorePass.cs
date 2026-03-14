using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScorePass : MonoBehaviour
{

    public int _numberOfDevices;

    public int _numberOfSO;

    public int _numberOfEnemies;

    public int _numberOfCostumes;

    public int _levelNum;

    public string _nextLevel;

    public string _currentLevel;

    private GameObject _endPoint;

    public int _phoneSmashCount;

    public int _soCount;

    public int _objectSmashCount;

    public int _doorSmashCount;

    public int _enemiesKilled;

    public string _savedTime;

    public int _secondsElapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(gameObject);

        _endPoint = GameObject.Find("EndPoint");

    }

    public void LoadScores()
    {

        _phoneSmashCount = _endPoint.GetComponent<TimerManagement>()._phoneSmashCount;
        _soCount = _endPoint.GetComponent<TimerManagement>()._soCount;
        _objectSmashCount = _endPoint.GetComponent<TimerManagement>()._objectSmashCount;
        _doorSmashCount = _endPoint.GetComponent<TimerManagement>()._doorSmashCount;
        _enemiesKilled = _endPoint.GetComponent<TimerManagement>()._enemiesKilled;
        _savedTime = _endPoint.GetComponent<TimerManagement>()._savedTime;
        _secondsElapsed = _endPoint.GetComponent<TimerManagement>()._secondsElapsed;

    }

}
