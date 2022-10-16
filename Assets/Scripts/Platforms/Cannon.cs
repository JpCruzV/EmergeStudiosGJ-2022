using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float _launchSpeed = 1f;

    [SerializeField]
    private float _rotateTime = 1.5f;

    private bool _isGrabbingPlayer;
    private float _timeToRotate;

    private int _rotationIndex = 0;
    private int[] _rotations = new int[] { 0, 45, 90, 135, 180, 225, 270, 315 };

    private void Update()
    {
        if(_isGrabbingPlayer)
        {
            _timeToRotate += Time.deltaTime;

            if(_timeToRotate > _rotateTime)
            {
                _timeToRotate = 0f;
                RotateCannon();
            }
        }
    }

    private void RotateCannon()
    {
        _rotationIndex++;

        if(_rotationIndex >= _rotations.Length)
        {
            _rotationIndex = 0;
        }

        this.transform.localEulerAngles = new Vector3(0f, 0f, _rotations[_rotationIndex]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player != null)
        {
            player.SetGrabbedByCannon(this);
            SetGrabbingPlayer(true);
        }
    }

    private void SetGrabbingPlayer(bool pValue)
    {
        _isGrabbingPlayer = pValue;
        if (pValue)
        {
            _timeToRotate = 0f; 
        }
    }

    public float GetLaunchSpeed()
    {
        return _launchSpeed;
    }

    public void ReleasePlayer()
    {
        _isGrabbingPlayer = false;
    }
}
