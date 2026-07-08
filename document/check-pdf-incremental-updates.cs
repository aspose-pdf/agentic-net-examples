using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Determine whether the PDF contains incremental updates
            bool hasIncremental = doc.HasIncrementalUpdate();

            // Report the result
            Console.WriteLine($"Has incremental updates: {hasIncremental}");
        }
    }
}