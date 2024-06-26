using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{
    public static int yy = 10; // Số hàng của ma trận
    public static int xx = 7; // Số cột của ma trận
   
    public static GameObject matrixParent;
    [SerializeField]
    GameObject broudMatrix;

    //int n = 0;

     void Start()
    {
        for (int y = 0; y >= 0; y--)
        {
            Debug.Log("xxxx: "+ y);
        }
            matrixParent = broudMatrix;
    }
    void Update()
    {

      
        // Check if the E key is pressed
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("y =====>" + GameData.y);
            destroy(matrixParent);

            if (GameData.y > 0)
            {
                GameData.y = GameData.y - 1;
            //    int v = checkCellOfRowHide(GameData.y);
            }
            else
            {
                GameData.y = 0;
            }

            if (Data.rowY > 0)
            {
                Data.rowY -= 1;
            }
            else
            {
                Data.rowY = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            if (Data.rowY > 9)
            {
                Data.rowY = 9;
            }
            var n = Data.rowY;
            initBox(n);
        }
    }

  public static void initBox(int rowCurrent)
    {
        // Data.rowY = checkCellOfRowHide(rowCurrent);
        Debug.Log("rowNext a =====> " + rowCurrent);

        if (rowCurrent < 10)
        {
            System.Random random = new System.Random();
            for (int y = 0; y < yy; y++)
            {
                //int randomProbability = Random.Range(0, 7)
                if (y == rowCurrent)
                {
                    Data.rowY = y;
                    for (int x = 0; x < xx; x++)
                    {
                        khoiTaoDoiTuong(y,x, matrixParent);
                    }
                }
            }
        }
    }


    public static int checkCellOfRowHide(int rowCurrent)
    {
      
      //  Debug.Log("input rowCurrent -------------------------- " + rowNext);
        for (int y = rowCurrent; y >= 0; y--)
        {
            for (int x = 0; x < 7; x++)
            {
                GameObject cell = Data.gridObjects[y, x];
                //Debug.Log("Checking cell --------------------------" + y + "___" + cell.tag + "___" + y + ":" + x + "___" + cell.activeInHierarchy)
                if ((cell.tag.Equals("box") && cell.activeInHierarchy == true)
                    || (cell.tag.Equals("star") && cell.activeInHierarchy == true)
                    || (cell.tag.Equals("point") && cell.activeInHierarchy == true))
                {
                    Debug.Log("Condition met at --------------------------" + y + ":" + x +"___"+(y+1));

                    //Matrix.initBox(y + 1);
                    //break
                    //
                    int n = y + 1;
                    return n;
                }
            }
        }
        return 0;
    }


    private static void khoiTaoDoiTuong(int y, int x, GameObject Parent)
    {
        System.Random random = new System.Random();

        int randomProbability = random.Next(0, 7);
        int[] arr = arrX(randomProbability);
        int randomPoint = random.Next(0, 15);
        int randomStar = random.Next(0, 10);

        GameObject gameObject = Data.gridObjects[y, x];

        if (gameObject.CompareTag("cell") && gameObject.activeInHierarchy)
        {
            Vector3 spawnPosition = gameObject.transform.position;

            if (gameObject != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {

                    if (x == arr[i] && arr[i] == random.Next(0, 7))
                    {
                        gameObject.SetActive(false);
                        gameObject = ObjectPools.SharedInstance.GetObjectFromPool(3);

                    }
                    else if (x == arr[i] && arr[i] == random.Next(0, 10))
                    {
                        gameObject.SetActive(false);
                        gameObject = ObjectPools.SharedInstance.GetObjectFromPool(4);
                    }
                    else if (x == arr[i])
                    {
                        gameObject.SetActive(false);
                        gameObject = ObjectPools.SharedInstance.GetObjectFromPool(2);// Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
                      
                    }
                    gameObject.transform.position = spawnPosition;
                    Data.gridObjects[y, x] = gameObject;
                    //Debug.Log("n =====>" + x + "," + y);
                    gameObject.transform.SetParent(Parent.transform, true);
                }
            }
        }
    }

    private static int randomStar(int maxValue)
    {
        System.Random random = new System.Random();
        int value = random.Next(0, maxValue);
       // Debug.Log("randomStar ===> " + value);
        return value;
    }


    private static void destroy(GameObject gameParent)
    {
        for (int x = 0; x < 7; x++)
        {
            GameObject gameObject = Data.gridObjects[GameData.y, x];
            Vector3 spawnPosition = gameObject.transform.position;
            if (gameObject != null && gameObject.activeSelf)
            {
                gameObject.SetActive(false);

               GameObject cell = ObjectPools.SharedInstance.GetObjectFromPool(1);
                if (cell != null)
                {
                   // Debug.Log("position=========>" + x + "," + GameData.y);
                    cell.transform.position = spawnPosition;
                    cell.SetActive(true);
                    Data.gridObjects[GameData.y, x] = cell;
                }

            }


        }
    }

    private static int[] arrX(int choose)
    {
        int[] arr = null;
        switch (choose)
        {
            case 0:
                arr = GenerateRandomArray(1);
                //  Debug.Log("0");
                break;
            case 1:
                arr = GenerateRandomArray(2);
                //  Debug.Log("1");
                break;
            case 2:
                arr = GenerateRandomArray(3);
                //  Debug.Log("2");
                break;
            case 3:
                arr = GenerateRandomArray(4);
                //  Debug.Log("3");
                break;
            case 4:
                arr = GenerateRandomArray(5);
                // Debug.Log("4");
                break;
            case 5:
                arr = GenerateRandomArray(6);
                // Debug.Log("5");
                break;
            case 6:
                arr = GenerateRandomArray(7);
                //  Debug.Log("6");
                break;
        }
        return arr;
    }

    public static int[] GenerateRandomArray(int size)
    {
        System.Random random = new System.Random();
        int[] newArray = new int[size];
        HashSet<int> set = new HashSet<int>();

        for (int i = 0; i < size; i++)
        {
            int val = random.Next(0, 7);

            // Kiểm tra nếu giá trị đã tồn tại trong HashSet
            while (set.Contains(val))
            {
                val = random.Next(0, 7); // Sinh ra giá trị mới nếu trùng lặp
            }

            newArray[i] = val;
            set.Add(val); // Thêm giá trị vào HashSet để kiểm tra trùng lặp trong lần lặp tiếp theo
        }

        //Debug.Log("newArray ========> " + ArrayToString(newArray));
        return newArray;
    }


    static string ArrayToString(int[] array)
    {
        string result = "[";

        for (int i = 0; i < array.Length; i++)
        {
            result += array[i];

            if (i < array.Length - 1)
            {
                result += ", ";
            }
        }

        result += "]";

        return result;
    }
}
