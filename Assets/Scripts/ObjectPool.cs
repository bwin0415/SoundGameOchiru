using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefabObj;
    List<GameObject> pool = new List<GameObject>();

    public void CreatePool(int maxCount)
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = Instantiate(prefabObj);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObj(Vector2 position)
    {
        // 先在pool中尋找非啟用的物件
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }

        // 如果pool中沒有非啟用的物件，則創建一個新的物件並加入pool
        GameObject newObj = Instantiate(prefabObj, position, Quaternion.identity);
        newObj.SetActive(true);
        pool.Add(newObj);

        return newObj;
    }
}