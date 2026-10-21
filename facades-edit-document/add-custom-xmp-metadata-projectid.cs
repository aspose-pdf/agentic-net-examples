using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string customKey = "ProjectID";
        const string customValue = "12345";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Add or update a custom XMP metadata field using the Metadata dictionary
            pdfDoc.Metadata[customKey] = customValue;

            // Also add the same value to the document's Info dictionary so it appears in the UI properties dialog
            pdfDoc.Info[customKey] = customValue;

            // Save the PDF with the updated metadata
            pdfDoc.Save(outputPath);

            Console.WriteLine($"Custom XMP metadata '{customKey}={customValue}' added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
