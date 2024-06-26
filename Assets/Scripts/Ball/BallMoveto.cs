using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveto : MonoBehaviour
{
    public bool Move;
    private float speed = 30f;
    private float step;
    public static bool firstHit;



    private void Start()
    {
        Move = false;
    }
    private void FixedUpdate()
    {
        step = speed * Time.deltaTime;

        if (Move == true)
        {
            step = speed * Time.deltaTime;

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, WallButtom.posFirst, step);
            if (Vector2.Distance(gameObject.transform.position, WallButtom.posFirst) < 0.0001f)
            {
                this.gameObject.SetActive(false);

                this.transform.GetComponent<CircleCollider2D>().enabled = true;
                this.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                this.transform.GetComponent<BallMoveto>().Move = false;
            }
        }
    }
    private void OnDestroy()
    {
        if (firstHit == false)
        {
            firstHit = true;
        }
        //gameObject.transform.parent.GetComponent<BallSpawner>().list.Remove(this.gameObject);
        // Wall.count++;
    }

    private int collisionCount = 0; // Biến đếm số lần va chạm tại cùng vị trí
    private Vector2 lastCollisionPoint; // Vị trí va chạm cuối cùng

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Left" || collision.gameObject.name == "Right")
        {
            Vector2 currentCollisionPoint = collision.contacts[0].point;

            // Kiểm tra nếu khoảng cách giữa vị trí va chạm hiện tại và cuối cùng là nhỏ
            if (Vector2.Distance(currentCollisionPoint, lastCollisionPoint) < 0.01f)
            {
                collisionCount++; // Tăng biến đếm
            }
            else
            {
                // Đặt lại biến đếm và cập nhật vị trí va chạm cuối cùng
                collisionCount = 1;
                lastCollisionPoint = currentCollisionPoint;
            }

            if (collisionCount > 2)
            {
                // Đổi hướng của gameObject
                ChangeDirection(collision.gameObject.name == "Left" ? 15f : -15f);
            }
        }
    }

    private void ChangeDirection(float angleInDegrees)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Lấy góc hiện tại của vận tốc
            float currentAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Đổi hướng vận tốc với góc mới
            float newAngle = currentAngle + angleInDegrees;
            Vector2 newVelocity = Quaternion.AngleAxis(newAngle, Vector3.forward) * rb.velocity;
            rb.velocity = newVelocity;
        }

        // Đặt lại biến đếm và vị trí va chạm cuối cùng
        collisionCount = 0;
        lastCollisionPoint = Vector2.zero;
    }

}
