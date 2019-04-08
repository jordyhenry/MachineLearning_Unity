using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBrain : MonoBehaviour
{
    private const string DEADZONE_TAG = "dead";
    private const string WALL_TAG = "wall";

    public float distanceFromStart = 0f;
    public float walkingTime = 0f;
    public DNA dna;
    public float botSpeed = 1f;
    public Transform eyes;

    private int dnaLength = 2;
    private bool alive = true;
    private bool seeingWall = true;
    private Vector3 startingPosition;
    private Material m_material;

    private void Start()
    {
        startingPosition = transform.position;
        m_material = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alive) return;

        if (collision.gameObject.CompareTag(DEADZONE_TAG))
        {
            alive = false;
            distanceFromStart = 0f;
        }
        
    }

    public void Init()
    {
        /*
         * 0 - forward
         * 1 - left
        */
        dna = new DNA(dnaLength, 360);
        alive = true;
    }

    private void Update()
    {
        if(!alive) return;

        Ray eyesRay = new Ray(eyes.position, eyes.forward * .1f);
        Debug.DrawRay(eyesRay.origin, eyesRay.direction, Color.red, .1f);

        seeingWall = false;
        RaycastHit hit;
        if(Physics.Raycast(eyesRay, out hit, .5f))
        {
            if (hit.collider.CompareTag(WALL_TAG))
                seeingWall = true;
        }

        float hMov = 0;
        float vMov = dna.GetGene(0);

        if (seeingWall)
            hMov = dna.GetGene(1);
        
        distanceFromStart = Vector3.Distance(transform.position, startingPosition);
        m_material.color = MazeDistanceGradient.Instance.GetColorByDistance(distanceFromStart);

        transform.Translate(0, 0, vMov * Time.deltaTime * botSpeed);
        transform.Rotate(0, hMov, 0);
    }
}
