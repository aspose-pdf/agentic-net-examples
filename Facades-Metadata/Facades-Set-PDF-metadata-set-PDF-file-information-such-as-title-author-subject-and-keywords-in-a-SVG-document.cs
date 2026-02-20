using System;
using System.IO;
using Aspose.Pdf;          // Core PDF API (used for loading, setting metadata, saving)
using Aspose.Pdf.Facades; // Included as required by the task (facade namespace)

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output SVG path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SetPdfMetadata <input.pdf> <output.svg>");
            return;
        }

        string inputPdfPath = args[0];
        string outputSvgPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Set the required metadata fields
            pdfDocument.Info.Title   = "Sample PDF Title";
            pdfDocument.Info.Author  = "John Doe";
            pdfDocument.Info.Subject = "Demonstration of PDF metadata via Aspose.Pdf.Facades";
            pdfDocument.Info.Keywords = "Aspose, PDF, metadata, SVG";

            // Save the document as SVG.
            // The file extension determines the output format, so a simple Save call is sufficient.
            pdfDocument.Save(outputSvgPath);
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}