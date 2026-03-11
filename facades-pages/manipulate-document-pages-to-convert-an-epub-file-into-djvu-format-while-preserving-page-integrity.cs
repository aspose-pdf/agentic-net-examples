using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input EPUB file path
        const string epubPath = "input.epub";

        // Intermediate PDF file path (preserves page integrity)
        const string pdfPath = "intermediate.pdf";

        // Verify that the source EPUB file exists before attempting to load it.
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at path '{epubPath}'.");
            return;
        }

        try
        {
            // Load the EPUB file into a PDF Document using EpubLoadOptions
            using (Document pdfDoc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Save the document as PDF – this is the format Aspose.Pdf can output
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine("EPUB successfully converted to PDF.");
            Console.WriteLine($"PDF saved at: {pdfPath}");
            Console.WriteLine("Conversion to DJVU is not supported by Aspose.Pdf.");
            Console.WriteLine("Use a third‑party tool to convert the PDF to DJVU while preserving pages.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., corrupted EPUB, permission issues)
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
