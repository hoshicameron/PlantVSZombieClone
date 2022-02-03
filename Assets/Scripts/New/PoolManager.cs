using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    private Dictionary<int,Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

        [SerializeField] private Pool[] pool = null;
        [SerializeField] private Transform objectPoolTransform = null;

        [System.Serializable]
        public struct Pool
        {
            public int poolSize;
            public GameObject prefab;
        }

        private void Start()
        {
            // Create object pools on start
            for (int i = 0; i < pool.Length; i++)
            {
                CreatePool(pool[i].prefab, pool[i].poolSize);
            }
        }

        private void CreatePool(GameObject prefab, int poolSize)
        {
            int poolKey = prefab.GetInstanceID();
            string prefabName = prefab.name;    // Get prefab name

            GameObject parentGameObject=new GameObject(prefabName+"Anchor"); // create parent gameobject to parent the child objects to

            parentGameObject.transform.SetParent(objectPoolTransform);

            if (!poolDictionary.ContainsKey(poolKey))
            {
                poolDictionary.Add(poolKey, new Queue<GameObject>());

                for (int i = 0; i < poolSize; i++)
                {
                    GameObject newGameObject=Instantiate(prefab,parentGameObject.transform)as GameObject;
                    newGameObject.SetActive(false);

                    poolDictionary[poolKey].Enqueue(newGameObject);
                }
            }
        }

        public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            int poolkey = prefab.GetInstanceID();
            if (poolDictionary.ContainsKey(poolkey))
            {
                GameObject objectToReuse = GetObjectFromPool(poolkey);

                ResetObject(position, rotation, objectToReuse, prefab);

                return objectToReuse;
            } else
            {
                Debug.Log("No Object pool for"+ prefab);
                return null;
            }
        }

        private GameObject GetObjectFromPool(int poolkey)
        {
            GameObject objectToReuse = poolDictionary[poolkey].Dequeue();
            poolDictionary[poolkey].Enqueue(objectToReuse);

            // Log to console if object is currently active
            if (objectToReuse.activeSelf == true)
            {
                objectToReuse.SetActive(false);
            }

            return objectToReuse;
        }

        private void ResetObject(Vector3 position, Quaternion rotation, GameObject objectToReuse, GameObject prefab)
        {
            objectToReuse.transform.position = position;
            objectToReuse.transform.rotation = rotation;

            // objectToReuse.GetComponent<Rigidbody2d>().velocity=vector3.zero;
            objectToReuse.transform.localScale = prefab.transform.localScale;

        }
}
