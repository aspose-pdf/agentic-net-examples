using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPsPath  = "input.ps";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        try
        {
            // Load the PostScript file and convert it to a PDF document
            using (Document pdfDoc = new Document())
            {
                // PsLoadOptions is the load option for PostScript files
                pdfDoc.LoadFrom(inputPsPath, new PsLoadOptions());

                // Save the result as a regular PDF (non‑PDF/A)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully converted '{inputPsPath}' to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}