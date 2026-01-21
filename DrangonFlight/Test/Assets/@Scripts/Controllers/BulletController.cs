using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _moveSpeed = 1;
    public GameObject EffectObj;

    void Start()
    {

    }


    void Update()
    {
        //y축 이동
        transform.Translate(0, _moveSpeed * Time.deltaTime, 0);
    }

    //화면밖으로 나가면 호출되는 함수
     void OnBecameInvisible()
    {
        //미사일 지우기
        Destroy(gameObject);
    }
   
}
