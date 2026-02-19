using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output PPTX path as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Save the document as PPTX. The file extension determines the format.
            pdfDocument.Save(outputPath); // document-save rule

            Console.WriteLine($"Conversion completed successfully. PPTX saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}