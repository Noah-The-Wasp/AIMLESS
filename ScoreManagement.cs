using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{

    [SerializeField]
    private string[] _ranks;

    [SerializeField]
    private TMP_Text _devices;

    [SerializeField]
    private TMP_Text _SO;

    [SerializeField]
    private TMP_Text _time;

    [SerializeField]
    private TMP_Text _style;

    [SerializeField]
    private TMP_Text _enemies;

    [SerializeField]
    private TMP_Text _costumes;

    [SerializeField]
    private TMP_Text _level;

    [SerializeField]
    private TMP_Text _rank;

    private int _numberOfDevices;

    private int _numberOfSO;

    private int _numberOfEnemies;

    private int _numberOfCostumes;

    private int _levelNum;

    private int _phoneSmashCount;

    private int _soCount;

    private int _objectSmashCount;

    private int _doorSmashCount;

    private int _enemiesKilled;

    private string _savedTime;

    private int _secondsElapsed;

    private GameObject _ScorePass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _ScorePass = GameObject.Find("ScoreStore");

        _numberOfDevices = _ScorePass.GetComponent<ScorePass>()._numberOfDevices;

        _numberOfSO = _ScorePass.GetComponent<ScorePass>()._numberOfSO;

        _numberOfEnemies = _ScorePass.GetComponent<ScorePass>()._numberOfEnemies;

        _numberOfCostumes = _ScorePass.GetComponent<ScorePass>()._numberOfCostumes;

        _levelNum = _ScorePass.GetComponent<ScorePass>()._levelNum;

        _phoneSmashCount = _ScorePass.GetComponent<ScorePass>()._phoneSmashCount;

        _soCount = _ScorePass.GetComponent<ScorePass>()._soCount;

        _objectSmashCount = _ScorePass.GetComponent<ScorePass>()._objectSmashCount;

        _doorSmashCount = _ScorePass.GetComponent<ScorePass>()._doorSmashCount;

        _enemiesKilled = _ScorePass.GetComponent<ScorePass>()._enemiesKilled;

        _savedTime = _ScorePass.GetComponent<ScorePass>()._savedTime;

        _secondsElapsed = _ScorePass.GetComponent<ScorePass>()._secondsElapsed;

        int scoreCount = (_objectSmashCount + _doorSmashCount) * 100;

        _devices.text = _phoneSmashCount + "/" + _numberOfDevices;
        _SO.text = _soCount + "/" + _numberOfSO;
        _time.text = _savedTime;
        _style.text = scoreCount.ToString();
        _enemies.text = _enemiesKilled + "/" + _numberOfEnemies;
        _costumes.text = "0/0";

        _level.text = "Level #" + _levelNum;
        _rank.text = "Rank " + RankCalc();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string RankCalc()
    {

        float Score = 0f;

        Score += _phoneSmashCount * 200;
        Score += _soCount * 300;
        Score -= _secondsElapsed;
        Score += (_objectSmashCount + _doorSmashCount) * 100;
        Score += _enemiesKilled * 400;

        Score = Score / 1000;
        Score = Mathf.FloorToInt(Score);

        if (Score <= 0)
        {

            return "F";

        }
            
        if (Score >= 5)
        {

            return "S+";

        }

        Score -= 1;
        return _ranks[(int)Score];

    }

}
