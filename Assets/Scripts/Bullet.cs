using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.transform.tag == "box")
        {
            GameObject cell = ObjectPools.SharedInstance.GetObjectFromPool(1);
            collision.gameObject.SetActive(false);
            cell.transform.position = collision.gameObject.transform.position;
            if (cell != null)
            {
                  cell.SetActive(true);
                  getPositionGameActive(collision.gameObject, cell);
            }
        }
        else if(collision.transform.tag == "point")
        {
            GameObject cell = ObjectPools.SharedInstance.GetObjectFromPool(1);
            collision.gameObject.SetActive(false);
            cell.transform.position = collision.gameObject.transform.position;
            if (cell != null)
            {
                cell.SetActive(true);
                 getPositionGameActive(collision.gameObject, cell);
            }
        }
        else if(collision.transform.tag == "star")
        {
            GameObject cell = ObjectPools.SharedInstance.GetObjectFromPool(1);
            collision.gameObject.SetActive(false);
            cell.transform.position = collision.gameObject.transform.position;

            if (cell != null)
            {
                getPositionGameActive(collision.gameObject, cell);
                cell.SetActive(true);
            }
        }
    }

    private void getPositionGameActive(GameObject gameCollision, GameObject cell)
    {
       
        for (int y = 0; y < 9; y++) { 
           for(int x = 0; x < 7; x++)
           {
              GameObject game = Data.gridObjects[y, x];
                if ((gameCollision.transform.position.x == game.transform.position.x) 
                && (gameCollision.transform.position.y == game.transform.position.y))
                {
                  //  Data.gridObjects[GameData.y, x] = cell;
                    //gameObject.transform.SetParent(parent.transform, true);
                    Debug.Log("gameActiveTrue =====> " + cell.name + "____" + y + ":" + x);
                }
            }
        } 
    }
}
