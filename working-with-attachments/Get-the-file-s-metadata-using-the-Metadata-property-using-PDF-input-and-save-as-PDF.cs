using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle managed by using)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the metadata object via the Metadata property
            Metadata metadata = doc.Metadata;

            // Example: display the type of the metadata object (or any available info)
            Console.WriteLine($"Metadata object type: {metadata?.GetType().FullName ?? "null"}");

            // Save the document as PDF (no format conversion needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with metadata to '{outputPath}'.");
    }
}