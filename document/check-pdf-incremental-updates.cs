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
            // Check if the PDF has been saved with incremental updates
            bool hasIncrementalUpdate = doc.HasIncrementalUpdate();

            Console.WriteLine($"Has incremental update: {hasIncrementalUpdate}");
        }
    }
}