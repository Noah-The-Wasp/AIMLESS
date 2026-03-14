using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class ThoughtControl : MonoBehaviour
{

    [SerializeField]
    private Image _grabbedImage;

    [SerializeField]
    private Image _thoughtImage;

    [SerializeField]
    private Image _bubbleImage;

    [SerializeField]
    private Sprite[] _grabbedSprites;

    [SerializeField]
    private Sprite[] _thoughtSprites;

    [SerializeField]
    private GameObject _audioPlayer;

    private bool _isGrabbing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _bubbleImage.enabled = false;
        _thoughtImage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (_grabbedSprites.Contains(_grabbedImage.sprite))
            HoldingObjective();
        else
            NotHoldingObjective();

    }

    private void HoldingObjective()
    {

        int currentSpriteIndex = System.Array.IndexOf(_grabbedSprites, _grabbedImage.sprite);

        _thoughtImage.sprite = _thoughtSprites[currentSpriteIndex];

        _audioPlayer.SetActive(true);
        _bubbleImage.enabled = true;
        _thoughtImage.enabled = true;

    }

    private void NotHoldingObjective()
    {

        _audioPlayer.SetActive(false);
        _bubbleImage.enabled = false;
        _thoughtImage.enabled = false;

    }

}
