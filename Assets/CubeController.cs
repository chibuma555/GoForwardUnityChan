using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    // キューブの移動速度
    private float speed = -12;
    //消滅位置
    private float deadLine = -10;
    //コンポーネントの追加
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceコンポーネントの取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //キューブを移動させる
        transform.Translate(this.speed * Time.deltaTime, 0, 0);
        //画面外に出たら破棄する
        if(transform.position.x < this.deadLine)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // 衝突した相手にタグが付いている場合サウンドを鳴らす
        if (other.gameObject.tag == "CubeTag")
        {
            audioSource.Play();
        }
        if (other.gameObject.tag == "GroundTag")
        {
            audioSource.Play();
        }

    }

}
