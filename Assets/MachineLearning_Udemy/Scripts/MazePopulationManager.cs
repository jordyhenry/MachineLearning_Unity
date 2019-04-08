using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazePopulationManager : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 30;
    public float trialTime = 10f;
    public float elapsed = 0;

    private List<MazeBrain> population = new List<MazeBrain>();
    private int generation = 1;

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            MazeBrain brain = CreateBot();
            brain.Init();
            population.Add(brain);
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }

        UIStatsManager.Instance.SetStats(generation, population.Count, elapsed);
    }

    public MazeBrain Breed(MazeBrain parent1, MazeBrain parent2)
    {
        MazeBrain offspring = CreateBot();
        offspring.Init();

        if (Random.Range(0, 100) == 1)
            offspring.dna.Mutate();
        else
            offspring.dna.Combine(parent1.dna, parent2.dna);

        return offspring;
    }

    public void BreedNewPopulation()
    {
        List<MazeBrain> sortedList = population.OrderBy(o => o.distanceFromStart).ToList();
        population.Clear();
        for(int i = (int)(sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i].gameObject);
        }

        generation++;

    }

    private MazeBrain CreateBot()
    {
        return Instantiate(botPrefab, transform.position, transform.rotation, transform).GetComponent<MazeBrain>();
    }
}
