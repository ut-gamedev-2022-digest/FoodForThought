using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Player Data Item")]
    public class PlayerCharacterDataItem : ScriptableObject
    {
        [Tooltip("Main character object that includes meshes, animations, etc.")]
        public GameObject characterPrefab;

        [Tooltip("Character controller's center relative location.")]
        public Vector3 characterControllerCenter = new(0, 1.5f, 0);
        
        [Tooltip("Camera's body rig shoulder offset. Depends on the characters' size.")]
        public Vector3 cameraBodyRigShoulderOffset = new(0, 3f, -11);

        [Tooltip("Character controller's radius.")]
        public float characterControllerRadius = 0.5f;

        [Tooltip("Character controller's height.")]
        public float characterControllerHeight = 2f;
    }
}