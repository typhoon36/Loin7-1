using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get;  set; }

    Dictionary<GameObject, Queue<GameObject>> _pool = new Dictionary<GameObject, Queue<GameObject>>();

     void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject Pop(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("[PoolManager] Pop failed: prefab is null");
            return null;
        }

        if (!_pool.TryGetValue(prefab, out Queue<GameObject> q))
        {
            q = new Queue<GameObject>();
            _pool[prefab] = q;
        }

        GameObject obj;
        if (q.Count > 0)
        {
            obj = q.Dequeue();
            if (obj == null) // ¾À¿¡¼­ ÆÄ±«µÈ °æ¿ì
                return CreateNew(prefab);
        }
        else
        {
            obj = CreateNew(prefab);
        }

        obj.SetActive(true);
        return obj;
    }

    public void Push(GameObject prefab, GameObject obj)
    {
        if (prefab == null)
        {
            Debug.LogError("[PoolManager] Push failed: prefab is null (key must be original prefab)");
            Destroy(obj);
            return;
        }

        if (obj == null)
            return;

        if (!_pool.TryGetValue(prefab, out Queue<GameObject> q))
        {
            q = new Queue<GameObject>();
            _pool[prefab] = q;
        }

        obj.SetActive(false);
        q.Enqueue(obj);
    }

     GameObject CreateNew(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);

        var pooled = obj.GetComponent<MonsterController>();
        if (pooled != null)
            pooled.SetOriginalPrefab(prefab);

        return obj;
    }
}
