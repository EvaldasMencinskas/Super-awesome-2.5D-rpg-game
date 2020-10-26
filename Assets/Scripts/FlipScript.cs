using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    public EnemyController controller;
    private int value = 0;

    // Update is called once per frame
    void Update()
    {
        if(!controller.goingLeft && value == 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            value++;
        }
        if (controller.goingLeft && value == 1)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                value--;
            }
    }
}
