using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject BulletObj; //미사일 프리팹 가져올 변수

    void Start()
    {

        //InvokeRepeating("함수이름",초기지연시간,지연할 시간);
        InvokeRepeating("Fire", 0.5f, 1f);

    }

    void Fire()
    {
        //미사일 프리팹 , 런쳐포지션, 방향값 없음
        Instantiate(BulletObj, transform.position, Quaternion.identity);

        SoundManager.instance.SoundBullet();
    }
}
