namespace Utilits.Task4;

public class GroupProbability
{
    public GroupProbability(List<double> ps, int n)
    {
        _ps = ps;
        _n = n;
        FindPrefSum();
        Counters = new List<int>(new int[_ps.Count]);
    }

    private int _n = 1;
    private List<double> _ps = new();

    private List<double> PrefSum = new();

    private void FindPrefSum()
    {
        PrefSum.Add(_ps[0]);
        for (int i = 1; i < _ps.Count; i++)
        {
            PrefSum.Add(_ps[i] + PrefSum[i - 1]);
        }
    }
    
    public List<int> Counters { get; set; } = new List<int>();
    public List<double> Frequencys { get; set; } = new List<double>();
    
    Random rnd = new Random();

    public void Start()
    {
        for (int i = 0; i < _n; i++)
        {
            double value = rnd.NextDouble();

            int j = 0;
            while (j < PrefSum.Count - 1 && value >= PrefSum[j])
            {
                j++;
            }

            Counters[j]++;
        }

        for (int i = 0; i < Counters.Count; i++)
        {
            Frequencys.Add((double)Counters[i] / _n);
        }
    }
}