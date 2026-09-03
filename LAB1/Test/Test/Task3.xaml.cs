using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilits.Task3;

namespace Test;

public partial class Task3 : ContentPage
{
    public Task3()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object? sender, EventArgs e)
    {
        try
        {
            
            string inputTextA = ProbabilityEntryA.Text.Replace('.', ',');
            string inputTextAB = ProbabilityEntryAB.Text.Replace('.', ',');
            
            if (!double.TryParse(inputTextA, out double probabilityA))
            {
                await DisplayAlert("Ошибка", "Введите корректное число для вероятности\nПример: 0.5", "OK");
                return;
            }

            if (probabilityA < 0 || probabilityA > 1)
            {
                await DisplayAlert("Ошибка", "Введите корректное число для вероятности\nПример: 0.5", "OK");
                return;
            }
            
            if (!double.TryParse(inputTextAB, out double probabilityAB))
            {
                await DisplayAlert("Ошибка", "Введите корректное число для вероятности\nПример: 0.5", "OK");
                return;
            }

            if (probabilityAB < 0 || probabilityAB > 1)
            {
                await DisplayAlert("Ошибка", "Введите корректное число для вероятности\nПример: 0.5", "OK");
                return;
            }
            
            if (!int.TryParse(AttemptsEntry.Text, out int attempts))
            {
                await DisplayAlert("Ошибка", "Введите корректное число попыток\nПример: 100", "OK");
                return;
            }

            if (attempts < 1 || attempts > 1000000000)
            {
                await DisplayAlert("Ошибка", "Введите корректное число попыток\nПример: 100", "OK");
                return;
            }
            
            StartButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
            StartButton.Text = "Выполняется...";
            StatusLabel.Text = "Выполняется...";
            StatusLabel.TextColor = Colors.Orange;
            
            string result = await Task.Run(() => CalculateProbability(probabilityA, probabilityAB, attempts));
            
            ClearButton.IsEnabled = true;
            ResultLabel.Text = result;
            ResultFrame.IsVisible = true;
            StatusLabel.Text = "Готово!";
            StatusLabel.TextColor = Colors.Green;
        }
        catch (Exception exception)
        {
            await DisplayAlert("Ошибка", $"Произошла ошибка:\n{exception.Message}", "OK");
            StatusLabel.Text = "Ошибка";
            StatusLabel.TextColor = Colors.Red;
        }
        finally
        {
            StartButton.IsEnabled = true;
            StartButton.Text = "Запустить";
        }
    }
    
    private string CalculateProbability(double pA, double pBA, int n)
    {

        ConditionProbability generator = new ConditionProbability(pA, pBA, n); 
        
        generator.Start();
        
        return $"Результаты теста\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
               $"Попыток: {n}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━"+
               $"Вид: P(AB)\n" +
               $"Успехов: {generator.ABCounter}\n" +
               $"Теоретическая вероятность: {generator.pAB:P4}\n" +
               $"Фактическая вероятность: {generator.ABFrequency:P4}\n" +
               $"Отклонение: {generator.pAB-generator.ABFrequency:P4}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" + 
               $"Вид: P(A-B)\n" +
               $"Успехов: {generator.ANotBCounter}\n" +
               $"Теоретическая вероятность: {generator.pANotB:P4}\n" +
               $"Фактическая вероятность: {generator.ANotBFrequency:P4}\n" +
               $"Отклонение: {generator.pANotB-generator.ANotBFrequency:P4}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
               $"Вид: P(-AB)\n" +
               $"Успехов: {generator.NotABCounter}\n" +
               $"Теоретическая вероятность: {generator.pNotAB:P4}\n" +
               $"Фактическая вероятность: {generator.NotABFrequency:P4}\n" +
               $"Отклонение: {generator.pNotAB-generator.NotABFrequency:P4}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
               $"Вид: P(-A-B)\n" +
               $"Успехов: {generator.NotANotBCounter}\n" +
               $"Теоретическая вероятность: {generator.pNotANotB:P4}\n" +
               $"Фактическая вероятность: {generator.NotANotBFrequency:P4}\n" +
               $"Отклонение: {generator.pNotANotB-generator.NotANotBFrequency:P4}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n";
        
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        ResultFrame.IsVisible = false;
        ResultLabel.Text = "";
        StatusLabel.Text = "Готов к работе";
        StatusLabel.TextColor = Colors.Green;
    }
}