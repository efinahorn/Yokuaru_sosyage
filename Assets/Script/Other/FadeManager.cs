using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_Panel = null;
    private Texture2D m_panel{
        //  必須。フェードインアウトする画像　無くてもいいようにはしたいが
        get{
            if( m_Panel == null){    //  非推奨。基本静的に設定
                m_Panel = (Texture2D) Resources.Load( "Resources/Texture/BLACK.png",typeof(Texture2D));
            }
            return m_Panel;
        }
        set{
            m_Panel = value;
        }
    }
    [SerializeField] private Texture2D m_nowLoading = null; // 無くても良い。ロード時にアニメーションさせる用の画像
    private float m_fadeAlpha = 0.0f;  //  フェードアニメーション作業用
    private bool m_isFading = false;    //  フェード中かどうか
    private float m_RotatenowLoading = 0.0f;    //  m_nowLoadingで使用。くるくる回す。

    void Awake()
    {
        m_fadeAlpha = 0f;
        m_isFading = false;
        m_RotatenowLoading = 0.0f;
    }

    public void OnGUI()
    {
        if( !m_isFading)
        {
            return;
        }

        GUI.color = new Color(255, 255, 255, m_fadeAlpha);  //色は初期値入れてます。透過度は非同期で変化
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_panel);  //
        if (m_nowLoading != null) {
            float _loadingTexSize = 200.0f;     //  表示サイズ。ベーススクリーンサイズによっては結構変わる
            float _baseScreenWidth = 1920.0f;   //  ベーススクリーンサイズのWidth、自分が想定している推奨画面サイズでいい
            float _baseScreenHeight = 1080.0f;  //  ベーススクリーンサイズのHeight
            float _rotSpeed = 150.0f;   //  画像の回転速度。超スピードにならないようにデルタタイムで可変してる
            Rect rect0 = new Rect(
                (float)(Screen.width - (_loadingTexSize * (Screen.width / _baseScreenWidth))),  //  右下に表示したいので。左上なら0でいい
                (float)(Screen.height - (_loadingTexSize * (Screen.height / _baseScreenHeight))),   //  上に同じ
                (float)(_loadingTexSize * (Screen.width / _baseScreenWidth)),   //  表示サイズなのでスクリーンサイズに合わせて拡大縮小する必要がある
                (float)(_loadingTexSize * (Screen.width / _baseScreenWidth))   //  上に同じ
                );
            GUIUtility.RotateAroundPivot(m_RotatenowLoading, rect0.center);
            GUI.DrawTexture(rect0, m_nowLoading);
            m_RotatenowLoading += _rotSpeed * Time.deltaTime;
        }
    }
    
    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間( 秒 )</param>
    public void ChangeScene( string scene, float interval )
    {
        if (!m_isFading)
        {
            StartCoroutine(TransScene(scene, interval));
        }
    }

    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="interval">暗転にかかる時間( 秒 )</param>
    /// <returns></returns>
    private IEnumerator TransScene( string scene, float interval )
    {
        // だんだん暗く
        m_isFading = true;
        float time = 0;
        while( time <= interval )
        {
            m_fadeAlpha = Mathf.Lerp( 0f, 1f, time / interval );
            time += Time.deltaTime;
            yield return 0;
        }
        m_fadeAlpha = 1.0f; //  一回マックス値でフェードアウト完了させる。(上の処理だけだとフェード途中でシーンロードする)
        yield return 0;
        // シーン切り替え
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;    // シーン遷移をしない

        while (async.progress < 0.9f)
        {
            //Debug.Log(async.progress);
            yield return new WaitForEndOfFrame();
        }
#if UNITY_EDITOR
        Debug.Log("Scene Loaded. Scene name is \"" + scene + "\".");
#endif
        yield return new WaitForSeconds(1);

        async.allowSceneActivation = true;    // シーン遷移許可

        // だんだん明るく
        time = 0;
        while( time <= interval )
        {
            m_fadeAlpha = Mathf.Lerp( 1f, 0f, time / interval );
            time += Time.deltaTime;
            yield return 0;
        }

        // フェード終了
        m_isFading = false;
    }

    /// <summary>
    /// フェード状態の取得
    /// </summary>
    /// <returns>今フェードしているかどうか</returns>
    public bool isFading()
    {
        return m_isFading;
    }
}
