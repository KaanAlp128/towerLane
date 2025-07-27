using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{


    private float x;
    private float y;
    private float sensitivity = -1.0f;
    private Vector3 rotate;
    public GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(-2,1,0);

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");

        rotate = new Vector3(x, y * sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;

    }
}
