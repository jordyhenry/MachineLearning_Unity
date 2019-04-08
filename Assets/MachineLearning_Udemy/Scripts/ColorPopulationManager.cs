using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorPopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationSize = 10;
    public int trialTime = 10;
    
    public static float elapsed = 0;

    private List<GameObject> populationList = new List<GameObject>();
    private int generation = 1;

    GUIStyle guiStyle = new GUIStyle();
    
    private void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            ColorDNA goDNA = go.GetComponent<ColorDNA>();
            goDNA.r = Random.Range(0f, 1.0f);
            goDNA.g = Random.Range(0f, 1.0f);
            goDNA.b = Random.Range(0f, 1.0f);
            goDNA.s = Random.Range(0.1f, 0.5f);
            
            populationList.Add(go);
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation : " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time : " + (int)elapsed, guiStyle);
    }

    private void BreedNewPopulation()
    {
        //Get rid of unfit individuals
        List<GameObject> sortedList = populationList.OrderByDescending(o => o.GetComponent<ColorDNA>().timeToDie).ToList();

        populationList.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            populationList.Add(Breed(sortedList[i], sortedList[i + 1]));
            populationList.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }

        generation++;
    }

    private GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);

        ColorDNA dna1 = parent1.GetComponent<ColorDNA>();
        ColorDNA dna2 = parent2.GetComponent<ColorDNA>();

        ColorDNA offspringDna = offspring.GetComponent<ColorDNA>();
        if (Random.Range(0, 1000) > 5)
        {
            //Swap parent dna
            offspringDna.r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspringDna.g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspringDna.b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspringDna.s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;
        }
        else
        {
            //Mutation dna
            offspringDna.r = Random.Range(0f, 1f);
            offspringDna.g = Random.Range(0f, 1f);
            offspringDna.b = Random.Range(0f, 1f);
            offspringDna.s = Random.Range(0.1f, 0.3f);
        }
        
        return offspring;
    }
}
