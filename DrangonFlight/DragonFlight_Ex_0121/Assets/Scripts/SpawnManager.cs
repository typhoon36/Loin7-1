using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public bool IsSpawn = false;

    public GameObject enemy;


    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 0.5f);
    }
    public void SpawnEnemy()
    {
        float RandomX = Random.Range(-2.5f, 2.5f);

        if (IsSpawn)
        {
            Instantiate(enemy, new Vector3(RandomX, transform.position.y, 0), Quaternion.identity);
        }

    }

    private void Update()
    {

    }

}

