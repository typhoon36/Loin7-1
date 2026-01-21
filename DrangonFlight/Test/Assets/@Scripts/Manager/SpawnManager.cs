using System.Collections;
using UnityEngine;

[System.Serializable]
public class MonsterTypeTable
{
    public Defines.EMonsterType type;


    public float weight = 1f;

    public GameObject[] prefabs;
}

public class SpawnManager : Singleton<SpawnManager>
{

    public MonsterTypeTable[] tables;

    public float spawnDelay = 0.5f;

    float[] spawnX = { -2.23f, -1.06f, 0.25f, 1.38f, 2.34f };

     void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        GameObject selectedPrefab = PickPrefabByType();
        if (selectedPrefab == null)
        {
            Debug.LogError("[SpawnManager] Spawn failed: selectedPrefab is null. tables 설정을 확인하세요.");
            return;
        }

        int posIndex = Random.Range(0, spawnX.Length);
        Vector3 pos = new Vector3(spawnX[posIndex], transform.position.y, 0);

        GameObject enemy = PoolManager.Instance.Pop(selectedPrefab);
        enemy.transform.position = pos;

        MonsterController mc = enemy.GetComponent<MonsterController>();
        if (mc != null)
            mc.SetOriginalPrefab(selectedPrefab);
    }

    GameObject PickPrefabByType()
    {
        if (tables == null || tables.Length == 0)
            return null;

        float total = 0f;
        for (int i = 0; i < tables.Length; i++)
        {
            if (tables[i] == null) continue;
            if (tables[i].prefabs == null || tables[i].prefabs.Length == 0) continue;

            total += Mathf.Max(0f, tables[i].weight);
        }

        if (total <= 0f)
            return null;

        float r = Random.Range(0f, total);
        MonsterTypeTable chosen = null;

        for (int i = 0; i < tables.Length; i++)
        {
            var t = tables[i];
            if (t == null) continue;
            if (t.prefabs == null || t.prefabs.Length == 0) continue;

            float w = Mathf.Max(0f, t.weight);
            if (w <= 0f) continue;

            r -= w;
            if (r <= 0f)
            {
                chosen = t;
                break;
            }
        }

        if (chosen == null)
            chosen = tables[tables.Length - 1];

        // 2) 그 타입 안에서 프리팹 랜덤
        int idx = Random.Range(0, chosen.prefabs.Length);
        return chosen.prefabs[idx];
    }
}
