using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document to the extractor
                extractor.BindPdf(pdfPath);

                // Extract all embedded attachments to the output directory.
                // The method preserves the original file names and extensions.
                extractor.ExtractAttachment(outputDir);

                // List the extracted files for confirmation
                foreach (var filePath in Directory.GetFiles(outputDir))
                {
                    Console.WriteLine($"Saved attachment: {filePath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
