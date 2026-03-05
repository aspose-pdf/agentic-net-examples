using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for potential text operations

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Example processing: output page count
                Console.WriteLine($"Page count: {doc.Pages.Count}");

                // Save the document (could be the same file or a new one)
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        // Capture the most specific Aspose.Pdf exception first
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid PDF format: {ex.Message}");
        }
        // Then capture the more general Aspose.Pdf exception
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        // Fallback for any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}