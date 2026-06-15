using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The core Aspose.Pdf API does not expose a direct zoom property.
            // As an alternative, enable FitWindow so that the PDF viewer
            // automatically resizes the window to fit the first page,
            // which provides a similar “zoom‑to‑fit” user experience.
            doc.FitWindow = true;

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with FitWindow enabled: {outputPath}");
    }
}