using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    // Update is called once per frame
    public void DeactivateRagdoll()
    {
        foreach (var rigidbody1 in _rigidbodies)
        {
            rigidbody1.isKinematic = true;
        }

        _animator.enabled = true;
    }
    
    public void ActivateRagdoll()
    {
        foreach (var rigidbody1 in _rigidbodies)
        {
            rigidbody1.isKinematic = false;
        }

        _animator.enabled = false;
    }

    public void ApplForce(Vector3 force)
    {
        var rigidBody = _animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
