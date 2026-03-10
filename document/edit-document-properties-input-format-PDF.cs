using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Edit standard document properties via the DocumentInfo object
            doc.Info.Title   = "New Document Title";
            doc.Info.Author  = "John Doe";
            doc.Info.Subject = "Sample Subject";
            doc.Info.Keywords = "Aspose, PDF, Metadata";

            // Alternatively, you can use the dedicated SetTitle method
            doc.SetTitle("New Document Title");

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Properties updated and saved to '{outputPath}'.");
    }
}