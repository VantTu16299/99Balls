using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools: MonoBehaviour
{
   public GameObject[] objectPrefabs;
   public static ObjectPools SharedInstance;

   public List<List<GameObject>> pooledObjects;
  //  public GameObject objectToPool;
    //public int amountToPool = 100;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<List<GameObject>>();

        // Initialize the object pools for each object type
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            int tatol = numPool(i);

            List<GameObject> pool = new List<GameObject>();
            for (int j = 0; j < tatol; j++)
            {
                GameObject obj = Instantiate(objectPrefabs[i]);
                obj.SetActive(false);
                pool.Add(obj);
            }
            pooledObjects.Add(pool);
        }
    }

    private static int numPool(int type)
    {
        switch (type)
        {
            case 0:  return 56; // cell
            case 1:  return 100;// dot dir
            case 2:  return 100; // box
            case 3: return 50; // star
            case 4: return 50; // point
            case 5: return 150; // point
        }

        return 0;
    }
    public GameObject GetObjectFromPool(int objectTypeIndex)
    {
        if (objectTypeIndex >= 0 && objectTypeIndex < objectPrefabs.Length)
        {
            for (int i = 0; i < pooledObjects[objectTypeIndex].Count; i++)
            {
                if (!pooledObjects[objectTypeIndex][i].activeInHierarchy)
                {
                    pooledObjects[objectTypeIndex][i].SetActive(true);
                    return pooledObjects[objectTypeIndex][i];
                }
            }
            return null;
        }
        else
        {
            Debug.LogError("Invalid objectTypeIndex: " + objectTypeIndex);
            return null;
        }
    }
}
