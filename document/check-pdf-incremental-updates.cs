using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Check whether the PDF has been saved with incremental updates
            // HasIncrementalUpdate is a method, so invoke it
            bool hasIncremental = doc.HasIncrementalUpdate();

            // Output the result
            Console.WriteLine($"Has incremental updates: {hasIncremental}");
        }
    }
}
