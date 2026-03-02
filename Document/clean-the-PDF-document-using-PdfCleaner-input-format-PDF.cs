using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned_output.pdf";

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
                // Remove PDF/A and PDF/UA compliance if present
                doc.RemovePdfaCompliance();
                doc.RemovePdfUaCompliance();

                // Remove metadata from the document
                doc.RemoveMetadata();

                // Optimize resources (remove unused objects, merge duplicates)
                doc.OptimizeResources();

                // Linearize the document for faster web access
                doc.Optimize();

                // Save the cleaned PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}