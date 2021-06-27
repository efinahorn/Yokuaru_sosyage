using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ただ回るだけのスクリプト
/// </summary>
public class RotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(10.0f * Time.deltaTime, 0, 0));
    }
}
