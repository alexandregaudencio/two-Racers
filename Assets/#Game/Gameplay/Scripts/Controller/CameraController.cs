using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject targetPlayer;

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z - 10f);

    }
}
