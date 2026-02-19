using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source EPUB file (the file to be opened as a PDF)
        string epubPath = "input.epub";

        // Path where the resulting PDF will be saved
        string pdfOutputPath = "output.pdf";

        // Verify that the EPUB file exists before attempting to load it
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at '{epubPath}'.");
            return;
        }

        try
        {
            // Create load options for EPUB format
            var loadOptions = new EpubLoadOptions();

            // Load the EPUB file into an Aspose.Pdf Document using the load options
            var pdfDocument = new Document(epubPath, loadOptions);

            // Save the loaded document as a PDF file
            pdfDocument.Save(pdfOutputPath);

            Console.WriteLine($"EPUB file successfully converted and saved as PDF at '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}