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
    private string map_Matrix;   //  マップのマス配置情報が入っている
    public int[,] squaresData = new int[10,13];  //  マス自体の情報

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// マップマトリックスからマス情報を登録していく。正直オブジェクトの情報は現状どうでもいい
    /// 3
    /// 2
    /// 1
    ///  123456789  左下を基点として考える   
    /// </summary>
    /// <param name="mapMatrix">マップ情報</param>
    /// <returns></returns>
    public bool setData( string mapMatrix)
    {
        map_Matrix = mapMatrix;
        string[] map_matrix_arr = map_Matrix.Split(':');    //  配列数はZ軸

        for (int z = 0; z < map_matrix_arr.Length; z++)
        {
            //xを元に配列の要素を取り出す
            string z_map = map_matrix_arr[z];
            //１配列に格納されている文字の数でx軸をループ
            for (int x = 0; x < z_map.Length; x++)
            {
                //配列から取り出した１要素に値が入っているのでこれを１文字づつ取り出す
                int obj = int.Parse(z_map.Substring(x, 1));
                squaresData[z,x] = obj;
            }
        }

        return true;
    }

    /// <summary>
    /// スポーン地点の座標を返す
    /// </summary>
    /// <param name="num">指定のスポーン地点の座標を返す。-1であれば一番若いスポーン。</param>
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
                        return new Vector3(x + 1, 0, z + 1);   //  実座標は+1の地点で有る
                    }
                    else
                    {
                        //  仮にその後見つからなかったら現状見つけたやつを返す
                        spawnPos.Set(x + 1, 0, z + 1);
                    }
                    spawnCnt++;
                }
            }
        }

        return spawnPos;   //  見つからなかった。
    }

    /// <summary>
    /// ゴール地点の座標を返す
    /// </summary>
    /// <param name="num">指定のゴール地点の座標を返す。-1であれば一番若いゴール。</param>
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
                        return new Vector3(x + 1, 0, z + 1);   //  実座標は+1の地点で有る
                    }
                    else
                    {
                        //  仮にその後見つからなかったら現状見つけたやつを返す
                        goalPos.Set(x + 1, 0, z + 1);
                    }
                    spawnCnt++;
                }
            }
        }

        return goalPos;   //  見つからなかった。
    }

    /// <summary>
    /// 指定座標に一番近いマス情報を返す
    /// </summary>
    public int GetSquareData(Vector3 pos)
    {
        if (pos.x < squaresData.GetLength(1) 
            && pos.x > 0 
            && pos.z < squaresData.GetLength(0) 
            && pos.z > 0) return squaresData[(int)pos.z-1, (int)pos.x-1];
        return -1;  //  エリアアウトしてる
    }
}
