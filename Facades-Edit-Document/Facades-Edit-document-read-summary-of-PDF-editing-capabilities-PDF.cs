using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf provides the Document class for PDF operations

class Program
{
    static void Main(string[] args)
    {
        // Expect the path to the PDF file as the first command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <pdfPath>");
            return;
        }

        string pdfPath = args[0];

        // Verify that the specified file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document using Aspose.Pdf.Document.
            Document pdfDocument = new Document(pdfPath);

            // Provide a simple capabilities summary – here we show basic information.
            Console.WriteLine("PDF Loading Capabilities Summary:");
            Console.WriteLine($"Number of pages: {pdfDocument.Pages.Count}");
            Console.WriteLine($"Document title: {pdfDocument.Info.Title ?? "(none)"}");
            Console.WriteLine($"Author: {pdfDocument.Info.Author ?? "(none)"}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
