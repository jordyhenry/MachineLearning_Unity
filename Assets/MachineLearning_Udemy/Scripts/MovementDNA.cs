using System.Collections.Generic;
using UnityEngine;

public class MovementDNA
{

    private List<int> genes = new List<int>();
    private int dnaLength = 0;
    private int maxValues = 0;

    public MovementDNA (int _dnaLength, int _maxValues)
    {
        dnaLength = _dnaLength;
        maxValues = _maxValues;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValues));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    public void Combine(MovementDNA d1, MovementDNA d2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            if(i < dnaLength / 2.0)
            {
                int c = d1.genes[i];
                genes[i] = c;
            }
            else
            {
                int c = d2.genes[i];
                genes[i] = c;
            }
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValues);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
