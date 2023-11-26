using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{

	[SerializeField]
	float _attackRange = 1;

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Enemy;
	}

	protected override void UpdateIdle()
	{
		GameObject player = Managers.Game.Player;
		if (player == null)
			return;
		_lockTarget = player;
		State = Define.State.Moving;
		return;
	}

	protected override void UpdateMoving()
	{
		// 플레이어가 내 사정거리보다 가까우면 공격
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
				SetDestination(nma, transform.position);
				State = Define.State.Attack;
				return;
			}
		}

		// 이동
		Vector3 dir = _destPos - transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
			SetDestination(nma, _destPos);

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
			Managers.Scene.LoadScene(Define.Scene.GameOverScene);
		}
    }

    private void SetDestination(NavMeshAgent nma, Vector3 destPos) {
		nma.SetDestination(destPos);
	}
}
