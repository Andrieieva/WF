using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class WordFrequencyCalculator
{
    static void Main()
    {
        string inputFilePath = "input.txt"; // Replace with your input file path
        string outputFilePath = "output.csv"; // Replace with your desired output file path

        try
        {
            // Read the text file
            string textContent = File.ReadAllText(inputFilePath);

            // Calculate word frequencies
            Dictionary<string, int> wordFrequencies = CalculateWordFrequencies(textContent);

            // Write results to CSV file
            WriteToCsv(wordFrequencies, outputFilePath);

            Console.WriteLine("Word frequencies calculated and saved to CSV file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static Dictionary<string, int> CalculateWordFrequencies(string text)
    {
        string[] words = text.Split(' ', '.', ',', ';', ':', '!', '?', '\r', '\n');
        Dictionary<string, int> wordFrequencies = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                string cleanedWord = word.ToLower();
                if (wordFrequencies.ContainsKey(cleanedWord))
                {
                    wordFrequencies[cleanedWord]++;
                }
                else
                {
                    wordFrequencies[cleanedWord] = 1;
                }
            }
        }

        return wordFrequencies;
    }

    static void WriteToCsv(Dictionary<string, int> wordFrequencies, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header
            writer.WriteLine("word, count, frequency");

            // Write data
            foreach (var entry in wordFrequencies.OrderByDescending(e => e.Value))
            {
                double frequency = (double)entry.Value / wordFrequencies.Values.Sum();
                writer.WriteLine($"{entry.Key}, {entry.Value}, {frequency:P2}");
            }
        }
    }
}
