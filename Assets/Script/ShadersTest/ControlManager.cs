using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlManager : MonoBehaviour
{
	#region キャラ操作パラメータ
	//	必須メンバ(予め登録しておく)
	[SerializeField] private GameObject playerChara;    //	プレイヤーキャラオブジェクト
	[SerializeField] private GameObject playerCamera;   //	プレイヤーカメラ
	[SerializeField] private Animator charaanim;		//	アニメーター
    private bool m_isPlayerCharaActive;   //  キャラ操作可能か

	//	キャラ状態系メンバ
	private bool m_isJump = false;			//	ジャンプ中か
	private bool m_nonMoveAtk = false;      //	攻撃中か(移動させないフラグなので移動しながら使える技は該当しない)

	//	操作保持メンバ(移動量以外はフラグ系に回すかも)
	private float m_horizontal = 0.0f;  //	右左方向の移動量
	private float m_vertical = 0.0f;    //	前後方向の移動量
	private bool m_normalAtk = false;	//	通常攻撃を入力したか

	//	移動量等の数値(可変)
	[SerializeField] private float m_moveSpd = 1.0f; //	移動速度
	[SerializeField] private float moveForceMultiplier = 1.0f; //	追従度

	#endregion

	// Start is called before the first frame update
	void Start() {
		if (playerChara == null) GameObject.FindGameObjectWithTag("Player");
		if (charaanim == null) charaanim = playerChara.GetComponent<Animator>();
		m_isPlayerCharaActive = true; //  テスト段階なのでとりあえずtrue

		if (playerCamera == null) playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	//	Updateでフレーム管理する場合はunscaledDeltaTimeを使用する(timeScaleの影響を受けないので)
	private void Update() {
        if(m_isPlayerCharaActive) {   //  キャラ操作可能であればキャラクターの操作処理を行う
			m_horizontal = Input.GetAxis("Horizontal");	//	横移動
			m_vertical = Input.GetAxis("Vertical");		//	縦移動
			var camerascp = playerCamera.GetComponent<CameraManager>();
			if (camerascp) camerascp.CameraUpdate();    //	カメラスクリプトのアップデートを実行
			m_normalAtk = Input.GetButtonDown("Fire3");	//	通常攻撃

		}
    }

	//  フレーム管理はしろ(戒め)
	//	移動処理はこっちでする
	private void FixedUpdate() {
		Vector3 _moveVec = Vector3.zero;
		Rigidbody _rb = playerChara.GetComponent<Rigidbody>();
		Vector3 _cameraForward = playerCamera.transform.forward;
		Vector3 _cameraRight = playerCamera.transform.right;
		_cameraForward.y = 0.0f;    // 水平方向に移動させたい場合はy方向成分を0にする
		_cameraRight.y = 0.0f;

		//移動不可攻撃は移動量リセットする
		if ( m_nonMoveAtk) m_horizontal = m_vertical = 0.0f;

		//	移動処理
		if (m_horizontal != 0.0f || m_vertical != 0.0f) {
			_moveVec = (m_moveSpd * Time.deltaTime) * (_cameraRight.normalized * m_horizontal + _cameraForward.normalized * m_vertical);

			var direction = (transform.position + _moveVec) - transform.position;
			direction.y = 0;
			var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
			playerChara.transform.rotation = Quaternion.Lerp(playerChara.transform.rotation, lookRotation, 0.2f);
		}
		//	モーション更新
//		charaanim.SetBool("Attack", m_nonMoveAtk);	//	現状攻撃モーションするのは移動不可ぐらい？
		charaanim.SetFloat("Speed", Mathf.Abs(m_horizontal) + Mathf.Abs(m_vertical));	//	移動モーション更新
		
		_rb.AddForce(moveForceMultiplier * (_moveVec - _rb.velocity));   //	キャラ移動
		
	}

}
public class WaitForAnimation : CustomYieldInstruction
{
	Animator m_animator;
	int m_lastStateHash = 0;
	int m_layerNo = 0;

    [Obsolete]
    public WaitForAnimation(Animator animator, int layerNo) {
		Init(animator, layerNo, animator.GetCurrentAnimatorStateInfo(layerNo).nameHash);
	}

	void Init(Animator animator, int layerNo, int hash) {
		m_layerNo = layerNo;
		m_animator = animator;
		m_lastStateHash = hash;
	}

	public override bool keepWaiting
	{
		get {
			var currentAnimatorState = m_animator.GetCurrentAnimatorStateInfo(m_layerNo);
			return currentAnimatorState.fullPathHash == m_lastStateHash &&
				(currentAnimatorState.normalizedTime < 1);
		}
	}
}