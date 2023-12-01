using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonsterController : MonoBehaviour
{
	[SerializeField] protected Animator _animator;
	[SerializeField] protected Vector3 _destPos;
	[SerializeField] protected Define.EnemyState _enemyState = Define.EnemyState.None;
	
	protected GameObject _lockTarget => GameManager.Instance.Player;
	protected readonly int _movingId = Animator.StringToHash("Moving");
	protected readonly int _attackId = Animator.StringToHash("Attack");
	protected readonly int _dieId = Animator.StringToHash("Die");

	public Define.EnemyState enemyState
	{
		get { return _enemyState; }
		set
		{
			if (_enemyState == value) return;
			
			_enemyState = value;
			OnEnemyStateChanged(_enemyState);
		}
	}

	void Update()
	{
		switch (enemyState)
		{
			case Define.EnemyState.None:
				ChangeEnemyState(Define.EnemyState.Moving);
				break;
			case Define.EnemyState.Die:
				UpdateDie();
				break;
			case Define.EnemyState.Moving:
				UpdateMoving();
				break;
			case Define.EnemyState.Idle:
				UpdateIdle();
				break;
			case Define.EnemyState.Attack:
				UpdateAttack();
				break;
		}
	}

	protected void ChangeEnemyState(Define.EnemyState state)
	{
		enemyState = state;
	}

	protected virtual void UpdateDie() { }
	protected virtual void UpdateMoving() { }
	protected virtual void UpdateIdle() { }
	protected virtual void UpdateAttack() { }

	protected virtual void OnEnemyStateChanged(Define.EnemyState newState)
	{
		switch (newState)
		{
			case Define.EnemyState.Die:
				_animator.SetBool(_dieId, true);
				break;
			case Define.EnemyState.Moving:
				_animator.SetBool(_movingId, true);
				break;
			case Define.EnemyState.Attack:
				_animator.SetBool(_attackId, true);
				break;
		}
	}
}

