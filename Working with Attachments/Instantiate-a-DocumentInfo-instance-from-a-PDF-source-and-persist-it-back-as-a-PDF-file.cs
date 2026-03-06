using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Instantiate a DocumentInfo object from the loaded document
            DocumentInfo info = new DocumentInfo(doc);

            // Example modifications to the metadata
            info.Title    = "Updated Title";
            info.Author   = "John Doe";
            info.Subject  = "Sample Subject";
            info.Keywords = "Aspose, PDF, Metadata";

            // Persist the changes by saving the document back to PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated metadata to '{outputPath}'.");
    }
}