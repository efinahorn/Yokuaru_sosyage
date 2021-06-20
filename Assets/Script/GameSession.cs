using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameSession : SingletonMonoBehaviourFast<GameSession>
{
    //----------------------------------------- �f�[�^�x�[�X�n ---------------------------------------
    public UnitDataBase unitDataBase;
    public UserData userData;


    //----------------------------------------- �V�[���J�ڌn ---------------------------------------
    /// <summary>
    /// �V�[���ύX���Ɏg�p�B�������������Ɏ����Ă��Ă�������
    /// ���΂ɃI�[���C���ɂ���ƌ��h���̂ŕʃX�N���v�g�ɂ��Ă�B
    /// ���̂Ƃ���ɏ����ƍ���������������getter�ɏ����Ă邪�����ł����g��Ȃ��̂ŕK�v���͒Ⴂ
    /// </summary>
    private FadeManager m_FadeManager = null;
    private FadeManager m_fadeManager
    {  //  ����v���C�x�[�g�̂܂܂ł���
        get
        {
            if (m_FadeManager == null)
            {
                if (this.GetComponent<FadeManager>() == null)
                { //  �񐄏��B��{�ÓI�ɐݒ肷��
                    this.gameObject.AddComponent<FadeManager>();  //�Ȃ�������Add�B�������̏ꍇ�K�؂ȉ摜�t�@�C����Add�o���Ȃ��@
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

    // ���g�̒l�̏������͂����ł��Ƃ���

    protected override void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);  //  �V�[���J�ڎ��ɔj�����Ȃ�
        }

        if (!CheckInstance())   //  GameSession����������ꍇ�͓o�^�ς݈ȊO�͏���(�����Y��΍�B��{�^�C�g���ȊO�̂͏����Ă���)
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
        //  �G���[�`�F�b�N�B�t���[�Y�h�~�B�[���ȃG���[�̏ꍇ�̓Q�[���I���ē����o���ďI������
        try
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Quit();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    /// <summary>
    /// �Q�[���̏I��
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
    /// �V�[���J�ڊ֐�
    /// </summary>
    /// <param name="scene">�ύX��̃V�[�����B�Y�����Ȃ��ꍇ�̓G���[�Ԃ��悤��</param>
    /// <param name="interval">�Ó]�A���]�ɂ����鎞�ԁB�f�t�H���g��1�b</param>
    public void SceneChange(string scene, float interval = 1.0f)
    {
        m_fadeManager.ChangeScene(scene, interval);
    }

    /// <summary>
    /// �J�ڏ�Ԃ𓾂�
    /// </summary>
    /// <returns>�J�ڒ���</returns>
    public bool isChangeScene()
    {
        return m_fadeManager.isFading();
    }


    //----------------------------------------- �f�[�^�n ---------------------------------------
    /// <summary>
    /// ���[�h����B�^�C�g�����炢�ł������Ȃ��Ǝv��
    /// </summary>
    public void LoadUserData()
    {
        userData.InitUserData();
        if (!System.IO.File.Exists(Application.dataPath + "/filedata.dat"))
        {
            userData.CreateUserData();    //  �Ȃ��ꍇ�͍��
            Save();
        }

        Load();
    }

    //�@�t�@�C���X�g���[��
    private FileStream fileStream;
    //�@�o�C�i���t�H�[�}�b�^�[
    private BinaryFormatter bf;

    public void Save()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //�@�Q�[���t�H���_��filedata.dat�t�@�C�����쐬
            fileStream = File.Create(Application.dataPath + "/filedata.dat");
            //�@�t�@�C���ɃN���X��ۑ�
            bf.Serialize(fileStream, userData);
        }
        catch (IOException e1)
        {
            Debug.Log("�t�@�C���I�[�v���G���[");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }
    }

    public void Load()
    {
        bf = new BinaryFormatter();
        fileStream = null;

        try
        {
            //�@�t�@�C����ǂݍ���
            fileStream = File.Open(Application.dataPath + "/filedata.dat", FileMode.Open);
            //�@�ǂݍ��񂾃f�[�^���f�V���A���C�Y
            userData = bf.Deserialize(fileStream) as UserData;
        }
        catch (FileNotFoundException e1)
        {
            Debug.Log("�t�@�C��������܂���");
        }
        catch (IOException e2)
        {
            Debug.Log("�t�@�C���I�[�v���G���[");
        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }

    }

}
