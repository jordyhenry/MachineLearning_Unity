using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatformBrain : MonoBehaviour
{
    private const string DEADZONE_TAG = "dead";
    private const string PLATFORM_TAG = "platform";

    public float timeAlive = 0f;
    public float timeWalking = 0f;
    public DNA dna;
    public float botSpeed = 1f;
    public Transform eyes;

    private int dnaLength = 2;
    private bool alive = true;
    private bool seeingGround = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!alive) return;

        if (collision.gameObject.CompareTag(DEADZONE_TAG))
        {
            alive = false;
        }
    }

    public void Init()
    {
        /*
         * 0 - forward
         * 1 - left
         * 3 - right
        */
        dna = new DNA(dnaLength, 3);
        timeAlive = 0;
        alive = true;
    }

    private void Update()
    {
        if (!alive) return;

        Ray eyesRay = new Ray(eyes.position, eyes.forward * 10);
        Debug.DrawRay(eyesRay.origin, eyesRay.direction, Color.red, 10);

        seeingGround = false;
        RaycastHit hit;
        if(Physics.Raycast(eyesRay, out hit))
        {
            if (hit.collider.CompareTag(PLATFORM_TAG))
                seeingGround = true;
        }

        timeAlive = StayOnPlatformPopulationManager.elapsed;

        //read DNA
        float hMov = 0;
        float vMov = 0;

        //The first gene has the decision to take if youre seeing the ground
        //The second gene has the decision if youre not seeing the ground
        int gene = (seeingGround) ? dna.GetGene(0) : dna.GetGene(1);

        if (gene == 0) { vMov = 1; timeWalking += .1f; }
        else if (gene == 1) hMov = -90;
        else if (gene == 2) hMov = 90;

        transform.Translate(0, 0, vMov * Time.deltaTime * botSpeed);
        transform.Rotate(0, hMov, 0);
    }

}
