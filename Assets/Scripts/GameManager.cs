using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using Klak.Timeline.Midi;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// @今後やること
    /// ＊1weekで作ったものと同様のシステムを実装していく
    /// ・ノーツが落ちてくるのを3レーンにする（済）
    /// ・ノーツを叩く場所を3レーンにする（済）
    /// ・ノーツが重なった場合にうまく消えないバグ修正
    /// ・ノーツのずれ修正

    /// ＊リクエストがあればYouTubeメンバーシップへ
    /// ・ノーツが下までいったら破壊する
    /// ・オブジェクトプール（ノーツの再利用）
    /// </summary>


    //・カウントダウンのテキスト
    [SerializeField] Text countDownText = default;
    //・ゲーム終了時のリザルトパネル
    [SerializeField] GameObject resultPanel = default;
    //・リトライボタン:シーンの再読み込み

    //・ゲーム中のスコア表示（スコア機能:未実装）
    [SerializeField] Text scoreText = default;

    [SerializeField] TextMeshProUGUI perfectTxt = default;
    [SerializeField] TextMeshProUGUI missTxt = default;

    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] MidiControl midiControl; // 在 Inspector 中将 MIDI 控制器组件分配给这个字段


    int score = 0, pcount = 0, mcount = 0;

    void Start()
    {
        StartCoroutine(GameMain());
        missTxt.text = "0";
        perfectTxt.text = "0";
    }

    IEnumerator GameMain()
    {
        // 获取BPM值
       //float bpm = midiControl.GetControlValue(0); // 假设 BPM 控制器的 ID 是 0
        float bpm = 80; // 假设 BPM 控制器的 ID 是 0

        // 使用BPM值计算每个节拍的时间间隔
        float beatInterval = 60f / bpm;

        countDownText.text = "3";
        yield return new WaitForSeconds(beatInterval);
        countDownText.text = "2";
        yield return new WaitForSeconds(beatInterval);
        countDownText.text = "1";
        yield return new WaitForSeconds(beatInterval);
        countDownText.text = "GO!";
        yield return new WaitForSeconds(beatInterval * 0.5f);
        countDownText.text = "";
        playableDirector.Play();
    }

    // スコア上昇:scoreの数値を大きくする&UIに反映させる
    // どのタイミングでスコアを上昇させる
    public void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    public void Addpresut(int point)
    {
        pcount += point;
        perfectTxt.text = pcount.ToString();
    }
    public void Addmresut(int point)
    {
        mcount += point;
        missTxt.text = mcount.ToString();
    }

    public void OnEndEvent()
    {
        Debug.Log("ゲーム終了:結果表示");
        resultPanel.SetActive(true);
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnPause()
    {
        Debug.Log("ゲーム一時停止");
        playableDirector.Pause();
    }

    public void OnSPeedUp()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(2);

    }
    public void OnSPeedDown()
    {
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0.5f);

    }
}
