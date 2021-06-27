using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	主に当たり判定を処理
//	Tagで種類、Layerで誰かを判断する(Tag:Attack Layer:Playerといった形)
public class CharacterScript : MonoBehaviour
{
	private Animator charaanim;        //	アニメーター
	[SerializeField] public GameObject[] attackObj;	//	キャラによっては使わん
	/// <summary>
	/// 固定値として、0を通常攻撃とする
	/// </summary>
	private float m_nowInvincibleTime = 0.0f;  //	無敵時間
	[SerializeField] private float m_invincibleTime = 1.0f;	//	無敵時間
	// Start is called before the first frame update
	void Start()
    {
		charaanim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate() {
		m_nowInvincibleTime -= Time.deltaTime;
	}

	private bool CheckInvincible(){
		if (m_nowInvincibleTime > 0.0f) return true;
		return false;
	}

	public bool AttackHit(){
		return true;	//	当たったらtrue
	}

}
