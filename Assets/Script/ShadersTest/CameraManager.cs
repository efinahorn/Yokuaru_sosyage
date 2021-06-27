using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	public Transform target;
	public float smoothing = 5f;
	public float distance = 3.0f;
	public float distance_max = 3.0f;
	public float distance_min = 1.0f;
	public float horizontalspd = 3.0f;
	public float verticalspd = 3.0f;
	public Vector3 correction;
	private Vector3 offset;
	private Vector2 _cameramove = Vector2.zero;	//	操作量
	[SerializeField] private Vector2 _nowmove = Vector2.zero;	//	今の移動量
	
	void Start() {
		if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
		offset = target.position + new Vector3( 0, correction.y, distance);
	}

	//	更新はコントロールマネージャーでする
	public void CameraUpdate() {
		if (Input.GetMouseButton(2))
		{
			_cameramove.x = -Input.GetAxis("Mouse X");
			_cameramove.y = Input.GetAxis("Mouse Y");
		}
        else
        {
			_cameramove = Vector2.zero;
		}
		distance -= Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance, distance_min, distance_max);
	}

	void FixedUpdate() {
		Vector3 pos = Vector3.zero;
		_nowmove += new Vector2(_cameramove.x * horizontalspd, _cameramove.y * verticalspd);
		//	左右回転オーバーフロー対策(そもそも数値残し方が悪い節はある)
		if (_nowmove.x > 2.0f) _nowmove.x -= 2.0f;
		if (_nowmove.x < -2.0f) _nowmove.x += 2.0f;
		//	上下回転限度処理
		_nowmove.y = Mathf.Clamp(_nowmove.y, 0.3f, 0.95f);
		
		// 球面座標系変換
		pos.x = distance * Mathf.Sin(_nowmove.y * Mathf.PI) * Mathf.Cos(_nowmove.x * Mathf.PI);
		pos.y = -distance * Mathf.Cos(_nowmove.y * Mathf.PI);
		pos.z = -distance * Mathf.Sin(_nowmove.y * Mathf.PI) * Mathf.Sin(_nowmove.x * Mathf.PI);

		pos *= offset.z;

		pos.y += offset.y;
		
		// 座標の更新
		Vector3 targetCamPos = pos + target.position;  //	カメラの移動先
		Vector3 rayPos = target.position + correction; //	誤差分レイキャストの始点をズラす。じゃないとパンツ見える
		RaycastHit wallhit;
		//	カメラがオブジェクトを貫通していたらオブジェクト前まで移動先をそのオブジェクト前にする
		if ( Physics.Raycast(rayPos, targetCamPos - rayPos, out wallhit,
			Vector3.Distance(rayPos, targetCamPos),LayerMask.GetMask("Ground"),QueryTriggerInteraction.Ignore))
		{
			targetCamPos = Vector3.Lerp(rayPos, wallhit.point, 0.9f);	//	ちょっと前に出す(若干貫通する角度がある)
		}
		transform.position = Vector3.Lerp(
			transform.position,
			targetCamPos,
			Time.deltaTime * smoothing
		);
		transform.LookAt(target.position + correction);
		
///		rotf += _camerahorizontal * horizontalspd * Time.deltaTime;
///		transform.LookAt( target.position + correction);
///		offset = new Vector3(distance * Mathf.Sin(rotf * Mathf.PI), baseoffset.y, distance * Mathf.Cos(rotf * Mathf.PI));
	}
}
