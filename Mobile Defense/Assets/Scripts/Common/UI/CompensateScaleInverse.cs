using System.Collections;
using System.Collections.Generic;
using TiltFive;
using UnityEngine;

public class CompensateScaleInverse : MonoBehaviour
{
    /// <summary>
    /// The glasses settings from the tilt five manager in the scene.
    /// </summary>
    private GlassesSettings _glassesSettings;
    private GameBoardSettings _gameBoardSettings;

    /// <summary>
    /// The original scale of the board.
    /// </summary>
    private Vector3 _originalBoardScale;

    /// <summary>
    /// The scale of the board to compare with the previous update.
    /// </summary>
    private Vector3 _previousBoardScale;

    /// <summary>
    /// The original object scale.
    /// </summary>
    private Vector3 _originalObjectScale;

    /// <summary>
    /// The original scale ratio.
    /// </summary>
    private Vector3 _originalScaleRatio;

    private void Start()
    {
        TiltFiveManager tiltFiveManager = FindObjectOfType<TiltFiveManager>();
        _glassesSettings = tiltFiveManager.glassesSettings;
        _gameBoardSettings = tiltFiveManager.gameBoardSettings;

        _originalBoardScale = _gameBoardSettings.currentGameBoard.transform.localScale;

        _previousBoardScale = _originalBoardScale;

        _originalObjectScale = transform.localScale;

        // At the start, get the original scale ratio by dividing the original scale by the world space units per physical meter value in the glasses settings.
        _originalScaleRatio = new Vector3(_originalObjectScale.x / _originalBoardScale.x ,
            _originalObjectScale.y / _originalBoardScale.y,
            _originalObjectScale.z / _originalBoardScale.z); 
    }

    private void OnEnable()
    {
        if(_glassesSettings != null)
        {
            CompensateScale();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CompensateScale();
    }

    private void CompensateScale()
    {
        Vector3 boardScale = _gameBoardSettings.currentGameBoard.transform.localScale;

        if(boardScale != _previousBoardScale)
        {
            _previousBoardScale = boardScale;

            // Compensate for the scale change by multiplying the world space units per physical meter value in the glasses settings
            // with the original scale ratio obtained in Start().
            transform.localScale = new Vector3(1/(_originalScaleRatio.x * boardScale.x),
               1/(_originalScaleRatio.x * boardScale.y),
               1/(_originalScaleRatio.x * boardScale.z));
        }
    }
}
