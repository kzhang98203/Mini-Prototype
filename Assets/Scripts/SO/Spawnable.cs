using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Spawnable", fileName = "Spawnable")]
    public class Spawnable : ScriptableObject
    {
        public float minBaseSpeed;
        public float maxBaseSpeed;
    }
}
