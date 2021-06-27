using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public class UserData
{
    //  �L�����N�^�[�̏������Ƃ��Ƃ�
    public string playerName; //  �v���C���[�̖��O
    public int playerLevel;    //  �v���C���[�̃��x��
    public int gameMoney;      //  �Q�[�����ݕ�
    public int jewel;          //  �Ƃ��ׂȐ�
    public UnitData[] units;
    public int[] team;         //  �`�[���f�[�^(Units�̔ԍ������i�[���Ă���)
    public int maxUnit;        //  ���j�b�g�̍ő及����
    
    public void InitUserData()
    {
        playerName = "";
        playerLevel = 1;
        gameMoney = 0;
        jewel = 0;
        maxUnit = 100;
        units = new UnitData[maxUnit];
        team = new int[8];  //  �Ƃ肠�����}�W�b�N�i���o�[
    }
    public void CreateUserData()
    {
        playerName = "test����";
        playerLevel = 1;
        gameMoney = 0;
        jewel = 0;
        maxUnit = 100;
        units = new UnitData[maxUnit];
        for(int n=0; n<5; n++)
        {
            units[n] = new UnitData(n+1);
            team[n] = n;    //  �i�[�ꏊ������
        }
        
    }

}
