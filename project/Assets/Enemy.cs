using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "Ball(Clone)")
        {
            Destroy(collision.gameObject);
            health = health - 1;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
