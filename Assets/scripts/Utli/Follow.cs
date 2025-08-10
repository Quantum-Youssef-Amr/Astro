using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] protected Transform Target;
    [SerializeField] protected Vector3 Offset;
    [SerializeField] protected float Speed;
    [SerializeField, Range(0, 1)] protected float Weight;
    protected Transform _t;

    protected void Start()
    {
        _t = transform;
    }

    void LateUpdate()
    {
        track();
    }

    public virtual void track()
    {
        if (Weight == 0)
        {
            _t.position = Target.position + Offset;
        }
        else
        {
            _t.position = Vector3.Lerp(_t.position, Target.position + Offset, Time.deltaTime * Mathf.Max(Weight, 0.1f) * Speed);
        }
    }

}
