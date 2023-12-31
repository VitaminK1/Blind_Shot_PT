using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingMonsterController : BaseMonsterController
{
	[SerializeField] float _attackRange = 0.1f;
	[SerializeField] NavMeshAgent _nma;
	[SerializeField] private Rigidbody _rigidBody = null;

	private void Awake()
	{
		if (!_animator) { gameObject.GetComponent<Animator>(); }
		if (!_rigidBody) { gameObject.GetComponent<Animator>(); }
		_animator.SetBool("Reset", true);
	}


	protected override void Init()
	{
		base.Init();
		SetDestination(_lockTarget.transform.position);
	}
	
	protected override void UpdateMoving()
	{
		// 플레이어가 내 사정거리보다 가까우면 공격 상태로 전환
		if (_lockTarget != null)
		{
			float distance = (_lockTarget.transform.position - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				SetDestination(transform.position);
				ChangeEnemyState(Define.EnemyState.Attack);
				return;
			}
		}

		SetDestination(_lockTarget.transform.position);
		// 이동
		Vector3 dir = _lockTarget.transform.position - transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.1f)
		{
			enemyState = Define.EnemyState.Idle;
		}
		else
		{
			_animator.SetFloat("MoveSpeed", dir.magnitude);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
			
		}
	}

	protected override void UpdateDie()
	{
		Despawn();
    }

	protected override void UpdateAttack()
	{
		if (_lockTarget != null)
		{
			_animator.SetBool("Attack", true);
			Vector3 dir = _lockTarget.transform.position - transform.position;
			dir.y = 0;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

    private void SetDestination(Vector3 destPos) 
    {
	    _nma.SetDestination(destPos);
	}

}
