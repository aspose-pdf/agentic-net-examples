using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);
                extractor.ExtractAttachment();
                // Extract all embedded files (e.g., PDF attachments) to the output directory.
                extractor.GetAttachment(outputDir);
            }

            Console.WriteLine($"All attachments have been extracted to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachments: {ex.Message}");
        }
    }
}