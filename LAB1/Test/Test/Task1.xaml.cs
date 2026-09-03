using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilits.Task1;

namespace Test;

public partial class Task1 : ContentPage
{
    public Task1()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {

        try
        {
            
            string inputText = ProbabilityEntry.Text.Replace('.', ',');
            
            if (!double.TryParse(inputText, out double probability))
            {
                await DisplayAlert("Ошибка", "Введите корректное число для вероятности\nПример: 0.5", "OK");
                return;
            }

            if (probability < 0 || probability > 1)
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
            
            string result = await Task.Run(() => CalculateProbability(probability, attempts));
            
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

    private string CalculateProbability(double p, int n)
    {

        TrueFalseGenerator generator = new TrueFalseGenerator(p, n); 
        
        generator.Start();
        
        return $"Результаты теста\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
               $"Попыток: {n}\n" +
               $"Успехов: {generator.TrueCounter}\n" +
               $"Неудач: {generator.FalseCounter}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
               $"Теоретическая вероятность: {p:P4}\n" +
               $"Фактическая вероятность: {generator.TrueFrequency:P4}\n" +
               $"Отклонение: {p-generator.TrueFrequency:P4}\n" +
               $"━━━━━━━━━━━━━━━━━━━━━━━";
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        ResultFrame.IsVisible = false;
        ResultLabel.Text = "";
        StatusLabel.Text = "Готов к работе";
        StatusLabel.TextColor = Colors.Green;
    }
}