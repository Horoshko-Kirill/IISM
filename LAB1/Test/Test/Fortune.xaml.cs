using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using Utilits.Fortune;

namespace Test;

public partial class Fortune : ContentPage
{
    public Fortune()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ObservableCollection<FortuneRowItem> ProbabilityRows { get; set; } = new();
    private async void OnStartClicked(object? sender, EventArgs e)
    {
        try
        {
            List<double> probabilitys = new();

            double sumDonat = 0;

            for (int i = 0; i < ProbabilityRows.Count; i++)
            {
                int.TryParse(ProbabilityRows[i].Donat, out var donat);
                sumDonat += donat;
            }

            for (int i = 0; i < ProbabilityRows.Count; i++)
            {
                int.TryParse(ProbabilityRows[i].Donat, out var donat);
                probabilitys.Add(donat / sumDonat);
            }
            
            StartButton.IsEnabled = false;
            ClearButton.IsEnabled = false;
            StartButton.Text = "Выполняется...";
            StatusLabel.Text = "Выполняется...";
            StatusLabel.TextColor = Colors.Orange;
            
            string result = await Task.Run(() => CalculateProbability(probabilitys));

            await DisplayAlert("ПОБЕДИТЕЛЬ", result, "ОК");
            
            ClearButton.IsEnabled = true;
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
    
    private string CalculateProbability(List<double> probabilities)
    {

        FortuneGenerator generator = new FortuneGenerator(probabilities); 
        
        generator.Start();
        
        return $"{ProbabilityRows[generator.NumWin].NameGame}\n";
    }

    private async void OnAddRawClicked(object? sender, EventArgs e)
    {
        if (AddNameGame.Text == string.Empty && AddNameGame.Text == "")
        {
            await DisplayAlert("Ошибка", "Введите непустое название игры", "OK");
            return;
        }

        if (int.TryParse(AddDonat.Text, out var donat) && donat < 0)
        {
            await DisplayAlert("Ошибка", "Введите корректный донат", "OK");
            return;
        }

        for (int i = 0; i < ProbabilityRows.Count; i++)
        {
            if (ProbabilityRows[i].NameGame.ToLower() == AddNameGame.Text.ToLower())
            {
                ProbabilityRows[i].Donat = (Convert.ToDouble(ProbabilityRows[i].Donat) + donat).ToString();
                return;
            }
        }
        
        ProbabilityRows.Add(new FortuneRowItem { Donat = donat.ToString(), NameGame = AddNameGame.Text});
    }

    private void OnDeleteRawClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is FortuneRowItem itemToRemove)
        {
            ProbabilityRows.Remove(itemToRemove);
        }
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        ProbabilityRows.Clear();
        
        StatusLabel.Text = "Готов к работе";
        StatusLabel.TextColor = Colors.Green;
    }
}