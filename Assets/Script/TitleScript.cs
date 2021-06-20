using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    [SerializeField]
    private string[] m_work = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SceneChange();
            return;
        }

        //  タップ処理
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SceneChange();
                return;
            }
        }
    }

    /// <summary>
    /// シーン遷移呼び出し
    /// </summary>
    void SceneChange()
    {
        float _interval = float.Parse(m_work[1]);
        GameSession.Instance.SceneChange(m_work[0], _interval);
    }
}
