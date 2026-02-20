using System;
using System.IO;
using Aspose.Pdf;                 // Document and OfdLoadOptions
using Aspose.Pdf.Facades;        // PdfFileInfo facade

class Program
{
    static void Main()
    {
        // Paths for the source OFD and the output OFD
        const string inputPath = "input.ofd";
        const string outputPath = "output.ofd";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the OFD document to ensure it is a valid file.
            // The Document object is not strictly required for metadata editing,
            // but loading it validates the format and allows further processing if needed.
            Document doc = new Document(inputPath, new OfdLoadOptions());

            // Create a PdfFileInfo facade bound to the source file.
            // This facade provides access to the document's metadata properties.
            PdfFileInfo fileInfo = new PdfFileInfo(inputPath);

            // Set the desired metadata fields.
            fileInfo.Title    = "Sample OFD Title";
            fileInfo.Author   = "John Doe";
            fileInfo.Subject  = "Demonstration of metadata editing";
            fileInfo.Keywords = "Aspose, OFD, Metadata";

            // Save the updated OFD. The Save method preserves the original format.
            fileInfo.Save(outputPath);

            Console.WriteLine($"Metadata successfully updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}