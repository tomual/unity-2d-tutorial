using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject ball;
    int health = 5;
    Text healthText;
    
    void Start()
    {
        ball = GameObject.Find("Ball");
        ball.SetActive(false);

        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health :" + health;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector3 direction = target - transform.position;
            var clone = Instantiate(ball, transform.position, transform.rotation);
            clone.SetActive(true);
            clone.GetComponent<Rigidbody2D>().AddForce(direction * 100);
        }
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Enemy"))
        {
            health = health - 1;
            healthText.text = "Health :" + health;
            if (health <= 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}


