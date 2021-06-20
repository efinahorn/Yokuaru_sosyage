using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    private string[] m_work = null;

    //  �Q�[���J�n
    //  m_work[0] �V�[����
    //  m_work[1] �t�F�[�h����(float�ɕϊ�����܂�)
    public void SceneChange()
    {
        float interval = float.Parse(m_work[1]);
        GameSession.Instance.SceneChange(m_work[0], interval);
    }

    //  �Q�[���I��
    public void Quit()
    {
        GameSession.Instance.Quit();
    }
}
