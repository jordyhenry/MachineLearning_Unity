using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDistanceGradient : MonoBehaviour
{
    public Gradient distanceGradient;
    public Transform startingPoint;
    public Transform mostFarPoint;

    private float distanceThreshold = 0f;
    public static MazeDistanceGradient Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        distanceThreshold = Vector3.Distance(startingPoint.position, mostFarPoint.position);
    }

    public Color GetColorByDistance(float _dist)
    {
        float percent = _dist / distanceThreshold;
        return distanceGradient.Evaluate(percent);
    }
}
