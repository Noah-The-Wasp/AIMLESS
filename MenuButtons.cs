using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{

    [SerializeField]
    private Image _backdrop;

    public void NewGame()
    {

        _backdrop.enabled = true;

        StartCoroutine(WaitToLoad());

    }
    IEnumerator WaitToLoad()
    {

        yield return new WaitForSeconds(3);
        StartCoroutine(AsyncLoad());

    }

    IEnumerator AsyncLoad()
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync("Tutorial");

        yield return null;

    }

}
