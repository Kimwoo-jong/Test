using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePool : MonoBehaviour
{
    private List<GameObject> spherePool;
    public GameObject spherePrefab;

    public void Initialize(int size)
    {
        spherePool = new List<GameObject>();

        for (int i = 0; i < size; ++i)
        {
            GameObject obj = Instantiate(spherePrefab);
            obj.SetActive(false);
            spherePool.Add(obj);

            obj.transform.parent = this.gameObject.transform;
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject obj in spherePool)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(spherePrefab);
        newObj.SetActive(true);
        spherePool.Add(newObj);

        newObj.transform.parent = this.gameObject.transform;
        
        return newObj;
    }
}
