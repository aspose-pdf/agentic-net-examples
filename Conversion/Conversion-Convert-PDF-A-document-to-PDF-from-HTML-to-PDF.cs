using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input file path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFile> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            Document pdfDocument;

            // Determine conversion based on file extension
            string ext = Path.GetExtension(inputPath).ToLowerInvariant();

            if (ext == ".html" || ext == ".htm")
            {
                // Convert HTML to PDF
                var htmlLoadOptions = new HtmlLoadOptions();
                pdfDocument = new Document(inputPath, htmlLoadOptions);
            }
            else if (ext == ".pdf")
            {
                // Load existing PDF (including PDF/A) and re‑save as regular PDF
                pdfDocument = new Document(inputPath);
            }
            else
            {
                Console.Error.WriteLine("Error: Unsupported input format. Only .html and .pdf are supported.");
                return;
            }

            // Save the document as PDF (regular PDF format)
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Conversion successful. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}