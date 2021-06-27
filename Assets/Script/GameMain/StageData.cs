using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public enum SquareType
    {
        EMPTY = 0,
        WALL = 1,
        GOAL = 2,
        SPAWN = 3,
        MAX,
    }
    private string map_Matrix;   //  �}�b�v�̃}�X�z�u��񂪓����Ă���
    public int[,] squaresData = new int[10,13];  //  �}�X���̂̏��

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �}�b�v�}�g���b�N�X����}�X����o�^���Ă����B�����I�u�W�F�N�g�̏��͌���ǂ��ł�����
    /// 3
    /// 2
    /// 1
    ///  123456789  ��������_�Ƃ��čl����   
    /// </summary>
    /// <param name="mapMatrix">�}�b�v���</param>
    /// <returns></returns>
    public bool setData( string mapMatrix)
    {
        map_Matrix = mapMatrix;
        string[] map_matrix_arr = map_Matrix.Split(':');    //  �z�񐔂�Z��

        for (int z = 0; z < map_matrix_arr.Length; z++)
        {
            //x�����ɔz��̗v�f�����o��
            string z_map = map_matrix_arr[z];
            //�P�z��Ɋi�[����Ă��镶���̐���x�������[�v
            for (int x = 0; x < z_map.Length; x++)
            {
                //�z�񂩂���o�����P�v�f�ɒl�������Ă���̂ł�����P�����Â��o��
                int obj = int.Parse(z_map.Substring(x, 1));
                squaresData[z,x] = obj;
            }
        }

        return true;
    }

    /// <summary>
    /// �X�|�[���n�_�̍��W��Ԃ�
    /// </summary>
    /// <param name="num">�w��̃X�|�[���n�_�̍��W��Ԃ��B-1�ł���Έ�ԎႢ�X�|�[���B</param>
    /// <returns></returns>
    public Vector3 GetSpawnPos(int num = -1)
    {
        Vector3 spawnPos = new Vector3(-1, 0, -1);
        int spawnCnt = 0;
        for (int z = 0; z < squaresData.GetLength(0); z++)
        {
            for (int x = 0; x < squaresData.GetLength(1); x++)
            {
                if (squaresData[z, x] == (int)SquareType.SPAWN)
                {
                    if (num == spawnCnt || num < 0)
                    {
                        return new Vector3(x + 1, 0, z + 1);   //  �����W��+1�̒n�_�ŗL��
                    }
                    else
                    {
                        //  ���ɂ��̌㌩����Ȃ������猻�󌩂������Ԃ�
                        spawnPos.Set(x + 1, 0, z + 1);
                    }
                    spawnCnt++;
                }
            }
        }

        return spawnPos;   //  ������Ȃ������B
    }

    /// <summary>
    /// �S�[���n�_�̍��W��Ԃ�
    /// </summary>
    /// <param name="num">�w��̃S�[���n�_�̍��W��Ԃ��B-1�ł���Έ�ԎႢ�S�[���B</param>
    /// <returns></returns>
    public Vector3 GetGoalPos(int num = -1)
    {
        Vector3 goalPos = new Vector3(-1, 0, -1);
        int spawnCnt = 0;
        for (int z = 0; z < squaresData.GetLength(0); z++)
        {
            for (int x = 0; x < squaresData.GetLength(1); x++)
            {
                if (squaresData[z, x] == (int)SquareType.GOAL)
                {
                    if (num == spawnCnt || num < 0)
                    {
                        return new Vector3(x + 1, 0, z + 1);   //  �����W��+1�̒n�_�ŗL��
                    }
                    else
                    {
                        //  ���ɂ��̌㌩����Ȃ������猻�󌩂������Ԃ�
                        goalPos.Set(x + 1, 0, z + 1);
                    }
                    spawnCnt++;
                }
            }
        }

        return goalPos;   //  ������Ȃ������B
    }

    /// <summary>
    /// �w����W�Ɉ�ԋ߂��}�X����Ԃ�
    /// </summary>
    public int GetSquareData(Vector3 pos)
    {
        if (pos.x < squaresData.GetLength(1) 
            && pos.x > 0 
            && pos.z < squaresData.GetLength(0) 
            && pos.z > 0) return squaresData[(int)pos.z-1, (int)pos.x-1];
        return -1;  //  �G���A�A�E�g���Ă�
    }
}
