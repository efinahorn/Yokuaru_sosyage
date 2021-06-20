using System;
using UnityEngine;

public class GameSession : SingletonMonoBehaviourFast<GameSession>
{
    /// <summary>
    /// シーン変更時に使用。処理をこっちに持ってきてもいいが
    /// 流石にオールインにすると見辛いので別スクリプトにしてる。
    /// 他のところに書くと混乱しそうだからgetterに書いてるがここでしか使わないので必要性は低い
    /// </summary>
    private FadeManager m_FadeManager = null;
    private FadeManager m_fadeManager
    {  //  現状プライベートのままでいい
        get
        {
            if (m_FadeManager == null)
            {
                if (this.GetComponent<FadeManager>() == null)
                { //  非推奨。基本静的に設定する
                    this.gameObject.AddComponent<FadeManager>();  //なかったらAdd。だがこの場合適切な画像ファイルがAdd出来ない　
                }
                m_FadeManager = this.gameObject.GetComponent<FadeManager>();
            }
            return m_FadeManager;
        }
        set
        {
            if (value as FadeManager) m_FadeManager = value;
            else Debug.Log("Error. value as FadeManager Only. value = " + value);
        }
    }

    // 自身の値の初期化はここでやるといい

    protected override void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);  //  シーン遷移時に破棄しない
        }

        if (!CheckInstance())   //  GameSessionが複数ある場合は登録済み以外は消す(消し忘れ対策。基本タイトル以外のは消しておく)
        {
            Debug.Log("GameSession are maltiple, This object destroy.");
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  エラーチェック。フリーズ防止。深刻なエラーの場合はゲーム終了案内を出して終了する
        try
        {

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    /// <summary>
    /// ゲームの終了
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif

    }

    /// <summary>
    /// シーン遷移関数
    /// </summary>
    /// <param name="scene">変更先のシーン名。該当がない場合はエラー返すように</param>
    /// <param name="interval">暗転、明転にかかる時間。デフォルトは1秒</param>
    public void SceneChange(string scene, float interval = 1.0f)
    {
        m_fadeManager.ChangeScene(scene, interval);
    }

    /// <summary>
    /// 遷移状態を得る
    /// </summary>
    /// <returns>遷移中か</returns>
    public bool isChangeScene()
    {
        return m_fadeManager.isFading();
    }
}
