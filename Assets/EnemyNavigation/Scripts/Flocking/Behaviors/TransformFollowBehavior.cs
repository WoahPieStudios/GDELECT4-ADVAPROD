using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking.Behaviors
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Transform Follow")]
    public class TransformFollowBehavior:FlockBehavior
    {
        [SerializeField] private bool useTag;
        [SerializeField] private string tagToFollow;
        [SerializeField] private Transform transformToFollow;
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            // If using tags, set transform to follow to look for object with tag
            transformToFollow = useTag ? GameObject.FindGameObjectWithTag(tagToFollow)?.transform : transformToFollow;
            
            // If there's no transform to follow, return no adjustment
            if(transformToFollow == null) return Vector3.zero;

            var followMovement = Vector3.zero;

            var direction = transformToFollow.position - agent.transform.position;

            followMovement += direction;
            
            return followMovement;
        }
    }
}