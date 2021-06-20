using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// �Q�l:https://raharu0425.hatenablog.com/entry/20130131/1359601502
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

	//���ꂪ�}�b�v�̌��ɂȂ�f�[�^(�t���܂ɂȂ��Ă邩�琔�l�Ō���Ƃ��͒���)
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
		//�g����郁�\�b�h�Ăяo��
		CreateMapWaku();
		// �����ɂ�������ă}�b�v��������
		CreateMap();
		GameSession.Instance.LoadUserData();
		CreateTeamUI();
	}

	//�g����郁�\�b�h
	void CreateMapWaku()
	{
		//���[�v���Ȃ���z���̏�Ɖ��Q��ɘg�����܂�
		for (int dx = 0; dx <= default_x_max; dx++)
		{
			//Instantiate(defaultWallPrefab, new Vector3(dx, 0, 0), Quaternion.identity);
			//Instantiate(defaultWallPrefab, new Vector3(dx, 0, default_z_max), Quaternion.identity);
			StageAddChild(defaultWallPrefab, dx, 0);
			StageAddChild(defaultWallPrefab, dx, default_z_max);
		}

		//������x���ɉE�ƍ��ɘg�����܂�
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
	/// Desc: �}�b�v�����Bmap_matrix�����ɐ����B�o�H�T���p�ɕۑ�����ۂ�map_matrix���X�e�[�W���ɓo�^����
	/// </summary>
	void CreateMap()
	{
		//�u:�v���f���~�^�Ƃ��āAmap_matrix_arr�ɔz��Ƃ��ĕ������Ă���܂�
		string[] map_matrix_arr = map_matrix.Split(':');

		//map_matrix_arr�̔z��̐����ő�l�Ƃ��ă��[�v
		for (int z = 0; z < map_matrix_arr.Length; z++)
		{
			//x�����ɔz��̗v�f�����o��
			string z_map = map_matrix_arr[z];
			//�P�z��Ɋi�[����Ă��镶���̐���x�������[�v
			for (int x = 0; x < z_map.Length; x++)
			{
				//�z�񂩂���o�����P�v�f�ɒl�������Ă���̂ł�����P�����Â��o��
				int obj = int.Parse(z_map.Substring(x, 1));

				//������1��������ǂƂ������Ƃŕǂ̃v���n�u���C���X�^���X�����ă��[�v���ďo����x���Wz���W���w�肵�Đݒu
				//0���Ȃɂ��Ȃ���
				if (obj == (int)StageData.SquareType.WALL)
				{
					//Instantiate(wallPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(wallPrefab, x+1, z+1);
				}
				else if (obj == (int)StageData.SquareType.GOAL)	//	�G���������S�[��
				{
					//Instantiate(goalPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(goalPrefab, x+1, z+1);
				}
				else if (obj == (int)StageData.SquareType.SPAWN)	//	�G�̃X�|�[���n�_
				{
					//Instantiate(spawnPrefab, new Vector3(x + 1, 0, z + 1), Quaternion.identity);
					StageAddChild(spawnPrefab, x+1, z+1);
				}
			}
		}

	}
	/// <summary>
	/// func: StageAddChild
	/// Desc: ��������prefab�̐e��parentPrefab�ɂ���B
	///			�\��parentPrefab�͗p�ӂ���K�v������B
	/// </summary>
	/// <param name="obj">��������u���b�N</param>
	/// <param name="x">��������X���W</param>
	/// <param name="z">��������Z���W</param>
	void StageAddChild(GameObject obj, float x, float z)
	{
		GameObject _prefab = Instantiate(obj, new Vector3(x, 0, z), Quaternion.identity);
		_prefab.transform.SetParent( parentPrefab.transform);
	}

	public void StageSave()
    {
		StageData stageData = parentPrefab.GetComponent<StageData>();
		if( stageData == null) stageData = parentPrefab.AddComponent<StageData>();  //	����������t����
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
		// ���[�U�[�f�[�^������UI�v���n�u���Ăяo�������
		for( int i=0; i<ud.team.Length; i++)
        {
			if (ud.units[ud.team[i]].id <= 0) continue;	//	�N�����獢��
			GameObject obj = Instantiate(unitButtonUI, unitButtonParent.transform.position + new Vector3(50*i,0,0), Quaternion.identity);
			obj.transform.parent = unitButtonParent.transform;
			obj.GetComponent<Button>().onClick.AddListener(()=> { playerScript.SetUnitMode(i); });
        }
    }
}