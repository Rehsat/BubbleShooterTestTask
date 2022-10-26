using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileBubble : Bubble
{
    private Projectile _myProjectile;

    private void Awake()
    {
        _myProjectile = GetComponent<Projectile>();
    }
    public ProjectileBubble(BubbleColorType bubble) : base(bubble)
    {
       // MyBubbleColor = bubble;
    }
    public void StartMoving(Vector3 direction)
    {
        _myProjectile.StartMove(direction);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(BubblesLayer))
        {
            var collisionBubble = collision.gameObject.GetComponent<Bubble>();
            if (collisionBubble == null) return;

            if (collisionBubble.BubbleColor == BubbleColor)
            {
                collisionBubble.Burst();
            }
        }
    }
}
