using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CenteredRTL.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Center the PDF window when opened in a viewer
            doc.CenterWindow = true;

            // Set the reading direction to right‑to‑left (RTL)
            doc.Direction = Direction.R2L; // Correct enum for RTL direction

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved centered RTL PDF to '{outputPath}'.");
    }
}
