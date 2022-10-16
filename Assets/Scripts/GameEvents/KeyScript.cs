using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    Vector3 InitPos;
    [SerializeField] float amp;
    [SerializeField] float freq;


    private void Start() {

        InitPos = this.transform.position;
    }

    private void Update() {

        transform.position = new Vector3(InitPos.x, Mathf.Sin(Time.time * freq) * amp + InitPos.y, InitPos.z);
    }
}
