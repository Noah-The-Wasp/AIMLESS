using UnityEngine;
using System.Collections;

public class NPCBlink : MonoBehaviour
{

    [SerializeField]
    private Material[] _blinkSequence;

    [SerializeField]
    private float[] _blinkRatios;

    [SerializeField]
    private MeshRenderer _objectMaterial;

    private int _spriteIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {

        yield return new WaitForSeconds(_blinkRatios[_spriteIndex]);

        if (_spriteIndex == 0)
        {

            _objectMaterial.material = _blinkSequence[_spriteIndex];
            _spriteIndex = 1;
            StartCoroutine(Animate());

        }
        else
        {
            _objectMaterial.material = _blinkSequence[_spriteIndex];
            _spriteIndex = 0;
            StartCoroutine(Animate());

        }

    }

}
