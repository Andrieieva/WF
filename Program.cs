using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class WordFrequencyCalculator
{
    static void Main()
    {
        string inputFilePath = "input.txt"; 
        string outputFilePath = "output.csv"; 

        try
        {
            string textContent = File.ReadAllText(inputFilePath);

            Dictionary<string, int> wordFrequencies = CalculateWordFrequencies(textContent);

            WriteToCsv(wordFrequencies, outputFilePath);

            Console.WriteLine("Word frequencies calculated and saved to CSV file successfully.");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Please provide a valid path for the input file.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("You don't have permission to write to the specified output file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("There was an issue accessing the input or output file.");
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
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("word, count, frequency");

                foreach (var entry in wordFrequencies.OrderByDescending(e => e.Value))
                {
                    double frequency = (double)entry.Value / wordFrequencies.Values.Sum();
                    writer.WriteLine($"{entry.Key}, {entry.Value}, {frequency:P2}");
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("You don't have permission to write to the specified output file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("There was an issue accessing the output file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while writing to the CSV file: {ex.Message}");
        }
    }
}
