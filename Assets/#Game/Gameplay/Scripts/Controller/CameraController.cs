using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject targetPlayer2;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetPlayer2.transform.position.x, targetPlayer2.transform.position.y, targetPlayer2.transform.position.z - 10f);

    }
}
