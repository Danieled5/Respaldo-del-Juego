using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset = new Vector3(15f, 2f, 0f);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 camPos = transform.position;
            Vector3 desiredPos = player.transform.position;

            Vector3 smoothedPos = Vector3.Lerp(camPos, desiredPos, 0.125f);

            transform.position = new Vector3(smoothedPos.x, transform.position.y, transform.position.z);
        }
    }
}
