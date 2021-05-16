using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanController : MonoBehaviour
{
    // アニメーションするためのコンポーネントを入れる
    Animator animator;
    // Unityちゃんを移動させるコンポーネントの追加
    Rigidbody2D rigid2D;
    // 地面の位置
    private float groundLevel = -3.0f;
    // ジャンプの速度の減衰
    private float dump = 0.8f;
    // ジャンプ速度の追加
    float jumpVelocity = 20;
    // ゲームオーバーになる位置
    private float deadLine = -9;


    // Start is called before the first frame update
    void Start()
    {
        // アニメーターのコンポーネントを取得
        this.animator = GetComponent<Animator>();

        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 走るアニメーションを再生するために、Animatorのパラメータを調整する
        this.animator.SetFloat("Horizontal", 1);

        // 着地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);

        // ジャンプ状態のときにはボリュームを0にする（追加）
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        // 着地状態でクリックされたときの処理
        if (Input.GetMouseButton(0) && isGround)
        {
            // 上方向の力をかける
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }

        //　クリックやめたら上方向への速度を減衰する
        if (Input.GetMouseButton(0) == false)
        {
            if (rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= this.dump;
            }
        }

        // デッドラインを超えた場合ゲームオーバーにする（追加）
        if (transform.position.x < this.deadLine)
        {
            // UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示する（追加）
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            // ユニティちゃんを破棄する（追加）
            Destroy(gameObject);
        }
    }
}