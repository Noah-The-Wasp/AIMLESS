using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class DecalDeletionManager : MonoBehaviour
{

    private GameObject[] _decalsInScene;

    private void Update()
    {

        _decalsInScene = GameObject.FindGameObjectsWithTag("Decal");

        if (_decalsInScene.Length < 25)
            return;

        Destroy(_decalsInScene[0]);

    }

}
