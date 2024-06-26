using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButtom : MonoBehaviour
{
    public static Vector3 NextPosition;
    public static bool firstHit;
    private void Start()
    {
        firstHit = false;
        NextPosition = transform.position;
    }
    public GameObject FirstBall;
    public bool checkInitMatrixFirst = true;

    public static Vector3 posFirst;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "ballFirst")
        {
            if (checkInitMatrixFirst)
            {
                Matrix.initBox(Data.rowY);
                checkInitMatrixFirst = !checkInitMatrixFirst;
            }

        }
     
        if (collision.transform.tag == "bullet")
        {
            Data.countBulletHide += 1;

            if (GameData.boolFisrtBall)
            {
                posFirst = collision.gameObject.transform.position;
                FirstBall.transform.position = posFirst;
                collision.gameObject.SetActive(false);
                GameData.boolFisrtBall = false;
            }
            else
            {
                collision.transform.GetComponent<CircleCollider2D>().enabled = false;
                collision.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                collision.transform.GetComponent<BallMoveto>().Move = true;
            }

            if (Data.countBulletHide == 10)
            {
                Data.checkShoot = true;
                Data.countBulletHide = 0; 

            //   Matrix.checkCellOfRowHide();
           

            }

            FirstBall.transform.gameObject.SetActive(true);
        }
    }

}
