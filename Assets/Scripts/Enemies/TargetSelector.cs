using System.Collections.Generic;
using System.Linq;
using Ky;
using UnityEngine;

namespace Enemies
{
    public class TargetSelector : MonoBehaviour
    {
        public Transform Target { get; private set; }

        public float Range = 10f;
        
        public bool HasTarget()
        {
            var bubbles =  GameplayManager.I.Bubbles;

            var inRangeTargets = new List<Transform>();
            foreach (var bubble in bubbles)
            {
                var delta = bubble.transform.position - transform.position;
                if (delta.sqrMagnitude <= Range * Range)
                {
                    inRangeTargets.Add(bubble.transform);
                }
                   
            }

            if (inRangeTargets.Count <= 0) return false;
            var postions = inRangeTargets.Select(x => x.position).ToArray();
           
            transform.position.FindClosest(postions, out int index);
            Target = inRangeTargets[index];
            return true;
        }

        public Transform GetTarget()
        {
            return Target;
        }
    }
}