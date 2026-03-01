using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace (optional for this example)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ps";   // PostScript file to open
        const string outputPath = "output.pdf"; // Resulting PDF file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PS file using the dedicated load options.
            // PS is an input‑only format; we cannot save as PS.
            using (Document doc = new Document(inputPath, new PsLoadOptions()))
            {
                // Save the loaded document as PDF (default format).
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted '{inputPath}' to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}