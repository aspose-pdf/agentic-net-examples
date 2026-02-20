using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        try
        {
            // Verify that the source PDF exists
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
                return;
            }

            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Update metadata directly via the Document.Info object
            pdfDocument.Info.Title = "Updated Sample Title";
            pdfDocument.Info.Author = "Jane Smith";
            pdfDocument.Info.Keywords = "Aspose, PDF, Metadata, Updated";

            // Save the updated PDF to a new file
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Metadata successfully updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
