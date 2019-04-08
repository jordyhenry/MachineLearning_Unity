using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StayOnPlatformPopulationManager : MonoBehaviour
{
    public GameObject botPrefab;
    public int populationSize = 50;
    public float trialTime = 5;
    public static float elapsed = 0;

    private List<StayOnPlatformBrain> population = new List<StayOnPlatformBrain>();
    private int generation = 1;

    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            StayOnPlatformBrain b = CreateBot();
            b.Init();
            population.Add(b);
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

    public StayOnPlatformBrain Breed(StayOnPlatformBrain parent1, StayOnPlatformBrain parent2)
    {
        StayOnPlatformBrain offspring = CreateBot();
        offspring.Init();

        if (Random.Range(0, 100) == 1)
            offspring.dna.Mutate();
        else
            offspring.dna.Combine(parent1.dna, parent2.dna);

        return offspring;
    }

    public void BreedNewPopulation()
    {
        List<StayOnPlatformBrain> sortedList = population.OrderBy(o => (o.timeAlive * 3 + o.timeWalking)).ToList();
        population.Clear();
        for (int i = (int) (sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
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

    private StayOnPlatformBrain CreateBot()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(-2f, 2f);
        pos.z += Random.Range(-2f, 2f);

        return Instantiate(botPrefab, pos, transform.rotation, transform).GetComponent<StayOnPlatformBrain>();
    }
}
