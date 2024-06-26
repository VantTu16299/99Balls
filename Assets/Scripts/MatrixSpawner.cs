using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixSpawner: MonoBehaviour
{
    public Sprite newSprite;
    public GameObject gameObjectParent;
    //public GameObject gameObjectItemPrefab;
    public int y = 9; // Số hàng của ma trận
    public int x = 7; // Số cột của ma trận
    //public static GameObject[,] gridObjects;

    void Start()
    {
        Data.gridObjects = new GameObject[y, x];
        BoxCollider2D parentCollider = gameObjectParent.GetComponent<BoxCollider2D>();

        if (parentCollider == null)
        {
            Debug.LogError("GameObjectParent must have a BoxCollider2D component.");
            return;
        }

        Vector2 parentSize = parentCollider.size;
        float cellWidth = parentSize.x / x;
        float cellHeight = parentSize.y / y;

        for (int row = 0; row < y; row++)
        {
            for (int col = 0; col < x; col++)
            {
                // Tính toán vị trí spawn sao cho nó nằm trong BoxCollider2D của gameObjectParent
                float spawnX = (col + 0.5f) * cellWidth - parentSize.x / 2f;
                float spawnY = -((row + 0.5f) * cellHeight - parentSize.y / 2f);
                Vector2 spawnPos = new Vector2(spawnX, spawnY);

                GameObject newObject = ObjectPools.SharedInstance.GetObjectFromPool(1);
                //   GameObject newObject = Instantiate(gameObjectItemPrefab, spawnPos, Quaternion.identity);

                if (newObject != null)
                {
                    // Debug.Log("n =====>" + row + "," + col);
                    newObject.transform.position = spawnPos;
                    Data.gridObjects[row, col] = newObject;

                    //    Text textComponent = newObject.GetComponentInChildren<Text>();
                    // textComponent.text = randomtxt.ToString();

                    //  textComponent.text = row + "," + col;
                    newObject.transform.SetParent(gameObjectParent.transform, false);
                    SpriteRenderer spriteRendere = newObject.GetComponent<SpriteRenderer>();
                    newObject.GetComponent<SpriteRenderer>().sprite = newSprite;

                }
            }

            //173.91 , -1.82
            gameObjectParent.transform.rotation = Quaternion.Euler(-1.82f, 1.77f, 0f);

        } 
    }
}
