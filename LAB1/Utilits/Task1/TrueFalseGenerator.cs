namespace Utilits.Task1;

public class TrueFalseGenerator
{
    public TrueFalseGenerator(double p, int n)
    {
        _p = p;
        _n = n;
    }

    private double _p { get; set; } = 0;
    private int _n { get; set; } = 1;
    private int _counter { get; set; } = 0;
    private Random _rnd { get; set; } = new Random();

    private bool Generate()
    {
        double randomNumber = _rnd.NextDouble();
        if (randomNumber <= _p)
        {
            return true;
        }

        return false;
    }

    public double TrueFrequency { get; set; } = 0;
    public double FalseFrequency { get; set; } = 0;
    public int TrueCounter { get; set; } = 0;
    public int FalseCounter { get; set; } = 0;

    public void Start()
    {
        for (int i = 0; i < _n; i++)
        {
            if (Generate())
            {
                _counter++;
            };
        }
        
        TrueFrequency = (double)_counter / _n;
        FalseFrequency = (double)(_n-_counter) / _n;
        TrueCounter = _counter;
        FalseCounter = _n-_counter;
    }
}