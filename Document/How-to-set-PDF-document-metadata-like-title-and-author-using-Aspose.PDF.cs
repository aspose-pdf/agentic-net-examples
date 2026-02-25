using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // PDF with updated metadata

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata properties via the DocumentInfo object
            doc.Info.Title  = "My Document Title";
            doc.Info.Author = "John Doe";

            // Optional: set additional metadata fields
            doc.Info.Subject   = "Sample subject";
            doc.Info.Keywords  = "Aspose, PDF, metadata";
            doc.Info.Creator   = "My Application";
            doc.Info.Producer  = "Aspose.Pdf for .NET";

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}