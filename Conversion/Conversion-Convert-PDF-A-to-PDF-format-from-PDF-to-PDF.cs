using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_pdfa.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Remove PDF/A compliance to obtain a regular PDF
                doc.RemovePdfaCompliance();

                // Save the result as a standard PDF file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted PDF/A to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}