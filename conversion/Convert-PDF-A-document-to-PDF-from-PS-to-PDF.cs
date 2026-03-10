using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPsPath  = "input.ps";   // PostScript (PDF/A) source
        const string outputPdfPath = "output.pdf"; // Desired PDF output

        // Verify source file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPsPath}");
            return;
        }

        try
        {
            // Load the PS file using PsLoadOptions (PS is an input‑only format)
            using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
            {
                // If the document carries PDF/A compliance, remove it to obtain a regular PDF
                doc.RemovePdfaCompliance();

                // Save as PDF (default format when only a path is supplied)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Conversion completed: '{outputPdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}