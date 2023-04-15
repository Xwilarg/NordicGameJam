using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPath : MonoBehaviour
{
    public GameObject PathVisualPrefab;
    public Vector3 CurrentVelocity {get => PathMomentum;}

    private Transform PathVisual;
    private Vector3 PathMomentum;

    private const float G = 1f;

    void Start()
    {
        PathVisual = Instantiate(PathVisualPrefab, transform.position, Quaternion.identity).transform;
        PathVisual.SetParent(transform);
        PathMomentum = Vector3.right * 5;
    }

    void Update()
    {
        RenderPath(2);
    }

    private void RenderPath(float timeAhead)
    {
        int pointRange = 30;
        float delta = timeAhead/pointRange;

        PathVisual.position = transform.position;
        LineRenderer line = PathVisual.GetComponent<LineRenderer>();
        line.positionCount = pointRange;

        List<Attractor> attractors = new List<Attractor>();
        foreach(Attractor a in GameObject.FindObjectsOfType<Attractor>())
            if(a.Activated)
                attractors.Add(a);
        
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

        return (pos+(m*deltaT), m);
    }
}
