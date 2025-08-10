using UnityEngine;

public class Followphysics : Follow
{
    [Space(8),SerializeField, Tooltip("Offset of the linear velocity in local space")] private Vector2 PhysicsOffset;
    [SerializeField] protected Rigidbody2D TargetRigidbody;

    public override void track()
    {
        if (Weight == 0)
        {
            _t.position = Target.position + Offset;
        }
        else
        {
            _t.position = Vector3.Lerp(_t.position,
            Target.position + Offset + (Vector3) (TargetRigidbody.linearVelocity.normalized + (Vector2) (Target.up * PhysicsOffset.y + Target.right * PhysicsOffset.x)),
            Time.deltaTime * Mathf.Max(Weight, 0.1f) * Speed);
        }
    }
}
