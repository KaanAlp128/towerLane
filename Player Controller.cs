using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    public float speed;
    private CharacterController characterController;
    public GameObject projectile;
    private float launchVelocity = 1000f;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        characterController.Move(move * Time.deltaTime * speed);
        LaunchProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void LaunchProjectile()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject fireball = Instantiate(projectile, transform.position, transform.rotation);
            fireball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity, launchVelocity * 10));
            Debug.Log("Pew Pew");
        }
    }
}
