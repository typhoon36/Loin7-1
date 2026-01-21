using UnityEngine;

public class SingletonTest : MonoBehaviour
{
    public static SingletonTest instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //자기참조객체
        }
    }


    //public void PlayerSound()
    //{
    //    Debug.Log("플레이어 사운드야");
    //}


    //public void EnemySound()
    //{
    //    Debug.Log("플레이어 사운드야");
    //}
}
