using UnityEngine;

namespace Handlers
{
    public abstract class MaterialHandler : ScriptableObject
    {
        [SerializeField] protected Material material;
    }
}