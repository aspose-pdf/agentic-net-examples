using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove PDF/A compliance (tags) if present
                doc.RemovePdfaCompliance();

                // Remove PDF/UA compliance (tags) if present
                doc.RemovePdfUaCompliance();

                // Save the resulting PDF without tags
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tags removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}