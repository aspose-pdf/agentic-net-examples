using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_optimized.pdf";

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
                // Remove unused resources and merge duplicates
                doc.OptimizeResources();

                // Linearize the document for faster web access (optional)
                doc.Optimize();

                // Convert to PDF/A with size‑reduction flag enabled
                var conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
                {
                    OptimizeFileSize = true,
                    ErrorAction = ConvertErrorAction.Delete
                };
                doc.Convert(conversionOptions);

                // Save the optimized PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Optimized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}