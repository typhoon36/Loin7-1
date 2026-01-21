using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //스피드
    public float _MoveSpeed = 3;

    void Update()
    {
        //x쪽값 설정 vector 방향 * 시간 * 스피드
        float distance = Input.GetAxisRaw("Horizontal") * Time.deltaTime * _MoveSpeed;

        //x쪽 이동 설정
        transform.Translate(distance, 0, 0);
    }

}
