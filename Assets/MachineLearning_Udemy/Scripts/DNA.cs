using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    private List<int> genes = new List<int>();
    private int dnaLength = 0;
    private int maxValue = 0;

    public DNA(int _dnaLength, int _maxValue)
    {
        dnaLength = _dnaLength;
        maxValue = _maxValue;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }

    public void SetInt(int _pos, int _value)
    {
        genes[_pos] = Mathf.Clamp(_value, 0, maxValue);
    }

    public void Combine(DNA d1, DNA d2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            int c = (i < dnaLength / 2.0) ? d1.genes[i] : d2.genes[i];
            genes[i] = c;
        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValue);
    }

    public int GetGene(int _pos)
    {
        return genes[_pos];
    }
}
