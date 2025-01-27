using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Basic2DPlatformer.Player
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private float cameraFollowSpeed = 5f;

        private Vector3 bottomLeftTile, topRightTile;
        private Vector3 bottomLeftCamera, topRightCamera;
        private Camera mainCamera;
        private float cameraOrthographicSize, cameraVerticalSize;
        private float minXCameraPosition, minYCameraPosition, maxXCameraPosition, maxYCameraPosition;

        private void Start()
        {
            UpdateTilemapBounds();
            InitializeCamera();
        }

        private void LateUpdate()
        {
            FollowToPlayer();
        }

        private void UpdateTilemapBounds()
        {
            tilemap.CompressBounds(); // force the Tilemap to recalculate its bounds
            bottomLeftTile = tilemap.localBounds.min;
            topRightTile = tilemap.localBounds.max;
        }

        private void InitializeCamera()
        {
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                cameraOrthographicSize = mainCamera.orthographicSize * Screen.width / Screen.height;
                cameraVerticalSize = mainCamera.orthographicSize;
            }

            minXCameraPosition = bottomLeftTile.x + cameraOrthographicSize;
            maxXCameraPosition = topRightTile.x - cameraOrthographicSize;
            minYCameraPosition = bottomLeftTile.y + cameraVerticalSize;
            maxYCameraPosition = topRightTile.y - cameraVerticalSize;
        }

        private void FollowToPlayer()
        {
            var targetPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            var targetPositionX = Mathf.Clamp(targetPosition.x, minXCameraPosition, maxXCameraPosition);
            var targetPositionY = Mathf.Clamp(targetPosition.y, minYCameraPosition, maxYCameraPosition);
            var newTargetPosition = new Vector3(targetPositionX, targetPositionY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newTargetPosition, Time.deltaTime * cameraFollowSpeed);
        }
    }
}