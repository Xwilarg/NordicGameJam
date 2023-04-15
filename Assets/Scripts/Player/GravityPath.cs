using NordicGameJam.Player;
using System.Collections.Generic;
using NordicGameJam.SO;
using UnityEngine;

public class GravityPath : MonoBehaviour
{
    public GameObject PathVisualPrefab;
    public float PathSpeed;
    public BoxCollider2D Bounds;
    public Vector3 CurrentMomentum {get => PathMomentum;}

    [SerializeField]
    private PlayerInfo _info;

    private Transform PathVisual;
    public Vector3 PathMomentum { private set; get; }

    private PlayerController _pc;

    private const float G = 1f;

    private void Awake()
    {
        _pc = GetComponent<PlayerController>();
    }

    void Start()
    {
        PathVisual = Instantiate(PathVisualPrefab, transform.position, Quaternion.identity).transform;
        PathVisual.SetParent(transform);
        PathMomentum = Vector3.right;
        PathSpeed = 0;
    }

    void FixedUpdate()
    {
        RenderPath(_info.TimeAhead);
        if (_pc.DidMove)
        {
            (transform.position, PathMomentum) = PathDelta(transform.position, PathMomentum, Time.fixedDeltaTime * PathSpeed, GetAttractors());
        }
    }

    private List<Attractor> GetAttractors()
    {
        List<Attractor> attractors = new List<Attractor>();
            foreach(Attractor a in GameObject.FindObjectsOfType<Attractor>())
                if(a.Activated)
                    attractors.Add(a);
        return attractors;
    }

    public void SetVisualMomentum(Vector3 dir)
    {
        PathMomentum = dir;
    }

    private void RenderPath(float timeAhead)
    {
        int pointRange = _info.SimPoints;
        float delta = timeAhead/pointRange;

        PathVisual.position = transform.position;
        LineRenderer line = PathVisual.GetComponent<LineRenderer>();
        line.positionCount = pointRange;

        List<Attractor> attractors = GetAttractors();
        
        Vector3 momentum = PathMomentum;
        Vector3 position = transform.position;

        for(int i=0; i<pointRange; i++)
        {
            line.SetPosition(i, position);
            (position, momentum) = PathDelta(position, momentum, delta, attractors);
        }
    }

    private (Vector3, Vector3) PathDelta(Vector3 pos, Vector3 momentum, float deltaT, List<Attractor> attractors)
    {
        Vector3 m = momentum;
        foreach(Attractor att in attractors)
        {
            Vector3 dir = (att.transform.position - pos).normalized;
            m += dir * (1/(dir.magnitude*dir.magnitude)) * G * att.Strenght * deltaT;
        }

        Vector3 delta = m*deltaT;

        (bool hitBounds, Vector3 normal, float upto) = BoundsHit(pos, pos+(m*deltaT), Bounds.bounds);
        if(hitBounds)
        {
            m = Vector3.Reflect(m, normal);
            delta = delta.normalized * upto + m.normalized * (delta.magnitude-upto);
        }

        return (pos+delta, m);
    }

    private (bool, Vector3, float) BoundsHit(Vector3 before, Vector3 after, Bounds b)
    {
        Vector3 delta = after-before;

        if(after.x < b.min.x)
            return (true, Vector3.right, delta.magnitude * ((b.min.x-before.x) / (after.x-before.x)));
        if(after.y < b.min.y)
            return (true, Vector3.up, delta.magnitude * ((b.min.y-before.y) / (after.y-before.y)));
        if(after.x > b.max.x)
            return (true, Vector3.left, delta.magnitude * ((b.max.x-before.x) / (after.x-before.x)));
        if(after.y > b.max.y)
            return (true, Vector3.down, delta.magnitude * ((b.max.y-before.y) / (after.y-before.y)));
        return (false, Vector3.zero, 0);
    }
}
