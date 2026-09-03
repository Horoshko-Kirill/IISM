using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using Utilits.Task2;

namespace Test;

public partial class Task2 : ContentPage
{
    public Task2()
    {
        InitializeComponent();
        BindingContext = this;
    }
    
    public ObservableCollection<MyRowItem> ProbabilityRows { get; set; } = new();

    private async void OnStartClicked(object? sender, EventArgs e)
    {
        try
        {
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

            List<double> probabilitys = new();
            
            for (int i = 0; i < ProbabilityRows.Count; i++)
            {
                string inputText = ProbabilityRows[i].Probability.Replace('.', ',');

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
                
                probabilitys.Add(probability);
            }

            StartButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
            StartButton.Text = "Выполняется...";
            StatusLabel.Text = "Выполняется...";
            StatusLabel.TextColor = Colors.Orange;
            
            List<string> result = await Task.Run(() => CalculateProbability(probabilitys, attempts));
            
            ClearButton.IsEnabled = true;
            StatusLabel.Text = "Готово!";
            StatusLabel.TextColor = Colors.Green;
            for (int i = 0; i < ProbabilityRows.Count; i++)
            {
                ProbabilityRows[i].ResultText = result[i];
                ProbabilityRows[i].IsResultVisible = true;
            }
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
    
    private List<string> CalculateProbability(List<double> p, int n)
    {

        ManyTrueFalseGenerator generator = new ManyTrueFalseGenerator(p, n); 
        
        generator.Start();

        List<string> result = new();

        for (int i = 0; i < p.Count; i++)
        {

            result.Add($"Результаты теста\n" +
                      $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
                      $"Попыток: {n}\n" +
                      $"Успехов: {generator.TrueCounters[i]}\n" +
                      $"Неудач: {generator.FalseCounters[i]}\n" +
                      $"━━━━━━━━━━━━━━━━━━━━━━━\n" +
                      $"Теоретическая вероятность: {p[i]:P4}\n" +
                      $"Фактическая вероятность: {generator.TrueFrequencys[i]:P4}\n" +
                      $"Отклонение: {p[i] - generator.TrueFrequencys[i]:P4}\n" +
                      $"━━━━━━━━━━━━━━━━━━━━━━━\n");
        }

        return result;
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        for (int i = 0; i < ProbabilityRows.Count; i++)
        {
            ProbabilityRows[i].IsResultVisible = false;
            ProbabilityRows[i].ResultText = "";
        }
        StatusLabel.Text = "Готов к работе";
        StatusLabel.TextColor = Colors.Green;
    }

    private void OnAddRawClicked(object? sender, EventArgs e)
    {
        ProbabilityRows.Add(new MyRowItem());
    }

    private void OnDeleteRawClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is MyRowItem itemToRemove)
        {
            ProbabilityRows.Remove(itemToRemove);
        }
    }
}
