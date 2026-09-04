namespace Utilits.Fortune;

public class FortuneGenerator
{
    public FortuneGenerator(List<double> ps)
    {
        _ps = ps;
    }
    
    private List<double> _ps;
    
    private List<double> PrefSum = new();

    private void FindPrefSum()
    {
        PrefSum.Add(_ps[0]);
        for (int i = 1; i < _ps.Count; i++)
        {
            PrefSum.Add(_ps[i] + PrefSum[i - 1]);
        }
    }
    
    Random rnd = new Random();

    public int NumWin { get; set; } = 0;

    public void Start()
    {
        double value = rnd.NextDouble();
        
        FindPrefSum();

        int j = 0;
        while (j < PrefSum.Count - 1 && value >= PrefSum[j])
        {
            j++;
        }

        NumWin = j;
    }
}