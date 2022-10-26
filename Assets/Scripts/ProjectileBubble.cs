using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileBubble : Bubble
{
    private Projectile _myProjectile;

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
        if (collision.gameObject.IsInLayer(BubblesLayer))
        {
            var collisionBubble = collision.gameObject.GetComponent<Bubble>();
            if (collisionBubble == null) return;

            if (collisionBubble.BubbleColor == BubbleColor) collisionBubble.Burst();
            else ConnectWithBubbles(collisionBubble);

            Destroy(gameObject);
        }
    }
    private void ConnectWithBubbles(Bubble collisionBubble)
    {
        FindNeighboursWithColor(BubbleColorType.Empty);

        var closestEmptyBubble = FindClosestBubble();

        closestEmptyBubble.BubbleColor = BubbleColor;
    /*    closestEmptyBubble.FindNeighboursWithColor(BubbleColor);
        foreach(var neighbour in closestEmptyBubble.Neighbours)
        collisionBubble.Neighbours.Add(closestEmptyBubble);
    */
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
