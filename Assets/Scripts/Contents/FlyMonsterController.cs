using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyMonsterController : BaseMonsterController
{
    [SerializeField] float _attackRange = 1;
    [SerializeField] private Rigidbody _rigidBody = null;
    [SerializeField] private float speed = 5;
    [SerializeField] private AudioSource _movingSound = null;
    private void Awake()
    {
        if (!_animator) { gameObject.GetComponent<Animator>(); }
        if (!_rigidBody) { gameObject.GetComponent<Rigidbody2D>(); }
        _movingSound = GetComponent<AudioSource>();
        _movingSound.Play();
    }

    protected override void UpdateMoving()
    	{
    		// 플레이어가 내 사정거리보다 가까우면 공격 상태로 전환
    		if (_lockTarget != null)
    		{
    			float distance = (_lockTarget.transform.position - transform.position).magnitude;
    			if (distance <= _attackRange)
    			{
	                ChangeEnemyState(Define.EnemyState.Attack);
    				return;
    			}
    		}
    
    		// 이동
    		Vector3 dir = _lockTarget.transform.position - transform.position;
            if (dir.magnitude < 0.1f)
    		{
    			enemyState = Define.EnemyState.Idle;
    		}
    		else
            {
	            Vector3 nextVec = speed * Time.fixedDeltaTime * dir.normalized ;
	            _rigidBody.MovePosition(_rigidBody.position + nextVec);
	            _rigidBody.velocity = Vector3.zero;
    			_animator.SetBool("walk", true);
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
    			_animator.SetBool("attack_01", true);
                Vector3 dir = _lockTarget.transform.position - transform.position;
    			dir.y = 0;
    			Quaternion quat = Quaternion.LookRotation(dir);
    			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
    		}
    	}
	
}
