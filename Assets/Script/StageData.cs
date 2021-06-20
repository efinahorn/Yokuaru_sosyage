using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    private string map_Matrix;   //  �}�b�v�̃}�X�z�u��񂪓����Ă���
    private SquaresData[][] squaresData;  //  �}�X���̂̏��
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
        int[] objcnt = new int[(int)SquareType.MAX];
        squaresData = new SquaresData[map_matrix_arr.Length][];
        for (int z = 0; z < map_matrix_arr.Length; z++)
        {
            //x�����ɔz��̗v�f�����o��
            string z_map = map_matrix_arr[z];
            //�P�z��Ɋi�[����Ă��镶���̐���x�������[�v
            for (int x = 0; x < z_map.Length; x++)
            {
                //�z�񂩂���o�����P�v�f�ɒl�������Ă���̂ł�����P�����Â��o��
                int obj = int.Parse(z_map.Substring(x, 1));
                squaresData[z][x] = new SquaresData();
                squaresData[z][x].setSquareData((byte)obj, (byte)objcnt[obj]);
                objcnt[obj]++;
            }
        }

        return true;
    }

    /// <summary>
    /// �X�|�[���n�_�̍��W��Ԃ�
    /// </summary>
    /// <param name="num">�w��̃X�|�[���n�_�̍��W��Ԃ��B-1�ł���Έ�ԎႢ�X�|�[���B</param>
    /// <returns></returns>
    public Vector2 getSpawnPos(int num)
    {
        for (int z = 0; z < squaresData.Length; z++)
        {
            for (int x = 0; x < squaresData[z].Length; x++)
            {
                SquaresData sqdata = squaresData[z][x];
                if ( sqdata.squareType == (byte)SquareType.SPAWN)
                {
                    if(num == sqdata.squareNum || num < 0)
                    {
                        return new Vector2(x+1, z+1);   //  �����W��+1�̒n�_�ŗL��
                    }
                }
            }
        }

        return new Vector2(-1,-1);   //  ������Ȃ������B
    }
}
