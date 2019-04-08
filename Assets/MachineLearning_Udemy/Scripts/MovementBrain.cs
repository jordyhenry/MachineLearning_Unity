using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class MovementBrain : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;

    public float distanceTravelled = 0;
    private Vector3 startingPosition;

    public MovementDNA dna;

    private ThirdPersonCharacter thirdPersonCharacter;
    private Vector3 movement;
    private bool jump;
    private bool alive = true;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("dead"))
        {
            alive = false;
            distanceTravelled = Vector3.Distance(startingPosition, transform.position);
        }
    }

    public void Init()
    {
        /*
         * 0 - forward
         * 1 - backward
         * 2 - left
         * 3 - right
         * 4 - jump
         * 5 - crouch
         */

        dna = new MovementDNA(DNALength, 4);
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    private void FixedUpdate()
    {
        //read DNA
        float hMov = 0;
        float vMov = 0;
        bool crouch = false;

        int gene = dna.GetGene(0);

        if (gene == 0)
            vMov = 1;
        else if (gene == 1)
            vMov = -1;
        else if (gene == 2)
            hMov = -1;
        else if (gene == 3)
            hMov = 1;
        else if (gene == 4)
            jump = true;
        else if (gene == 5)
            crouch = true;

        movement = vMov * Vector3.forward + hMov * Vector3.right;
        thirdPersonCharacter.Move(movement, crouch, jump);
        jump = false;
        if (alive)
            timeAlive += Time.deltaTime;
    }


}
