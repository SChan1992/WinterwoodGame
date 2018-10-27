using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    #region Private Properties

    [SerializeField]
    Transform mTarget;

    [SerializeField]
    NavMeshAgent mAgent;

    [SerializeField]
    float mTurnSpeed;

    #endregion

    //
    #region Unity CallBacks
    // Use this for initialization
    void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mTurnSpeed = 5.0f;
        //jackAnimator = GetComponent<Animator>();
        //jackRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (null != mTarget)
        {
            mAgent.SetDestination(mTarget.position);
            FaceTarget();
        }
    }

    #endregion

    //

    #region Public Methods

    public void MoveToPoint(Vector3 point)
    {
        mAgent.SetDestination(point);
        //if (jackRB.velocity.magnitude > 0.0f)
        //{
        //    jackAnimator.SetFloat("MovementSpeed", jackRB.velocity.magnitude);
        //}
        //else
        //{
        //    jackAnimator.SetFloat("MovementSpeed", 0.0f);
        //}
    }

    public void FollowTarget(Interactable newTarget)
    {
        mAgent.stoppingDistance = newTarget.radius * 0.8f;
        mAgent.updateRotation = false;
        mTarget = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        mAgent.stoppingDistance = 0.0f;
        mAgent.updateRotation = true;
        mTarget = null;
    }

    #endregion

    //

    #region Private Methods

    void FaceTarget()
    {
        Vector3 direction = (mTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * mTurnSpeed);
    }

    #endregion
}
