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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF/A document
            using (Document doc = new Document(inputPath))
            {
                // Remove PDF/A compliance to obtain a regular PDF
                doc.RemovePdfaCompliance();

                // Save the result as a standard PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A converted to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}