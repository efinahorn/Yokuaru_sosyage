using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// 参考:https://raharu0425.hatenablog.com/entry/20130131/1359601502
/// </summary>
public class StageGenerator : MonoBehaviour
{
	public GameObject parentPrefab;
	public GameObject wallPrefab;
	public GameObject spawnPrefab;
	public GameObject goalPrefab;
	public GameObject defaultWallPrefab;
	public GameObject testEnemy;
	[SerializeField]
	private int default_x_max = 13;
	[SerializeField]
	private int default_z_max = 10;

	public GamePlayerScript playerScript;
	public GameObject unitButtonParent;
	public GameObject unitButtonUI;

	//これがマップの元になるデータ(逆さまになってるから数値で見るときは注意)
	const string map_matrix =	"300000000000:" +
								"300000000000:" +
								"111111111100:" +
								"111111111100:" +
								"111111111100:" +
								"111111111100:" +
								"111111111100:" +
								"200000000000:" +
								"200000000000";

	// Use this for initialization
	void Start()
	{
		//枠を作るメソッド呼び出し
		CreateMapWaku();
		// 引数にこれを入れてマップ生成する
		CreateMap();
		GameSession.Instance.LoadUserData();
		CreateTeamUI();
	}

	//枠を作るメソッド
	void CreateMapWaku()
	{
		//ループしながらz軸の上と下２列に枠を作ります
		for (int dx = 0; dx <= default_x_max; dx++)
		{
			//Instantiate(defaultWallPrefab, new Vector3(dx, 0, 0), Quaternion.identity);
			//Instantiate(defaultWallPrefab, new Vector3(dx, 0, default_z_max), Quaternion.identity);
			StageAddChild(defaultWallPrefab, dx, 0);
			StageAddChild(defaultWallPrefab, dx, default_z_max);
		}

		//同じくx軸に右と左に枠を作ります
		for (int dz = 0; dz <= default_z_max; dz++)
		{
			//Instantiate(defaultWallPrefab, new Vector3(0, 0, dz), Quaternion.identity);
			//Instantiate(defaultWallPrefab, new Vector3(default_x_max, 0, dz), Quaternion.identity);
			StageAddChild(defaultWallPrefab, 0, dz);
			StageAddChild(defaultWallPrefab, default_x_max, dz);
		}

	}

	/// <summary>
	/// func: CreateMap
	/// Desc: マップ生成。map_matrixを元に生成。経路探索用に保存する際はmap_matrixもステージ情報に登録する
	/// </summary>
	void CreateMap()
	{
		//「:」をデリミタとして、map_matrix_arrに配列として分割していれます
		string[] map_matrix_arr = map_matrix.Split(':');

		//map_matrix_arrの配列の数を最大値としてループ
		for (int z = 0; z < map_matrix_arr.Length; z++)
		{
			//xを元に配列の要素を取り出す
			string z_map = map_matrix_arr[z];
			//１配列に格納されている文字の数でx軸をループ
			for (int x = 0; x < z_map.Length; x++)
			{
				//配列から取り出した１要素に値が入っているのでこれを１文字づつ取り出す
				int obj = int.Parse(z_map.Substring(x, 1));

				//もしも1だったら壁ということで壁のプレハブをインスタンス化してループして出したx座標z座標を指定して設置
				//0がなにもない空白
				if (obj == (int)StageData.SquareType.WALL)
				{
					//Instantiate(wallPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(wallPrefab, x+1, z+1);
				}
				else if (obj == (int)StageData.SquareType.GOAL)	//	敵が向かうゴール
				{
					//Instantiate(goalPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(goalPrefab, x+1, z+1);
				}
				else if (obj == (int)StageData.SquareType.SPAWN)	//	敵のスポーン地点
				{
					//Instantiate(spawnPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(spawnPrefab, x+1, z+1);
				}
			}
		}

	}
	/// <summary>
	/// func: StageAddChild
	/// Desc: 生成するprefabの親をparentPrefabにする。
	///			予めparentPrefabは用意する必要がある。
	/// </summary>
	/// <param name="obj">生成するブロック</param>
	/// <param name="x">生成時のX座標</param>
	/// <param name="z">生成時のZ座標</param>
	void StageAddChild(GameObject obj, float x, float z)
	{
		GameObject _prefab = Instantiate(obj, new Vector3(x, 0, z), Quaternion.identity);
		_prefab.transform.SetParent( parentPrefab.transform);
	}

	public void StageSave()
    {
		StageData stageData = parentPrefab.GetComponent<StageData>();
		if( stageData == null) stageData = parentPrefab.AddComponent<StageData>();  //	無かったら付ける
		stageData.setData(map_matrix);
	}

	public void TestEnemy()
    {
		StageData stageData = parentPrefab.GetComponent<StageData>();
		Vector3 spawnPos = stageData.GetSpawnPos(-1);
		if( spawnPos.x < 0)
        {
			Debug.Log("Nothing spawn square.");
			return;
        }
		GameObject obj = Instantiate(testEnemy, new Vector3(spawnPos.x, 0, spawnPos.z), Quaternion.identity);
		obj.GetComponent<EnemyScript>().goalPos = stageData.GetGoalPos(-1);

	}

	public void CreateTeamUI()
    {
		UserData ud = GameSession.Instance.userData;
		// ユーザーデータを元にUIプレハブを呼び出し入れる
		for( int i=0; i<ud.team.Length; i++)
        {
			if (ud.units[ud.team[i]].id <= 0) continue;	//	起きたら困る
			GameObject obj = Instantiate(unitButtonUI, unitButtonParent.transform.position + new Vector3(50*i,0,0), Quaternion.identity);
			obj.transform.parent = unitButtonParent.transform;
			obj.GetComponent<Button>().onClick.AddListener(()=> { playerScript.SetUnitMode(i); });
        }
    }
}