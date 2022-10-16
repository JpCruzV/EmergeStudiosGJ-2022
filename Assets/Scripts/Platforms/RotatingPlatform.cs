using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour {

    [SerializeField] Animator anim;

    [SerializeField]
    private float _rotateTime = 1.5f;

    [SerializeField] float force = 15;

    private float _timeToRotate;

    private int _rotationIndex = 0;
    private int[] _rotations = new int[] { 0, 45, 90, 135, 180, 225, 270, 315 };


    private void Update() {

        _timeToRotate += Time.deltaTime;

        if (_timeToRotate > _rotateTime)
        {
            _timeToRotate = 0f;
            RotateCPlatform();
        }
    }

    private void RotateCPlatform() {

        _rotationIndex++;

        if (_rotationIndex >= _rotations.Length)
        {
            _rotationIndex = 0;
        }

        this.transform.localEulerAngles = new Vector3(0f, 0f, _rotations[_rotationIndex]);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        anim.SetBool("Bounce", true);
        StartCoroutine(waitForAnim());
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
    }

    IEnumerator waitForAnim() {

        yield return new WaitForSeconds(1f);
        anim.SetBool("Bounce", false);
    }
}
