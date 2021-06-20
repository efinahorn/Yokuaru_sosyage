using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    private string map_Matrix;   //  マップのマス配置情報が入っている
    private SquaresData[][] squaresData;  //  マス自体の情報
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
        int[] objcnt = new int[(int)SquareType.MAX];
        squaresData = new SquaresData[map_matrix_arr.Length][];
        for (int z = 0; z < map_matrix_arr.Length; z++)
        {
            //xを元に配列の要素を取り出す
            string z_map = map_matrix_arr[z];
            //１配列に格納されている文字の数でx軸をループ
            for (int x = 0; x < z_map.Length; x++)
            {
                //配列から取り出した１要素に値が入っているのでこれを１文字づつ取り出す
                int obj = int.Parse(z_map.Substring(x, 1));
                squaresData[z][x] = new SquaresData();
                squaresData[z][x].setSquareData((byte)obj, (byte)objcnt[obj]);
                objcnt[obj]++;
            }
        }

        return true;
    }

    /// <summary>
    /// スポーン地点の座標を返す
    /// </summary>
    /// <param name="num">指定のスポーン地点の座標を返す。-1であれば一番若いスポーン。</param>
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
                        return new Vector2(x+1, z+1);   //  実座標は+1の地点で有る
                    }
                }
            }
        }

        return new Vector2(-1,-1);   //  見つからなかった。
    }
}
