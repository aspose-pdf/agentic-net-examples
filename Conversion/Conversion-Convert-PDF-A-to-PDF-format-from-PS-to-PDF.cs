using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PostScript (PS) file path
        string inputPath = "input.ps";
        // Output PDF file path
        string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PS file using PsLoadOptions (LoadFormat.PS)
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Load the document
            using (Document pdfDocument = new Document(inputPath, loadOptions))
            {
                // Save as regular PDF (PDF/A compliance is not enforced)
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed successfully. PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}