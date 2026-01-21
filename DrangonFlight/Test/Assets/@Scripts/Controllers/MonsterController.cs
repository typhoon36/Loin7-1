using UnityEngine;


public enum MonsterType
{
    Goblin,
    Orc,
    Slime
}
[System.Serializable]
public class MonsterSpawnData
{
    public MonsterType type;
    public GameObject prefab;
}

public class MonsterController : MonoBehaviour
{
   
    [SerializeField]  GameObject _originalPrefab;
    public float _MoveSpeed = 1f;


    // 외부에서 원본 프리팹을 주입하기 위한 함수
    public void SetOriginalPrefab(GameObject prefab)
    {
        _originalPrefab = prefab;
    }
     void Update()
    {
        //움직임을 변수로 만들기
        float distanceY = _MoveSpeed * Time.deltaTime; //움직이기
        transform.Translate(0, -distanceY, 0);
    }


     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Die();

        
        }
    }

    public void Die()
    {
        // 풀 키가 없다면(= Monster(Clone)이 들어간다거나, 세팅 안된 경우) 바로 로그로 잡히게
        if (_originalPrefab == null)
        {
            Debug.LogError($"[MonsterController] Original Prefab is NULL. " +
                           $"Spawn 할 때 SetOriginalPrefab(enemyPrefab) 호출했는지 확인하세요. name={gameObject.name}");
            Destroy(gameObject);
            return;
        }

        PoolManager.Instance.Push(_originalPrefab, gameObject);

        SoundManager.instance.SoundDie();
        GameManager.Instance.AddScore(100);
    }
}
