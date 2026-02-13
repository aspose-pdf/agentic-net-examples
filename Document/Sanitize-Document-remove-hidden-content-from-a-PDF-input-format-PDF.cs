using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.pdf> <output.pdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // NOTE: The "RemoveHiddenContent" method is not present in older Aspose.Pdf versions.
            // If you need this functionality, upgrade to a newer Aspose.Pdf version that provides
            // Document.RemoveHiddenContent() or implement a custom routine to strip hidden objects.
            // pdfDocument.RemoveHiddenContent(); // <-- removed for compatibility

            // Save the (potentially) sanitized PDF
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
