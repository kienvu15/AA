using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSytem : MonoBehaviour
{
    public Transform Target;
    public Vector3 posOffset;
    public float smooth;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + posOffset, smooth * Time.deltaTime);
    }
}
