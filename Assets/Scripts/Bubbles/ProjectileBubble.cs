using UnityEngine;
using System;
namespace Bubbles
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileBubble : Bubble
    {
        private Projectile _myProjectile;
        private bool _isCollided;
        private PauseController _injectedPauseController;
        public PauseController InjectedPauseController
        {
            get => _injectedPauseController;
            set
            {
                _injectedPauseController = value;
                _myProjectile.InjectedPauseController = InjectedPauseController;
            }
        }

        public Action OnCollisionWithBubble;
        private void Awake()
        {
            _myProjectile = GetComponent<Projectile>();
        }
        public void StartMoving(Vector3 direction)
        {
            _myProjectile.StartMove(direction);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(BubblesLayer) && _isCollided == false)
            {
                HandleCollision(collision);
            }
        }
        private void HandleCollision(Collision2D collision)
        {
            var collisionBubble = collision.gameObject.GetComponent<Bubble>();
            if (collisionBubble == null)
            {
                return;
            }

            _isCollided = true;
            OnCollisionWithBubble?.Invoke();

            if (collisionBubble.BubbleColor == BubbleColor)
            {
                collisionBubble.Burst();
            }
            else
            {
                ConnectWithBubbles(collisionBubble);
            }

            Destroy(gameObject);
        }
        private void ConnectWithBubbles(Bubble collisionBubble)
        {
            DetectNeighbours();
            var emptyBubbles = GetNeighborsWithColor(BubbleColorType.Empty);
            Neighbours.Clear();
            foreach (var emptyBubble in emptyBubbles)
            {
                Neighbours.Add(emptyBubble);
            }

            var closestEmptyBubble = FindClosestBubble();

            closestEmptyBubble.BubbleColor = BubbleColor;
            var neighboursWithSameColor = closestEmptyBubble.GetNeighborsWithColor(BubbleColor);
            if (neighboursWithSameColor.Length > 0) closestEmptyBubble.Burst();
        }
        private Bubble FindClosestBubble()
        {
            var closestEmptyBubble = Neighbours[0];
            var distanceToClosestTarget = Vector2.Distance(transform.position, closestEmptyBubble.transform.position);

            foreach (var neighbour in Neighbours)
            {
                var distanceToEmptyBubble = Vector2.Distance(transform.position, neighbour.transform.position);
                if (distanceToEmptyBubble < distanceToClosestTarget)
                {
                    closestEmptyBubble = neighbour;
                    distanceToClosestTarget = distanceToEmptyBubble;
                }
            }
            return closestEmptyBubble;
        }
    }
}