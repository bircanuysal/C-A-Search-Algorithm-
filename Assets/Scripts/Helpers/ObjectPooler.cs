using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolableObjectTypes
{   
    None,
}
[System.Serializable]
public struct Pool
{
    public PoolableObjectTypes type;
    public GameObject prefab;
    public int size;
}
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<Pool> pools = new();
    private Dictionary<PoolableObjectTypes, HashSet<GameObject>> poolDictionary = new();

    private void Awake()
    {
        Instance = this;

        foreach (var pool in pools)
        {
            HashSet<GameObject> objectPool = new();
            poolDictionary.Add(pool.type, objectPool);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }
    } 

    public GameObject GetPooledObject(PoolableObjectTypes type, bool setObjActive = true)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogError("Pool with type " + type + " doesn't exist.");
            return null;
        }
        foreach (var obj in poolDictionary[type])
        {
            if (!obj.activeInHierarchy)
            {
                if (setObjActive)
                {
                    obj.SetActive(true);
                }
                return obj;
            }
        }
        return OnPoolIsEmpty(type);
    }
    public GameObject GetPooledObject(PoolableObjectTypes type, Transform parentObj, bool setObjActive = true)
    {
        GameObject pooledObject = GetPooledObject(type, setObjActive);

        if (pooledObject != null)
        {
            pooledObject.transform.SetParent(parentObj);
            pooledObject.transform.localPosition = Vector3.zero;
        }

        return pooledObject;
    }
    private GameObject OnPoolIsEmpty(PoolableObjectTypes type)
    {
        var pool = pools.Find(p => p.type == type);

        if (pool.prefab == null)
        {
            Debug.LogError("No prefab found for type " + type);
            return null;
        }

        GameObject obj = Instantiate(pool.prefab);
        obj.SetActive(true);

        poolDictionary[type].Add(obj);

        return obj;
    }
    
    public void ReturnToPool(GameObject objToReturn)
    {
        if (objToReturn == null) return;

        if (!objToReturn.activeSelf) { return; }

        PoolableObjectTypes type = PoolableObjectTypes.None;

        if (objToReturn.TryGetComponent(out IObjectPoolable objectPoolable))
        {
            type = objectPoolable.PoolableObjectType();
        }

        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogError("Trying to return not pooled object to pool " + objToReturn.name);
            return;
        }

        if (poolDictionary[type].Add(objToReturn))
        {
            objectPoolable.OnReturnToPool();
            objToReturn.SetActive(false);
        }
    }
}
