using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    private string[] m_work = null;

    //  ゲーム開始
    //  m_work[0] シーン名
    //  m_work[1] フェード時間(floatに変換されます)
    public void SceneChange()
    {
        float interval = float.Parse(m_work[1]);
        GameSession.Instance.SceneChange(m_work[0], interval);
    }

    //  ゲーム終了
    public void Quit()
    {
        GameSession.Instance.Quit();
    }
}
