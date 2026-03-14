using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScorecardButtons : MonoBehaviour
{

    private string _nextLevel;

    private string _currentLevel;

    private string _levelToLoad;

    private int _levelNum;

    private GameObject _scorePass;

    [SerializeField]
    private RawImage _backdrop;

    [SerializeField]
    private Texture[] _loadScreens;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _scorePass = GameObject.Find("ScoreStore");

        _nextLevel = _scorePass.GetComponent<ScorePass>()._nextLevel;
        _currentLevel = _scorePass.GetComponent<ScorePass>()._currentLevel;
        _levelNum = _scorePass.GetComponent<ScorePass>()._levelNum;

    }

    public void NextLevel()
    {

        _levelToLoad = _nextLevel;

        _backdrop.texture = _loadScreens[_levelNum + 1];

        Destroy(_scorePass);

        StartCoroutine(WaitToLoad());

    }

    public void Retry()
    {

        _levelToLoad = _currentLevel;

        _backdrop.texture = _loadScreens[_levelNum];

        Destroy(_scorePass);

        StartCoroutine(WaitToLoad());

    }

    IEnumerator WaitToLoad()
    {

        _backdrop.enabled = true;
        yield return new WaitForSeconds(3);
        StartCoroutine(AsyncLoad());

    }

    IEnumerator AsyncLoad()
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_levelToLoad);

        yield return null;

    }

}
