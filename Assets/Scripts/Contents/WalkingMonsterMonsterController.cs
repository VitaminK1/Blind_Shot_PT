using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingMonsterMonsterController : BaseMonsterController
{
	[SerializeField] float _attackRange = 1;
	[SerializeField] NavMeshAgent _nma;

	protected override void UpdateMoving()
	{
		// 플레이어가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				SetDestination(transform.position);
				enemyState = Define.EnemyState.Attack;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.1f)
		{
			enemyState = Define.EnemyState.Idle;
		}
		else
		{
			SetDestination(_destPos);

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}

	protected override void UpdateAttack()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			dir.y = 0;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

    void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("Player")) {
			// Managers.Scene.LoadScene(Define.Scene.GameOverScene);
		}
    }

    private void SetDestination(Vector3 destPos) 
    {
	    _nma.SetDestination(destPos);
	}
}
