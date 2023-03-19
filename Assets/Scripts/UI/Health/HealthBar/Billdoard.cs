using System;
using UnityEngine;

public class Billdoard : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
