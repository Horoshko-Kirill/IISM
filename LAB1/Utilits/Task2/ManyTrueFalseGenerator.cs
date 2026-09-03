using Utilits.Task1;

namespace Utilits.Task2;

public class ManyTrueFalseGenerator
{
    public ManyTrueFalseGenerator(List<double> ps, int n)
    {
        _ps = ps;
        _n = n;
    }
    
    private List<double> _ps;
    private int _n;
    
    public List<double> TrueFrequencys { get; set; } = new List<double>();
    public List<double> FalseFrequencys { get; set; } = new List<double>();
    public List<int> TrueCounters { get; set; } = new List<int>();
    public List<int> FalseCounters { get; set; } = new List<int>();

    public void Start()
    {
        for (int i = 0; i < _ps.Count; i++)
        {
            TrueFalseGenerator generator = new TrueFalseGenerator(_ps[i], _n);
            generator.Start();
            TrueCounters.Add(generator.TrueCounter);
            FalseCounters.Add(generator.FalseCounter);
            TrueFrequencys.Add(generator.TrueFrequency);
            FalseFrequencys.Add(generator.FalseFrequency);
        }
    }
}