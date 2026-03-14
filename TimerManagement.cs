using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerManagement : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _timerCount;

    [SerializeField]
    private float _countdownTime = 30f;

    [SerializeField]
    private float _videoLength;

    [SerializeField]
    private int _mins;

    [SerializeField]
    private int _secs;

    [SerializeField]
    private int _noOfPhones;

    [SerializeField]
    private TextMeshProUGUI _timer;

    [SerializeField]
    private TextMeshProUGUI _escapeText;

    [SerializeField]
    private RawImage _videoPlayer;

    [SerializeField]
    private RawImage _backdrop;

    [SerializeField]
    private string _nextScene;

    [SerializeField]
    private string _previousScene;

    [SerializeField]
    private GameObject _scoreSaver;

    [SerializeField]
    private GameObject _skipIntroButton;

    private bool _isCountingDown;

    private bool _isIntroduced;

    public string _savedTime;

    public int _phoneSmashCount;

    public int _soCount;

    public int _objectSmashCount;

    public int _doorSmashCount;

    public int _enemiesKilled;

    public int _secondsElapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(WaitForIntro());

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (_isIntroduced == false)
            return;

        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);

        if (_isCountingDown == true && distanceFromPlayer < 1)
            LoadNextScene();

        if (_countdownTime < 1)
        {

            TimerRunOut();

        }

        if (_phoneSmashCount == _noOfPhones)
            BeginEscape();

        if (_isCountingDown == false)
            CountUp();

        if (_isCountingDown == true)
            CountDown();

        _timer.text = string.Format("{0:00}:{1:00}", _mins, _secs);

    }

    void CountUp()
    {

        _timerCount += Time.deltaTime;

        _mins = Mathf.FloorToInt(_timerCount / 60f);
        _secs = Mathf.FloorToInt(_timerCount - _mins * 60);

        _secondsElapsed = (_mins * 60 + _secs);
        _savedTime = _timer.text;

    }

    void CountDown()
    {

        _countdownTime -= Time.deltaTime;

        _mins = Mathf.FloorToInt(_countdownTime / 60f);
        _secs = Mathf.FloorToInt(_countdownTime - _mins * 60);

    }

    void BeginEscape()
    {

        _escapeText.enabled = true;
        _isCountingDown = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;

    }

    void LoadNextScene()
    {

        _countdownTime = 20000;

        StartCoroutine(WaitToLoad());

    }

    void TimerRunOut()
    {

        Destroy(_scoreSaver);

        SceneManager.LoadScene(_previousScene);

    }

    public void AddToSmashCount()
    {

        _phoneSmashCount += 1;

    }

    public void AddToSOCount()
    {

        _soCount += 1;

        Debug.Log(_soCount);

    }

    public void SkipIntro()
    {

        StopCoroutine(WaitForIntro());
        _videoPlayer.enabled = false;
        _backdrop.enabled = false;
        _isIntroduced = true;
        _skipIntroButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    IEnumerator WaitForIntro()
    {

        yield return new WaitForSeconds(_videoLength);
        _videoPlayer.enabled = false;
        _backdrop.enabled = false;
        _isIntroduced = true;
        _skipIntroButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    IEnumerator WaitToLoad()
    {

        _scoreSaver.GetComponent<ScorePass>().LoadScores();
        _backdrop.color = Color.white;
        _backdrop.enabled = true;
        gameObject.transform.position = new Vector3(100f, 100f, 100f);
        yield return new WaitForSeconds(1);
        StartCoroutine(AsyncLoad());

    }

    IEnumerator AsyncLoad()
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(_nextScene);

        yield return null;

    }

}
