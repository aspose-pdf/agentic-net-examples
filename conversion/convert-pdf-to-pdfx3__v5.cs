using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDirectory = "Output";
        const string outputFileName = "converted_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);
        string outputPath = Path.Combine(outputDirectory, outputFileName);

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/X-3 format; log conversion details
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted PDF/X-3 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X-3 document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}