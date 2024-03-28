using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    using PlayerIndex = TiltFive.PlayerIndex;

    /// <summary>
    /// Perform switching cameras on glasses availability.
    /// Inherited from GlassesDetectorBase().
    /// </summary>
    public class GlassesDetectorCameraSwitch : GlassesDetectorBase
    {
        /// <summary>
        /// The TiltFive Camera.
        /// </summary>
        [SerializeField]
        private GameObject _tiltFiveCamera;

        /// <summary>
        /// The backup camera for when the glasses are unavailable.
        /// </summary>
        [SerializeField]
        private GameObject _backupCamera;

        /// <summary>
        /// Template object for creating player objects.
        /// </summary>
        [SerializeField]
        private GameObject _playerObjectTemplate;

        [SerializeField]
        private Material _player1Material;

        [SerializeField]
        private Material _player2Material;

        [SerializeField]
        private Material _player3Material;

        [SerializeField]
        private Material _player4Material;

        private Dictionary<PlayerIndex, HashSet<GameObject>> _playerObjects =
        new Dictionary<PlayerIndex, HashSet<GameObject>>()
        {
            [PlayerIndex.One] = new HashSet<GameObject>(),
            [PlayerIndex.Two] = new HashSet<GameObject>(),
            [PlayerIndex.Three] = new HashSet<GameObject>(),
            [PlayerIndex.Four] = new HashSet<GameObject>(),
        };

        /// <summary>
        /// Switches to glasses camera when glasses are available.
        /// </summary>
        protected override void DoGlassesAvailable(bool pForce = false)
        {
            _tiltFiveCamera.SetActive(true);
            _backupCamera.SetActive(false);

            base.DoGlassesAvailable(pForce);
        }

        /// <summary>
        /// Switches to backup camera when glasses are unavailable.
        /// </summary>
        protected override void DoGlassesUnavailable()
        {
            _tiltFiveCamera.SetActive(false);
            _backupCamera.SetActive(true);

            base.DoGlassesUnavailable();
        }

        /// <summary>
        /// Spawns a player object for the specified index if a template object was provided.
        /// </summary>
        private void MaybeSpawnPlayerObject(PlayerIndex playerIndex)
        {
            if (!_playerObjectTemplate)
            {
                return; // no template object
            }

            // Scale and position here are specific to the example scene.
            var localScale = new Vector3(10, 20, 10);
            float positionX;
            Material material;
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    positionX = -5.7f;
                    material = _player1Material;
                    break;
                case PlayerIndex.Two:
                    positionX = -2.1f;
                    material = _player2Material;
                    break;
                case PlayerIndex.Three:
                    positionX = 2.5f;
                    material = _player3Material;
                    break;
                case PlayerIndex.Four:
                    positionX = 5.9f;
                    material = _player4Material;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException(
                        nameof(playerIndex), $"Unexpected player index: {playerIndex}");
            };
            var position = new Vector3(positionX, 0, -7);

            GameObject playerObject = Instantiate(
                _playerObjectTemplate, position, Quaternion.identity);

            playerObject.name = $"{_playerObjectTemplate.name} (Player {(int)playerIndex})";
            playerObject.transform.localScale = localScale;

            // If a material is provided, set the material for all of the new object's children.
            if (material)
            {
                foreach (var r in playerObject.transform.GetComponentsInChildren<Renderer>())
                {
                    var mats = new Material[r.materials.Length];
                    for (var i = 0; i < mats.Length; i++)
                    {
                        mats[i] = material;
                    }
                    r.materials = mats;
                }
            }

            _playerObjects[playerIndex].Add(playerObject);
        }

        /// <summary>
        /// Adds player objects when players become available.
        /// </summary>
        protected override void DoPlayerAvailable(PlayerIndex playerIndex, bool pForce = false)
        {
            base.DoPlayerAvailable(playerIndex, pForce);

            MaybeSpawnPlayerObject(playerIndex);
        }

        /// <summary>
        /// Removes player objects when players become unavailable.
        /// </summary>
        protected override void DoPlayerUnavailable(PlayerIndex playerIndex)
        {
            base.DoPlayerUnavailable(playerIndex);

            foreach (GameObject gameObject in _playerObjects[playerIndex])
            {
                Destroy(gameObject);
            }
            _playerObjects[playerIndex].Clear();
        }
    }
}
