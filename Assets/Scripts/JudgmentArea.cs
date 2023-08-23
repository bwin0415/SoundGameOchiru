using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentArea : MonoBehaviour
{
    // 目標:Goodなどの評価Effectを実装
    // ・表示する文字のPrefabを作成：済
    // ・特定のタイミングでPrefabを生成する：済
    // ・生成したPrefabの文字をかえる => Textコンポーネントを編集する(それ用のクラスを作ってやる)：済

    [SerializeField] float radius;
    [SerializeField] GameManager gameManager = default;

    public KeyCode keyCode;

    [SerializeField] GameObject textEffectPrefab;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {

            playSound();
            // 2つのノーツがきたときに、上のノーツが消えてしまう => 下優先
            // => 複数のノーツを取得して、一番下を消す
            RaycastHit2D[] hits2D = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);
            // 一番下のやつを消す

            if (hits2D.Length == 0)
            {
                return;
            }
            // 一度y座標が小さいもの順で並べ替える(ソートする)
            List<RaycastHit2D> raycastHit2Ds = hits2D.ToList();
            raycastHit2Ds.Sort((a,b) => (int)(a.transform.position.y - b.transform.position.y));
            // 0番目の要素を消す
            RaycastHit2D hit2D = raycastHit2Ds[0];

            // RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, radius, Vector3.zero);
            if (hit2D)
            {
                float distance = Mathf.Abs(hit2D.transform.position.y - transform.position.y);
                if (distance < 1)
                {
                    gameManager.AddScore(100);
                    gameManager.Addpresut(1);
                    // ここでEffect
                    SpawnTextEffect("Excellent", hit2D.transform.position, Color.red);
                }
                else
                {
                    gameManager.AddScore(0);
                    gameManager.Addmresut(1);
                    // ここでEffect
                    SpawnTextEffect("Miss", hit2D.transform.position, Color.blue);
                }
                // ぶつかったものを破壊する
                // Destroy(hit2D.collider.gameObject);
                hit2D.collider.gameObject.SetActive(false);
            }
        }
    }

    void SpawnTextEffect(string message, Vector3 position, Color color)
    {
        GameObject effect = Instantiate(textEffectPrefab, position+Vector3.right*2.5f+Vector3.up*2.5f, Quaternion.identity);
        JudgmentEffect judgmentEffect = effect.GetComponent<JudgmentEffect>();
        judgmentEffect.SetText(message, color);
    }

    // 可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    void playSound()
    {
        audioSource.Play();
    }

    public void HandleKeyCode(KeyCode keyCode)
    {
        // 在这里根据接收到的按键码执行相应的操作
        if (keyCode == this.keyCode)
        {
            // 在这里执行按键对应的操作逻辑
        }
    }
}
