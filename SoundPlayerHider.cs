using UnityEngine;
using System.Collections;

public class SoundPlayerHider : MonoBehaviour
{

    [SerializeField]
    private GameObject _self;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {

        if (_self.activeInHierarchy == false)
            return;

        StartCoroutine(DisableSelf());

    }

    IEnumerator DisableSelf()
    {

        yield return new WaitForSeconds(0.2f);

        _self.SetActive(false);

    }

}
