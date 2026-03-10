using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the DocumentInfo object via the Info property
            DocumentInfo info = doc.Info;

            // Modify standard metadata fields
            info.Title   = "Updated Document Title";
            info.Author  = "John Doe";
            info.Subject = "Metadata Modification Example";
            info.Keywords = "Aspose.Pdf, metadata, example";

            // Add a custom metadata entry (custom keys are allowed)
            info.Add("CustomKey", "CustomValue");

            // Save the modified document as a PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}