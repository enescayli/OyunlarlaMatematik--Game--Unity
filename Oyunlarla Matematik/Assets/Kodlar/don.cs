using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class don : MonoBehaviour
{
    [Range(0.0f, 20f)]
    public float donme_Hizi;
    void Update()
    {
        transform.Rotate(0, 0, donme_Hizi);
    }
}