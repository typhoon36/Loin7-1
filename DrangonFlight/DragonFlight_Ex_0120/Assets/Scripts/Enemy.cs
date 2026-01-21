using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    void Start()
    {

    }

    void Update()
    {
        //움직임을 변수로 만들기
        float distanceY = moveSpeed * Time.deltaTime;
        //움직이기
        transform.Translate(0, -distanceY, 0);

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);

            SoundManager.instance.SoundDie();
        }
    }



}
