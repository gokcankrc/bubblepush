using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class BonesSpringWebMaker : MonoBehaviour
{
    // not the kind of joint you think
    private GameObject[] joints;
    [SerializeField] private float radius = 0.8f;
    [SerializeField] private float rbMass = 1f;
    [SerializeField] private PhysicsMaterial2D rbPhysMat;
    [SerializeField] private SpringSettings neighborSpringSettings;
    [SerializeField] private SpringSettings oppositeSpringSettings;

    [Button]
    private void AddRigidbodies()
    {
        joints = transform.Cast<Transform>().Select(x => x.gameObject).ToArray();
        int count = joints.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject current = joints[i];
            if (!current.TryGetComponent(out Rigidbody2D rb))
                rb = current.AddComponent<Rigidbody2D>();
            rb.mass = rbMass;
            rb.sharedMaterial = rbPhysMat;
            rb.gravityScale = 0;
        }
    }

    [Button]
    private void AddCircleColliders()
    {
        joints = transform.Cast<Transform>().Select(x => x.gameObject).ToArray();
        int count = joints.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject current = joints[i];
            if (!current.TryGetComponent(out CircleCollider2D cc2d))
                cc2d = current.AddComponent<CircleCollider2D>();
        }
    }

    [Button]
    private void ConnectSprings()
    {
        joints = transform.Cast<Transform>().Select(x => x.gameObject).ToArray();
        int count = joints.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject current = joints[i];
            SpringJoint2D[] oldJoints = current.GetComponents<SpringJoint2D>();
            smokeOldJoints(oldJoints);
            AddSpringJoint(current, joints[(i + 1) % count], neighborSpringSettings);
            AddSpringJoint(current, joints[(i - 1 + count) % count], neighborSpringSettings);
            AddSpringJoint(current, joints[(i + count / 2) % count], oppositeSpringSettings);
        }

        void smokeOldJoints(SpringJoint2D[] oldJoints)
        {
            foreach (var joint in oldJoints)
            {
                DestroyImmediate(joint);
            }
        }
    }

    [Button]
    private void AdjustBones()
    {
        joints = transform.Cast<Transform>().Select(x => x.gameObject).ToArray();
        int count = joints.Length;

        for (int i = 0; i < count; i++)
        {
            Transform current = joints[i].transform;
            var pos = Vector3.right * radius;
            var angle = 360f / count * i;
            pos = Quaternion.AngleAxis(angle, Vector3.back) * pos;
            current.localPosition = pos;
            var rot = current.rotation;
            rot.eulerAngles = new Vector3(0, 0,- angle + 180f);
            current.rotation = rot;
        }
    }

    private void AddSpringJoint(GameObject from, GameObject to, SpringSettings settings)
    {
        SpringJoint2D spring = from.AddComponent<SpringJoint2D>();
        spring.connectedBody = to.GetComponent<Rigidbody2D>();
        spring.autoConfigureDistance = false;
        spring.distance = settings.dist;
        spring.dampingRatio = settings.damp;
        spring.frequency = settings.freq;
        spring.breakForce = settings.breakForce;
        spring.autoConfigureConnectedAnchor = true;
    }

    [Serializable]
    private class SpringSettings
    {
        public float dist;
        public float damp;
        public float freq;
        public float breakForce;
    }
}