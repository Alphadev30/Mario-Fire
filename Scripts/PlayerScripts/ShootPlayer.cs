using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class ShootPlayer : MonoBehaviour
{
    public GameObject fireBullet;
    private int noOfBullet = 6;
    
    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if(noOfBullet > 0)
        {
            if (CrossPlatformInputManager.GetButtonDown("FireButton"))
            //if(Input.GetKeyDown(KeyCode.J))
            {
                GameObject bullet = Instantiate (fireBullet, transform.position, Quaternion.identity);
                bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
                noOfBullet--;
            }
        }
        
    }
}
