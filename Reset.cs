using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Reset : MonoBehaviour
{

    [SerializeField]
    private string _sceneName;

    [SerializeField]
    private GameObject _scoreSaver;

    [SerializeField]
    private RawImage _blackout;

    [SerializeField]
    private RawImage _wham;

    [SerializeField]
    private bool _isDefault;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "KillPlane")
        {

            _isDefault = true;

            StartCoroutine(ShowDeath());

        }
        else if (other.tag == "Deathbox")
        {

            _isDefault = false;

            StartCoroutine(ShowDeath());

        }

    }

    IEnumerator ShowDeath()
    {

        _blackout.enabled = true;

        if (_isDefault == false)
            _wham.enabled = true;

        yield return new WaitForSeconds(3);

        Destroy(_scoreSaver);

        SceneManager.LoadScene(_sceneName);

    }

}
