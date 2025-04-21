using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNASequencer;


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAnalyzeClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ResultsList.ItemsSource = null;

            var sequence = SequenceInput.Text?.ToUpper().Replace(" ", "").Replace("\n", "").Replace("\r", "") ?? "";

            if (sequence.Length < 4)
            {
                ResultsList.ItemsSource = new[] { "Sekwencja musi mieć co najmniej 4 znaki." };
                return;
            }

            Dictionary<string, int> tetramers = new();

            for (int i = 0; i <= sequence.Length - 4; i++)
            {
                string sub = sequence.Substring(i, 4);
                if (!sub.All(c => "ACGT".Contains(c)))
                    continue;

                if (tetramers.ContainsKey(sub))
                    tetramers[sub]++;
                else
                    tetramers[sub] = 1;
            }

            ResultsList.ItemsSource = tetramers
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => $"{kvp.Key} → {kvp.Value}");
        }
    }

