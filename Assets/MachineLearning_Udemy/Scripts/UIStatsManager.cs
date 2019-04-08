using UnityEngine;
using UnityEngine.UI;

public class UIStatsManager : MonoBehaviour
{
    public Text generationText;
    public Text populationText;
    public Text timeText;

    public static UIStatsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void SetStats(int _generation, int _population, float _time)
    {
        generationText.text = "Generation : " + _generation.ToString();
        populationText.text = "Population : " + _population.ToString();
        timeText.text = "Time : " + _time.ToString();
    }
}
