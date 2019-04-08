using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MovementPopulationManager : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    private int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.black;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen : " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time : {0:0.00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population : " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = CreateNewBot();
            go.GetComponent<MovementBrain>().Init();
            population.Add(go);
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            elapsed = 0;
            BreedNewPopulation();
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        GameObject offspring = CreateNewBot();
        MovementBrain brain = offspring.GetComponent<MovementBrain>();
        if(Random.Range(0, 100) == 1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<MovementBrain>().dna, parent2.GetComponent<MovementBrain>().dna);
        }

        return offspring;
    }

    private void BreedNewPopulation()
    {
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<MovementBrain>().timeAlive).ToList();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<MovementBrain>().distanceTravelled).ToList();

        population.Clear();
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

    GameObject CreateNewBot()
    {
        Vector3 startingPos = Vector3.zero;
        startingPos.x = transform.position.x + Random.Range(-2, 2);
        startingPos.y = transform.position.y;
        startingPos.z = transform.position.z + Random.Range(-2, 2);

        return Instantiate(botPrefab, startingPos, transform.rotation);
    }



}
