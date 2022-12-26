using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The camera will follow that target on the character.")]
        public GameObject lookAtTargetPrefab;

        [Tooltip("The Cinemachine Virtual Camera that will follow the character.")]
        public CinemachineVirtualCamera virtualCamera;

        [Tooltip("All available character data items.")]
        public List<PlayerCharacterDataItem> dataItems;

        private const string PlayerTag = "Player";
        private const string LayerName = "Character";

        private int _selectedCharacterIndex;
        private PlayerCharacterDataItem _dataItem;
        private GameObject _characterInstance;
        private GameObject _lookAtTargetInstance;
        private CharacterController _characterController;
        private ThirdPersonController _thirdPersonController;

        private void Awake()
        {
            _selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
            _dataItem = dataItems[_selectedCharacterIndex];
            dataItems[_selectedCharacterIndex]?.characterPrefab.SetActive(true);

            _thirdPersonController = GetComponent<ThirdPersonController>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            if (_dataItem == null)
            {
                Debug.LogError("Player's data item must be provided.");
                return;
            }

            SetUpCharacterController();

            SetUpCharacter();

            SetUpCamera();

            SetUpThirdPersonController();
        }


        private void SetUpThirdPersonController()
        {
            if (_thirdPersonController == null)
            {
                Debug.LogError("ThirdPersonController must be attached to the player.");
                return;
            }

            if (_lookAtTargetInstance == null)
            {
                Debug.LogError("LookAtTargetPrefab must be provided.");
                return;
            }

            _thirdPersonController.CinemachineCameraTarget = _lookAtTargetInstance;
        }

        private void SetUpCharacter()
        {
            if (_characterController == null)
            {
                Debug.LogError("CharacterController must be defined before the character is instantiated.");
                return;
            }

            _characterInstance =
                Instantiate(
                    _dataItem.characterPrefab,
                    transform.position,
                    transform.rotation,
                    transform);

            _characterInstance.tag = PlayerTag;
            _characterInstance.layer = LayerMask.NameToLayer(LayerName);
        }

        private void SetUpCharacterController()
        {
            if (_characterController == null)
            {
                Debug.LogError("CharacterController is missing on the PlayerCharacter GameObject.");
                return;
            }

            _characterController.center = _dataItem.characterControllerCenter;
            _characterController.radius = _dataItem.characterControllerRadius;
            _characterController.height = _dataItem.characterControllerHeight;
        }

        private void SetUpCamera()
        {
            if (virtualCamera == null)
            {
                Debug.LogError("No virtual camera found.");
                return;
            }

            // Set camera rig shoulder offset to -11
            var bodyRigShoulderOffset = new Vector3(0, 0, -11);
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = bodyRigShoulderOffset;

            // Set LookAt target
            _lookAtTargetInstance = Instantiate(
                lookAtTargetPrefab,
                transform.position,
                transform.rotation,
                _characterInstance.transform);

            _lookAtTargetInstance.tag = PlayerTag;
            _lookAtTargetInstance.layer = LayerMask.NameToLayer(LayerName);

            virtualCamera.Follow = _lookAtTargetInstance.transform;
            virtualCamera.LookAt = _lookAtTargetInstance.transform;

            // Set lookAtTarget y position to the half of the characterController height.
            _lookAtTargetInstance.transform.localPosition =
                new Vector3(0, _dataItem.characterControllerHeight / 1.5f, 0);
        }
    }
}