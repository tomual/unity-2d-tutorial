using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = 3f;
        Vector3 target = transform.position + new Vector3(horizontal, vertical);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1);
        }
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1);
        }
    }
}
