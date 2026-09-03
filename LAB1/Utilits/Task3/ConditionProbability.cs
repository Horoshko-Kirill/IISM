namespace Utilits.Task3;

public class ConditionProbability
{
    public ConditionProbability(double pA, double pBA, int n)
    {
        _pA = pA;
        _pBA = pBA;
        _n = n;
    }

    private int _n = 1;
    private double _pA = 0;
    private double _pBA = 0;
    private Random _rnd = new Random();
    
    public int ABCounter = 0;
    public int ANotBCounter = 0;
    public int NotABCounter = 0;
    public int NotANotBCounter = 0;

    public double ABFrequency = 0;
    public double ANotBFrequency = 0;
    public double NotABFrequency = 0;
    public double NotANotBFrequency = 0;

    public double pAB = 0;
    public double pANotB = 0;
    public double pNotAB = 0;
    public double pNotANotB = 0;

    public void Start()
    {   
        pAB = _pA * _pBA;
        pANotB = _pA * (1-_pBA);
        pNotAB = (1-_pA) * (1 - _pBA);
        pNotANotB = (1 - _pA) * _pBA;

        for (int i = 0; i < _n; i++)
        {
            double value = _rnd.NextDouble();

            if (value < pAB)
            {
                ABCounter++;
            }
            else if (value < pAB + pANotB)
            {
                ANotBCounter++;
            }
            else if (value < pAB + pANotB + pNotAB)
            {
                NotABCounter++;
            }
            else if (value < pAB + pANotB + pNotAB + pNotANotB)
            {
                NotANotBCounter++;
            }
        }

        ABFrequency = (double)ABCounter / _n;
        ANotBFrequency = (double)ANotBCounter / _n;
        NotABFrequency = (double)NotABCounter / _n;
        NotANotBFrequency = (double)NotANotBCounter / _n;
    }
}