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

        try
        {
            // Load the PDF document within a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Check if the PDF has been saved with incremental updates
                bool hasIncremental = doc.HasIncrementalUpdate();

                // Output the result
                Console.WriteLine($"Has incremental updates: {hasIncremental}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}